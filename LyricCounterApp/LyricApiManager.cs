using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AireLogicCLIApp
{
  public class LyricApiManager
  {
    private HTTPClientManager _clientManager;

    public LyricApiManager()
    {
      _clientManager = new HTTPClientManager("https://api.lyrics.ovh/v1/", null);
    }

    /// <summary>
    /// Get the song lyrics for a given song
    /// </summary>
    /// <param name="artist">The artist who performs the song</param>
    /// <param name="title">The title of the song</param>
    /// <returns></returns>
    public async Task<string> GetSongLyrics(string artist, string title)
    {
      string lyrics = null;

      if (title.Contains('/'))
      {
        int found = title.IndexOf('/');
        title = title.Substring(0, found);
      }

      try
      {
        string endpoint = String.Format("{0}/{1}", artist.ToLower(), title);

        string responseData = await _clientManager.GetJsonResponse(endpoint);

        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseData);

        lyrics = dict["lyrics"];
      }
      catch (Exception)
      {
        
      }
      return lyrics;
    }
  }
}
