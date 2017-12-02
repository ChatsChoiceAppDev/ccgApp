using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CCG
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class TwitchAuth : ContentPage
  {
    private string m_uri = "http://localhost:11011";
    public TwitchAuth()
    {
      InitializeComponent();
      Browser.Source = TwitchWrapper.Instance.GetLoginUri(m_uri, "token+id_token", "user_read+openid");
      Browser.Navigating += Redirect;
    }

    private void Redirect(object sender, WebNavigatingEventArgs e)
    {
      if (e.Url.IndexOf(m_uri) == 0)
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
        Navigation.PushModalAsync(new MainPage());
      }
    }

    private async void GetUserInfo(string accessToken)
    {
      TwitchUser user = await TwitchWrapper.Instance.GetUser(accessToken);
    }
  }
}