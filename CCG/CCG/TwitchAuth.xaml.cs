using CCG.Contracts;
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
      //string width = "854";
      //string height = "480";
      //string channel = "chatschoicegaming";
      //string html = ResourceLoader.GetEmbeddedResourceString("TwitchStream.html");
      //Browser.Source = html;
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
      CCGWrapper ccgWrapper = new CCGWrapper();

      ToolbarController.AddToolbarItem(userButton);
      Navigation.PushModalAsync(new NavigationPage(new MainPage()));
      User ccgUser;
      try
      {
        ccgUser = await ccgWrapper.GetUser(user.id, IdType.Twitch);
      }
      catch(Exception ex)
      {
        //CCG Server most likely not available
#if DEBUG
        ccgUser = new User();
        ccgUser.Name = "LocalDebugger";
        ccgUser.TwitchID = user.id;
#else
        //Warn the user that CCG servers could not be reached
#endif
      }

      if(ccgUser.Name == null)
      {
        int id= await ccgWrapper.CreateUser(user.id, user.display_name);
      }
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