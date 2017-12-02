using CCG.TwitchWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
  class Program
  {
    static void Main(string[] args)
    {
      TwitchWrapper.Instance.Login(@"http://localhost");
      Console.WriteLine("Press any key to terminate...");
      Console.Read();
    }
  }
}
