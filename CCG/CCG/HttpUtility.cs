using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCG
{
  public static class HttpUtility
  {
    public static Dictionary<string, string> ParseQueryString(string uri)
    {
      int pos = uri.IndexOf('?');
      if(pos < 0)
      {
        pos = uri.IndexOf('#');
      }
      pos++; // +1 for skipping '?' or '#'
      var query = uri.Substring(pos); 
      var pairs = query.Split('&');
      return pairs
          .Select(o => o.Split('='))
          .Where(items => items.Count() == 2)
          .ToDictionary(pair => Uri.UnescapeDataString(pair[0]),
              pair => Uri.UnescapeDataString(pair[1]));
    }
  }
}
