using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using AireLogicCLIApp;
using System.Threading.Tasks;
using System.Threading;

namespace AireLogicTests
{
  public class Tests
  {
    MusicBrainzManager _clientManager;
    [SetUp]
    public void Setup()
    {
      _clientManager = new MusicBrainzManager();
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
    public async Task GetReleseGroups(string artistId)
    {
      ReleaseGroupsWrapper releaseGroupWrapper = await _clientManager.GetAlbumReleaseGroups(artistId);
      Assert.NotNull(releaseGroupWrapper);
    }
  }
}