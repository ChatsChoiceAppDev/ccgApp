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

      image.Source = ImageSource.FromResource("CCG.bin.Images.ccgtext.png");

    }


    protected override async void OnAppearing()
    {
      base.OnAppearing();

      await Task.Delay(2000);

      await Task.WhenAll(
        SplashGrid.FadeTo(0, 500),
        Logo.ScaleTo(0, 300)
        );
      SplashGrid.IsVisible = false;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
      await Navigation.PushModalAsync(new TwitchAuth());
    }

  }
}