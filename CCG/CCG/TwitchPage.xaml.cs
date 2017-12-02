using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CCG
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class TwitchPage : ContentPage
  {
    public TwitchPage()
    {
      InitializeComponent();
      string width = "854";
      string height = "480";
      string channel = "chatschoicegaming";
      TwitchChannelView.Source =
        "<html>" +
          "<body>" +
            "<div id=\"twitch-embed\"></div>" +
            "<script src=\"https://embed.twitch.tv/embed/v1.js\" ></script>" +
            "<script type=\"text/javascript\">" +
              "new Twitch.Embed(\"twitch-embed\", {" +
                $"width: {width}," +
                $"height: {height}," +
                $"channel: \"{channel}\"" +
              "});" +
          "</script>" +
          "</body>" +
        "</html>";
    }
  }
}