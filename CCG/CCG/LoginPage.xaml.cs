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
      StartListening();
      image.Source = ImageSource.FromResource("CCG.Images.charmy.png");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
      await Navigation.PushAsync(new TwitchAuth());
    }

    private async void StartListening()
    {
      var listenPort = 11011;
      var receiver = new UdpSocketReceiver();

      receiver.MessageReceived += (sender, args) =>
      {
        // get the remote endpoint details and convert the received data into a string
        var from = String.Format("{0}:{1}", args.RemoteAddress, args.RemotePort);
        var data = Encoding.UTF8.GetString(args.ByteData, 0, args.ByteData.Length);
      };

      // listen for udp traffic on listenPort
      await receiver.StartListeningAsync(listenPort);
    }
  }
}