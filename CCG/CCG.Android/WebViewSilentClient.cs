using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace CCG.Droid
{
  public class WebViewSilentClient : WebViewClient
  {
    List<Action<string>> m_navigationHandlers = new List<Action<string>>();
    public override void OnPageStarted(WebView view, string url, Bitmap favicon)
    {
      foreach(var handler in m_navigationHandlers)
      {
        handler(url);
      }
    }

    public void AddNavigationHandler(Action<string> navHandler)
    {
      m_navigationHandlers.Add(navHandler);
    }
  }
}