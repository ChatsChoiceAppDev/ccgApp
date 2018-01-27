using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;

namespace CCG.WebApi
{
  public static class Util
  {
    public const string ConnectString = @"Server=localhost\SQLEXPRESS;Database=CCG;Trusted_Connection=True;";

    internal static SecureString ConvertToSecureString(string password)
    {
      if (password == null)
        throw new ArgumentNullException("password");

      var securePassword = new SecureString();

      foreach (char c in password)
        securePassword.AppendChar(c);

      securePassword.MakeReadOnly();
      return securePassword;
    }
  }
}