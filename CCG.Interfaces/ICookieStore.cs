using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace CCG
{
  public interface ICookieStore
  {
    IEnumerable<Cookie> CurrentCookies { get; }
    void DeleteAllCookiesForSite(string url);
    void DeleteCookie(string cookie);
    Cookie GetCookie(string name);
  }
}
