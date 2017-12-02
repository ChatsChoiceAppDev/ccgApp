using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CCG.TwitchWrapper
{
  public class TwitchWrapper
  {
    static TwitchWrapper m_instance = null;
    private string m_clientID = "j2ov9wff57ogplmzfdobg2r6ne29br";
    public static TwitchWrapper Instance
    {
      get
      {
        if (m_instance == null)
        {
          m_instance = new TwitchWrapper();
        }

        return m_instance;
      }
    }

    public bool Login(string redirectUri, 
      string responseType = "code", string scope = "user_read")
    {
      Uri uri = new Uri(@"https://api.twitch.tv/");
      HttpClient client = new HttpClient();
      client.BaseAddress = uri;
      client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
      string requestUri = $"kraken/oauth2/authorize?response_type={responseType}&client_id={m_clientID}&redirect_uri={redirectUri}&scope={scope}";
      var response = client.GetAsync(requestUri).Result;
      //"?response_type=code&client_id=8bmp6j83z5w4mepq0dn0q1a7g186azi&redirect_uri=https%3A%2F%2Fstreamlabs.com%2Fauth&scope=user_read+channel_subscriptions+user_subscriptions+chat_login";
      string responseString = response.Content.ReadAsStringAsync().Result;
      return false;
    }
  }
}
