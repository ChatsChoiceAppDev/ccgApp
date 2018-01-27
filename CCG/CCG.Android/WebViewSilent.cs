using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace CCG.Droid
{
  public class WebViewSilent : WebView, IWebViewSilent
  {
    public WebViewSilent(Context context) : base(context)
    {
    }

    public WebViewSilent(Context context, IAttributeSet attrs) : base(context, attrs)
    {
    }

    public WebViewSilent(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
    {
    }

    public WebViewSilent(Context context, IAttributeSet attrs, int defStyleAttr, bool privateBrowsing) : base(context, attrs, defStyleAttr, privateBrowsing)
    {
    }

    public WebViewSilent(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
    {
    }

    protected WebViewSilent(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
    {
    }

    WebViewClient m_client = null;
    public override void SetWebViewClient(WebViewClient client)
    {
      m_client = client;
      base.SetWebViewClient(client);
    }

    public void AddNavigationHandler(Action<string> navHandler)
    {
      if (m_client != null)
      {
        //WebViewSilentClient silentClient = this.GetC
      }
    }
  }
}