using System.Collections.Generic;
using Newtonsoft.Json;

namespace AireLogicCLIApp
{
  public class ArtistSearchResult
  {
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("id")]
    public string Id { get; set; }
  }

  public class ArtistWrapper
  {
    [JsonProperty("artists")]
    public List<ArtistSearchResult> Artists { get; set; }
  }
}
