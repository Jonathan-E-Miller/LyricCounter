using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace AireLogicCLIApp
{
  public class Media
  {
    [JsonProperty("format-id")]
    private string FormatId { get; set; }
    
    [JsonProperty("format")]
    public string Format { get; set; }

    [JsonProperty("track-count")]
    public int TrackCount { get; set; }

    [JsonProperty("tracks")]
    public List<Track> Tracks {get; set;}
  }

  public class MediaWrapper
  {
    [JsonProperty("media")]
    public List<Media> Mediums { get; set; }
  }

  public class Track
  {
    [JsonProperty("position")]
    public int Position { get; set; }
   
    [JsonProperty("title")]
    public string Title { get; set; }
  }
}
