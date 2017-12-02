using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCG
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

    public string GetLoginUri(string redirectUri,
      string responseType = "code", string scope = "user_read")
    {
      string uri = "https://api.twitch.tv/" + 
        $"kraken/oauth2/authorize?response_type={responseType}&client_id={m_clientID}&redirect_uri={redirectUri}&scope={scope}";
      return uri;
    }
  }
}
