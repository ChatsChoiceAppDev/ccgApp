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
  public partial class SocialMediaPage : ContentPage
  {
    public SocialMediaPage()
    {
      InitializeComponent();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
      Device.OpenUri(new Uri("https://twitter.com/chatschoice"));
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
      Device.OpenUri(new Uri("https://www.instagram.com/chatschoicegaming/"));
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
      Device.OpenUri(new Uri("https://discord.gg/gXzKzn8"));
    }
  }
}
