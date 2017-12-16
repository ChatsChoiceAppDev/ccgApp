using CCG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace CCG.Droid
{
  public class DroidCookieStore : ICookieStore
  {
    private string m_url;
    private readonly object m_refreshLock = new object();

    public string Url
    {
      get
      {
        return m_url;
      }
      set
      {
        m_url = value;
      }
    }

    public IEnumerable<Cookie> CurrentCookies
    {
      get { return RefreshCookies(); }
    }

    public DroidCookieStore(string url = "")
    {
      if (string.IsNullOrWhiteSpace(url))
      {
        throw new ArgumentNullException("url", "On Android, 'url' cannot be empty, " +
          "please provide a base URL for it to use when loading related cookies");
      }
      m_url = url;
    }

    private IEnumerable<Cookie> RefreshCookies()
    {
      lock (m_refreshLock)
      {
        // .GetCookie returns ALL cookies related to the URL as a single, long          
        // string which we have to split and parse         
        var allCookiesForUrl = CookieManager.Instance.GetCookie(m_url);

        if (string.IsNullOrWhiteSpace(allCookiesForUrl))
        {
          //LogDebug(string.Format("No cookies found for '{0}'. Exiting.", _url));
          yield return new Cookie("none", "none");
        }
        else
        {
          //LogDebug(string.Format("\r\n===== CookieHeader : '{0}'\r\n", allCookiesForUrl));

          var cookiePairs = allCookiesForUrl.Split(' ');
          foreach (var cookiePair in cookiePairs.Where(cp => cp.Contains("=")))
          {
            // yeah, I know, but this is a quick-and-dirty, remember? ;)             
            var cookiePieces = cookiePair.Split
              (new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            if (cookiePieces.Length >= 2)
            {
              cookiePieces[0] = cookiePieces[0].Contains(":")
                ? cookiePieces[0].Substring(0, cookiePieces[0].IndexOf(":"))
                : cookiePieces[0];

              // strip off trailing ';' if it's there (some implementations 
              // on droid have it, some do not)               
              cookiePieces[1] = cookiePieces[1].EndsWith(";")
                ? cookiePieces[1].Substring(0, cookiePieces[1].Length - 1)
                : cookiePieces[1];

              yield return new Cookie()
              {
                Name = cookiePieces[0],
                Value = cookiePieces[1],
                Path = "/",
                Domain = new Uri(m_url).DnsSafeHost,
              };
            }
          }
        }
      }
    }

    public void DumpAllCookiesToLog()
    {
      // same as for iOS     
    }


    public void DeleteAllCookiesForSite(string url)
    {
      
    }

    public void DeleteCookie(string name)
    {
      CookieManager.Instance.RemoveAllCookie();
    }

    public Cookie GetCookie(string name)
    {
      var cookies = CurrentCookies;

      foreach(var cookie in cookies)
      {
        if(cookie.Name == name)
        {
          return cookie;
        }
      }

      return null;
    }
  }
}