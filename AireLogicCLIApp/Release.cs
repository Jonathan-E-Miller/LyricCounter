using Newtonsoft.Json;
using System.Collections.Generic;

public class Release
{
  [JsonProperty("id")]
  public string Id { get; set; }
  [JsonProperty("country")]
  public string Country { get; set; }
}

public class ReleaseWrapper
{
  [JsonProperty("releases")]
  public List<Release> Releases { get; set; }
}