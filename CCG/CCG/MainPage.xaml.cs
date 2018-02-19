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
      
    }
  }
}
