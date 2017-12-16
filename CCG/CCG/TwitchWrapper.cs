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
  public class TwitchWrapper
  {
    static TwitchWrapper m_instance = null;
    private string m_clientID = "j2ov9wff57ogplmzfdobg2r6ne29br";
    private string m_twitchBaseAddress = "https://api.twitch.tv/";
    private int queryId = 0;
    private WebView m_webView = new WebView();

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
      string uri = m_twitchBaseAddress +
        $"kraken/oauth2/authorize?response_type={responseType}&client_id={m_clientID}&redirect_uri={redirectUri}&scope={scope}";
      return uri;
    }

    public async Task<TwitchUser> GetUser(string bearer)
    {
      HttpClient client = new HttpClient();
      client.BaseAddress = new Uri(m_twitchBaseAddress);
      client.DefaultRequestHeaders.Accept.Add
        (new MediaTypeWithQualityHeaderValue("application/json"));
      client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Client-ID", m_clientID);
      client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", bearer);

      string userStr = await client.GetStringAsync("helix/users");
      if (!string.IsNullOrEmpty(userStr))
      {
        TwitchUsers users = JsonConvert.DeserializeObject<TwitchUsers>(userStr);
        return users.data[0];
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Authentication for when there is a cached login in the cookie store
    /// </summary>
    /// <returns></returns>
    public bool AuthenticateSilently(Action<TwitchUser> authCallback)
    {
      if (IsLoginCached())
      { 
        m_webView.WidthRequest = 1000;
        m_webView.HeightRequest = 1000;
        m_webView.Navigating += Redirect;
        string loginUri = GetLoginUri(Util.Constants.RedirectUrl, "token+id_token", "user_read+openid");
        m_webView.Source = loginUri;
        m_webView.IsEnabled = true;
        return true;
      }

      return false;
    }

    public bool IsLoginCached()
    {
      ICookieStore cookieStore =
        Splat.Locator.Current.GetService(typeof(ICookieStore)) as ICookieStore;

      foreach (var cookie in cookieStore.CurrentCookies)
      {
        if (cookie.Name == "login")
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Private event handler to react when the WebView redirects
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Redirect(object sender, WebNavigatingEventArgs e)
    {
      if (e.Url.IndexOf(Util.Constants.RedirectUrl) == 0)
      {
        Uri uri = new Uri(e.Url);

        //Get the token and id_token
        string fragment = uri.Fragment;
        var values = HttpUtility.ParseQueryString(fragment);
        string accessToken = values["access_token"];

        //Cancel navigation
        e.Cancel = true;

      }
    }
  }

  public class TwitchUser
  {
    public TwitchUser()
    {
    }

    public string broadcaster_type { get; set; }
    public string description { get; set; }
    public string display_name { get; set; }
    public string email { get; set; }
    public string id { get; set; }
    public string login { get; set; }
    public string offline_image_url { get; set; }
    public string profile_image_url { get; set; }
    public string type { get; set; }
    public int view_count { get; set; }
  }

  public class TwitchUsers
  {
    public TwitchUsers()
    {
    }

    public List<TwitchUser> data;
  }
}
