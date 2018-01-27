using CCG.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
  class Program
  {
    static void Main(string[] args)
    {
      PostQuery();
      Console.WriteLine("Press any key to terminate...");
      Console.Read();
    }

    private static async void PostQuery()
    {
      User newUser = new User();
      newUser.TwitchID = 123;
      newUser.Name = "Big test boy";

      HttpClient client = new HttpClient();
      //client.BaseAddress = new Uri("http://68.205.74.37/ccg/");
      string jsonContent = JsonConvert.SerializeObject(newUser);
      StringContent content = new StringContent("borpo", Encoding.UTF32);

      var response = await client.PostAsync($"http://68.205.74.37/ccg/api/user/?user={jsonContent}", null).ConfigureAwait(false);
      Console.WriteLine("Post query completed.");

    }
  }
}
