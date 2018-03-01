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
      //var source = new HtmlWebViewSource
      //{
      //  Html = html
      //};

      //TwitchChannelView.Source = source;
      //TwitchChannelView.IsVisible = true;
    }

    public void SetTwitchStreamViewSource(string source)
    {

      var src = new HtmlWebViewSource
      {
        Html = source
      };

      TwitchChannelView.Source = src;
    }

    public void SetTwitchStreamViewSource(int width, int height, string channel)
    {
      string w = width.ToString();
      string h = height.ToString();
      string html = ResourceLoader.GetEmbeddedResourceString("TwitchStreamParams.html");
      string formatted = string.Format(html, w, h, channel);
      SetTwitchStreamViewSource(formatted);
    }

    public void SetTwitchStreamViewFitted(string channel)
    {
      string w = "854";
      if (Width > 0)
      {
        double width = Width - 8;
        w = width.ToString();
      }

      string h = "480";
      if (Height > 0)
      {
        double height = Height - 8;
        h = height.ToString();
      }
      
      string html = ResourceLoader.GetEmbeddedResourceString("TwitchStreamParams.html");
      html = html.Replace("%%WIDTH%%", w);
      html = html.Replace("%%HEIGHT%%", h);
      html = html.Replace("%%CHANNEL%%", channel);
      SetTwitchStreamViewSource(html);
    }
  }
}