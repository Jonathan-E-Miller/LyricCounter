using Newtonsoft.Json;
using System.Collections.Generic;

public class ReleaseGroup
{

  [JsonProperty("title")]
  public string Title { get; set; }

  [JsonProperty("first-release-date")]
  public string ReleaseDate { get; set; }

  [JsonProperty("id")]
  public string Id { get; set; }

}

public class ReleaseGroupsWrapper
{
  [JsonProperty("release-groups")]
  public List<ReleaseGroup> ReleaseGroups { get; set; }
}
