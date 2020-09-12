using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using AireLogicCLIApp;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;

namespace AireLogicTests
{
  public class Tests
  {
    MusicBrainzManager _clientManager;
    LyricApiManager _lyricManager;
    Artist _artist;
    [SetUp]
    public void Setup()
    {
      _clientManager = new MusicBrainzManager();
      _lyricManager = new LyricApiManager();


      _artist = new Artist()
      {
        Name = "Test",
        Albums = new List<Album>()
        {
          new Album("One", new List<Track>() {
            new Track()
            {
              Lyrics = "These are some",
              Title = "Test Song"
            },
            new Track()
            {
              Lyrics = "Pretend Song Lyrics",
              Title = "Test Song"
            },
            new Track()
            {
              Lyrics = "For Testing purposes only",
              Title = "Test Song"
            },
            new Track()
            {
              Lyrics = "If our statistic manager\nclass    and\nit's functions",
              Title = "Test Song"
            },
            new Track()
            {
              Lyrics = "Work Properly",
              Title = "Test Song"
            },
          })
        }
      };
    }

    [TestCase("Coldplay", "cc197bad-dc9c-440d-a5b5-d52ba2e14234")]
    [TestCase("Green day", "084308bd-1654-436f-ba03-df6697104e19")]
    public async Task TestGetArtistId(string artist, string id)
    {
      string result = await _clientManager.GetArtistId(artist);
      Assert.AreEqual(result, id);
    }

    [TestCase("cc197bad-dc9c-440d-a5b5-d52ba2e14234")]
    [TestCase("084308bd-1654-436f-ba03-df6697104e19")]
    public async Task TestGetReleaseGroups(string artistId)
    {
      ReleaseGroupsWrapper releaseGroupWrapper = await _clientManager.GetAlbumReleaseGroups(artistId);
      Assert.NotNull(releaseGroupWrapper);
    }

    [TestCase("c58228d1-05e9-3ce0-83f6-b0d33ffcaa90", true)]
    [TestCase("xxxxxx-bad-id-xxxxx", false)]
    public async Task TestGetUkUSVersion(string releaseGroupId, bool shouldPass)
    {
      Release release = await _clientManager.GetGBUSVersion(releaseGroupId);

      if (shouldPass)
      {
        Assert.IsNotNull(release);
      }
      else
      {
        Assert.IsNull(release);
      }
    }

    [TestCase("c1bea9b6-2aa2-4ae9-b1ac-c8d99c0e04fd", 10)]
    public async Task TestGetTracksFromRelease(string release, int trackCount)
    {
      List<Track> tracks = await _clientManager.FindTracksOnRelease(release);

      Assert.AreEqual(tracks.Count, trackCount);
    }

    [TestCase("Coldplay", "Yellow", true)]
    [TestCase("Green day", "Basket Case", true)]
    [TestCase("Should", "Fail", false)]
    public async Task TestGetSongLyrics(string artist, string song, bool shouldPass)
    {
      string lyrics = await _lyricManager.GetSongLyrics(artist, song);

      if (shouldPass)
      {
        Assert.IsNotNull(lyrics);
      }
      else
      {
        Assert.IsNull(lyrics);
      }
    }

    [TestCase]
    public void TestAverage()
    {
      StatisticManager statisticManager = new StatisticManager(_artist);
      double average = statisticManager.CalculateAverageNumberOfWordsInSong();
      Assert.AreEqual(average, 4);

    }

    [TestCase]
    public void TestStandardDeviation()
    {
      StatisticManager statisticManager = new StatisticManager(_artist);
      double stdDev = statisticManager.CalculateStandardDeviation();
      Assert.AreEqual(Math.Round(stdDev, 1), 2.1);
    }
  }
}