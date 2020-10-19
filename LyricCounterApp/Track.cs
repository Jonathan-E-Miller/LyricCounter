using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AireLogicCLIApp
{
  public class Track
  {
    [JsonProperty("position")]
    public int Position { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    public string Lyrics { get; set; }

  }
}
