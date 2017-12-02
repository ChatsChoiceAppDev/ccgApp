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
  public partial class LoginPage : ContentPage
  {
    public LoginPage()
    {
      InitializeComponent();

      image.Source = ImageSource.FromResource("CCG/Images/charmy.png");
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
      WebView webView = new WebView
      { Source =  TwitchWrapper.Instance.LoginUri("http://localhost") };
    }
  }
}