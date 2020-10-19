using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace AireLogicCLIApp
{
  public class Program
  {
    static void Main(string[] args)
    {
      while (true)
      {
        WriteInformation("Please Enter an Artist Name e.g. Coldplay or type exit to quit the application");
        string input = Console.ReadLine();

        if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
        {
          break;
        }

        Task<Artist> artistTask = Task.Run(() => Discover(input));
        artistTask.Wait();
        Artist artist = artistTask.Result;


      }
    }

    /// <summary>
    /// Go through the discovery procedure of a new artist.
    /// </summary>
    /// <param name="artistName">The name of the artist to discover</param>
    /// <returns></returns>
    public static async Task<Artist> Discover(string artistName)
    {
      Artist artist = null;
      MusicBrainzManager musicBrainzManager = new MusicBrainzManager();
      LyricApiManager lyricApiManager = new LyricApiManager();

      // get the artist ID and check that it is not null.
      string artistId = await musicBrainzManager.GetArtistId(artistName);
      if (artistId != null)
      {
        ReleaseGroupsWrapper rgw = await musicBrainzManager.GetAlbumReleaseGroups(artistId);

        // some variables to give the user some feedback.
        int progress = 0;
        int total = rgw.ReleaseGroups.Count;
        // go through all release group objects
        foreach (ReleaseGroup rg in rgw.ReleaseGroups)
        {
          progress++;
          // for a release group find the UK or US version.
          Release release = await musicBrainzManager.GetGBUSVersion(rg.Id);
         
          // if we have a UK or US version
          if (release != null)
          {
            WriteInformation(String.Format("Album {0}/{1}...", progress, total));
            WriteInformation(String.Format("Artist {0} has Album {1}", artistName, rg.Title));
            // Get all tracks for the release
            List<Track> tracks = await musicBrainzManager.FindTracksOnRelease(release.Id);
            foreach(Track track in tracks)
            {
              track.Lyrics = await lyricApiManager.GetSongLyrics(artistName, track.Title);
              WriteInformation(String.Format("\t Track {0} with {1} Words", track.Title, StringHelper.WordCount(track.Lyrics)));
            }
            // Create the Artist object
            artist = new Artist() { Name = artistName };
            artist.Albums.Add(new Album(rg.Title, tracks));
            
          }
          else
          {
            WriteInformation(String.Format("Artist {0} Album {1} does not have a UK or US Version", artistName, rg.Title));
          }
        }
      }
      if (artist != null)
      {
        StatisticManager manager = new StatisticManager(artist);
        double average = manager.CalculateAverageNumberOfWordsInSong();
        double standardDev = manager.CalculateStandardDeviation();
        WriteResult(String.Format("Average Number of words = {0}\nStandard Deviation = {1}", average, standardDev));
      }
     
      WriteResult("*********************************DONE*********************************");
      return artist;
    }

    public static void WriteInformation(string message)
    {
      Console.BackgroundColor = ConsoleColor.Black;
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine(message);
    }

    public static void WriteError(string message)
    {
      Console.BackgroundColor = ConsoleColor.Black;
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(message);
      Console.ForegroundColor = ConsoleColor.White;
    }

    public static void WriteResult(string message)
    {
      Console.BackgroundColor = ConsoleColor.Black;
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine(message);
      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}
