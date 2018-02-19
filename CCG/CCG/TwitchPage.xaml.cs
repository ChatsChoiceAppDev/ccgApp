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
  }
}