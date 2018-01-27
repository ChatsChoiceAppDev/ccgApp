using CCG.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CCG
{
  public enum IdType
  {
    CCG,
    Twitch
  }
  public class CCGWrapper
  {
    private string m_ccgBaseAdress = "http://68.205.74.37/ccg/";

    public async Task<User> GetUser(int id, IdType idType = IdType.CCG)
    {
      HttpClient client = new HttpClient();
      client.DefaultRequestHeaders.Accept.Add
        (new MediaTypeWithQualityHeaderValue("application/json"));
      client.BaseAddress = new Uri(m_ccgBaseAdress);
      string request = "api/user/";
      if (idType == IdType.CCG)
      {
        request += id;
      }
      else
      {
        request += $"twitchID/{id}";
      }

      var userStr = await client.GetStringAsync(request);
      if (!string.IsNullOrEmpty(userStr))
      {
        User user = JsonConvert.DeserializeObject<User>(userStr);
        return user;
      }
      else
      {
        return null;
      }
    }

    public async Task<bool> CreateUser(int twitchID, string name)
    {
      User newUser = new User();
      newUser.TwitchID = twitchID;
      newUser.Name = name;

      HttpClient client = new HttpClient();
      client.BaseAddress = new Uri(m_ccgBaseAdress);
      string jsonContent = JsonConvert.SerializeObject(newUser);
      StringContent content = 
        new StringContent(jsonContent, Encoding.UTF8, "application/json");

      var response = await client.PostAsync($"api/user/?user={jsonContent}", null);

      return response.IsSuccessStatusCode;
    }
  }
}
