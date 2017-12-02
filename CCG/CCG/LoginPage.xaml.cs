using Sockets;
using Sockets.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Net;
using System.Threading;

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
      image.Source = ImageSource.FromResource("CCG.Images.charmy.png");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
      await Navigation.PushModalAsync(new TwitchAuth());
    }
  }
}