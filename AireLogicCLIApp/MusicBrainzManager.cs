using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireLogicCLIApp
{
  public class MusicBrainzManager
  {
    public HTTPClientManager _clientManager;

    public MusicBrainzManager()
    {
      _clientManager = new HTTPClientManager("https://musicbrainz.org/ws/2/",1000);
    }

    /// <summary>
    /// Get the artist Id being used in the MusicBrainz database for the artist
    /// specified by the input parameter
    /// </summary>
    /// <param name="artist">The artist name e.g. Coldplay</param>
    /// <returns>The Artist Id</returns>
    public async Task<string> GetArtistId(string artist)
    {
      string artistId = null;
      string endpoint = String.Format("artist?query={0}", artist);
      string responseData = await _clientManager.GetJsonResponse(endpoint);

      if (responseData != null)
      {
        // Parse the result into our ArtistWrapper, there could be multiple matches.
        ArtistWrapper wrapper = JsonConvert.DeserializeObject<ArtistWrapper>(responseData);

        // Only take an exact match
        IEnumerable<ArtistSearchResult> matches = wrapper.Artists.Where(a => a.Name.Equals(artist, StringComparison.OrdinalIgnoreCase));

        if (matches.Count() >= 1)
        {
          artistId = matches.ToList()[0].Id;
        }
      }
      return artistId;
    }

    /// <summary>
    /// Get a list of Album Release Groups for a given artist.
    /// </summary>
    /// <param name="artistId">The artist Id to search for</param>
    /// <returns>ReleaseGroupWrapper object which contains a list of Release Groups</returns>
    public async Task<ReleaseGroupsWrapper> GetAlbumReleaseGroups(string artistId)
    {
      ReleaseGroupsWrapper releaseGroup = null;
      
      string endpoint = String.Format("artist/{0}?inc=release-groups&type=album", artistId);
      string responseData = await _clientManager.GetJsonResponse(endpoint);

      if (responseData != null)
      {
        releaseGroup = JsonConvert.DeserializeObject<ReleaseGroupsWrapper>(responseData);
      }

      return releaseGroup;
    }
  }
}
