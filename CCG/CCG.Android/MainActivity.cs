using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Android.OS;

namespace CCG.Droid
{
  [Activity(Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
  {
    protected override void OnCreate(Bundle bundle)
    {
      Splat.Locator.CurrentMutable.Register(CreateTwitchCookieStore, typeof(ICookieStore));

      TabLayoutResource = Resource.Layout.Tabbar;
      ToolbarResource = Resource.Layout.Toolbar;

      SetContentView(Resource.Layout.WebViewSilent);

      WebView silentWebView = FindViewById<WebView>(Resource.Id.WebViewSilent);
      WebViewClient client = new WebViewClient();
      silentWebView.SetWebViewClient(new WebViewClient()); // stops request going to Web Browser

      //Splat.Locator.CurrentMutable.Register();

      base.OnCreate(bundle);

      global::Xamarin.Forms.Forms.Init(this, bundle);
      LoadApplication(new App());
    }

    private object CreateTwitchCookieStore()
    {
      return new DroidCookieStore("https://www.twitch.tv/");
    }
  }
}

