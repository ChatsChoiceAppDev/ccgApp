using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CCG
{
  using System;
  using System.Collections.Generic;

  using System.Globalization;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Converters;

  public partial class GameSearchResult
  {
    [JsonProperty("games")]
    public List<Game> Games { get; set; }
  }

  public partial class Game
  {
    [JsonProperty("_id")]
    public long Id { get; set; }

    [JsonProperty("box")]
    public Box Box { get; set; }

    [JsonProperty("giantbomb_id")]
    public long GiantbombId { get; set; }

    [JsonProperty("logo")]
    public Box Logo { get; set; }

    public ImageSource BoxSmall
    {
      get
      {
        return ImageSource.FromUri(new Uri(Box.Small));
      }
    }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("popularity")]
    public long Popularity { get; set; }
  }

  public partial class Box
  {
    [JsonProperty("large")]
    public string Large { get; set; }

    [JsonProperty("medium")]
    public string Medium { get; set; }

    [JsonProperty("small")]
    public string Small { get; set; }

    [JsonProperty("template")]
    public string Template { get; set; }
  }

  public partial class GameSearchResult
  {
    public static GameSearchResult FromJson(string json) => JsonConvert.DeserializeObject<GameSearchResult>(json, Converter.Settings);
  }

  public static class Serialize
  {
    public static string ToJson(this GameSearchResult self) => JsonConvert.SerializeObject(self, Converter.Settings);
  }

  internal static class Converter
  {
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
      MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
      DateParseHandling = DateParseHandling.None,
      Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
    };
  }
}
