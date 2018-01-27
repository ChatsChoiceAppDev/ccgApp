using AndroidSpecific = Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CCG
{
  public partial class App : Application
  {
    public App()
    {
      //Check to see if there's a login cookie
      if (!TwitchWrapper.Instance.AuthenticateSilently(HandleAuthentication))
      {
        //No login cookie, go to the login page
        MainPage = new LoginPage();
        InitializeComponent();
      }
      else
      {
        MainPage = new TwitchAuth();
      }

      AndroidSpecific.Application.SetWindowSoftInputModeAdjust
        (this, AndroidSpecific.WindowSoftInputModeAdjust.Resize);
    }

    protected override void OnStart()
    {
      // Handle when your app starts
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }

    private void HandleAuthentication(TwitchUser user)
    {
      //Silent authenticate
      MainPage = new MainPage();
      InitializeComponent();
    }
  }
}
