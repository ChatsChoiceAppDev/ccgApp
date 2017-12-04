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
    }

    protected override async void OnAppearing()
    {
      base.OnAppearing();

      await Task.Delay(2000);

      await Task.WhenAll(
        SplashGrid.FadeTo(0, 2000),
        Logo.ScaleTo(0, 250)
        );
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
      await Navigation.PushModalAsync(new TwitchAuth());
    }
  }
}