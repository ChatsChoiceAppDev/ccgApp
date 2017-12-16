using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CCG
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class TwitchAuth : ContentPage
  {
    public TwitchAuth()
    {
      InitializeComponent();
      Browser.Source = TwitchWrapper.Instance.GetLoginUri
        (Util.Constants.RedirectUrl, "token+id_token", "user_read+openid");
      Browser.Navigating += Redirect;
    }

    private void Redirect(object sender, WebNavigatingEventArgs e)
    {
      if (e.Url.IndexOf(Util.Constants.RedirectUrl) == 0)
      {
        Uri uri = new Uri(e.Url);

        //Get the token and id_token
        string fragment = uri.Fragment;
        var values = HttpUtility.ParseQueryString(fragment);
        string accessToken = values["access_token"];

        GetUserInfo(accessToken);

        //Cancel navigation
        e.Cancel = true;

        //Clear navigation page
        foreach (Page page in Navigation.NavigationStack)
        {
          Navigation.RemovePage(page);
        }

        //Go to MainPage
        //Navigation.PushModalAsync(new NavigationPage(new MainPage()));
      }
    }

    private async void GetUserInfo(string accessToken)
    {
      TwitchUser user = await TwitchWrapper.Instance.GetUser(accessToken);
      ToolbarItem userButton = new ToolbarItem(user.display_name, "", Logout);
      string name = user.display_name;
      ToolbarController.AddToolbarItem(userButton);
      Navigation.PushModalAsync(new NavigationPage(new MainPage()));
    }

    /// <summary>
    /// Action handler for the logout button
    /// </summary>
    private void Logout()
    {
      ICookieStore cookieStore = 
        Splat.Locator.Current.GetService(typeof(ICookieStore)) as ICookieStore;
      cookieStore.DeleteCookie("login");
      ToolbarController.ToolbarPage = null;
      Navigation.PushModalAsync(new LoginPage());
    }
  }
}