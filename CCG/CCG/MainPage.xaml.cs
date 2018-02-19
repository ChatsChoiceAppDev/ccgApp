using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CCG
{
  public partial class MainPage : TabbedPage
  {
    public MainPage()
    {
      InitializeComponent();
      ToolbarController.ToolbarPage = this;
    }

    private void ToolbarItemActivated()
    {

    }
    private void ToolbarItem_Activated(object sender, EventArgs e)
    {

    }

    private void TwitchPage_Appearing(object sender, EventArgs e)
    {
      string width = "854";
      string height = "480";
      string channel = "chatschoicegaming";
      string html = ResourceLoader.GetEmbeddedResourceString("TwitchStream.html");
      TwitchPage twitchPage = CurrentPage as TwitchPage;
      twitchPage.SetTwitchStreamViewSource(html);
    }
  }
}
