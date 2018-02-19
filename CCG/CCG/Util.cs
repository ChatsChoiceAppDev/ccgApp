using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CCG
{
  public static class Util
  {
    public struct Constants
    {
      public const string RedirectUrl = "http://localhost:11011";
    }

    public static int GetNumbers(string input)
    {
      string retVal = "";
      foreach (char c in input.ToCharArray())
      {
        if (c >= 48 && c <= 57)
        {
          retVal += c.ToString();
        }
      }

      return Convert.ToInt32(retVal);
    }
  }
}
