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
      Browser.Source = TwitchWrapper.Instance.GetLoginUri(m_uri);
      Browser.Navigating += Redirect;
    }

    private void Redirect(object sender, WebNavigatingEventArgs e)
    {
      if (e.Url.IndexOf(m_uri) == 0)
      {
        //Get the auth code

        //Cancel navigation
        e.Cancel = true;

        //Go to MainPage
        Navigation.PushModalAsync(new MainPage());
      }
    }
  }
}