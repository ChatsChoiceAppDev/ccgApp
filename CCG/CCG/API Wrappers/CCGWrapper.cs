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

    public async Task<int> CreateUser(int twitchID, string name)
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
      string idStr = response.Content.ReadAsStringAsync().Result;
      int id = Util.GetNumbers(idStr);

      return id;
    }

    /// <summary>
    /// SubmitSuggestion creates a new suggestion and adds it to the CCG 
    /// OptionSuggestions data table
    /// </summary>
    /// <param name="userID">The ID of the CCG user making this suggestion
    /// </param>
    /// <param name="gameName">The name of the game they're suggesting</param>
    /// <param name="description">The description of the game they're 
    /// suggesting.</param>
    /// <returns>Unique ID of the suggestion</returns>
    /// <exception cref="WebException">Thrown if the CCG server can't be 
    /// reached.</exception>
    public async Task<int> SubmitSuggestion(int userID, string gameName, string description)
    {
      Option newOption = new Option();
      newOption.UserID = userID;
      newOption.Name = gameName;
      newOption.Description = description;

      HttpClient client = new HttpClient();
      client.BaseAddress = new Uri(m_ccgBaseAdress);
      string jsonContent = JsonConvert.SerializeObject(newOption);

      var response = await client.PostAsync($"api/Suggestion/?option={jsonContent}", null);
      if (response.IsSuccessStatusCode)
      {
        string idStr = response.Content.ReadAsStringAsync().Result;
        int id = Util.GetNumbers(idStr);
        return id;
      }
      else
      {
        throw (new WebException(response.ReasonPhrase));
      }
      
    }

    public async Task<bool> UpdateSuggestion(int userID, string gameName, string description)
    {
      Option newOption = new Option();
      newOption.UserID = userID;
      newOption.Name = gameName;
      newOption.Description = description;

      HttpClient client = new HttpClient();
      client.BaseAddress = new Uri(m_ccgBaseAdress);
      string jsonContent = JsonConvert.SerializeObject(newOption);

      var response = await client.PutAsync($"api/Suggestion/?option={jsonContent}", null);

      return response.IsSuccessStatusCode;
    }
  }
}
