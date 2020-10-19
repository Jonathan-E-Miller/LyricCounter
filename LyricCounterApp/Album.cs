using System;
using System.Collections.Generic;
using System.Text;

namespace AireLogicCLIApp
{
  public class Album
  {
    private readonly List<Track> _tracks;
    private readonly string _title;
    
    public Album(string title, List<Track> tracks)
    {
      _tracks = tracks;
      _title = title;
    }
    
    public List<Track> TrackList
    {
      get
      {
        return _tracks;
      }
    }

    public string Title
    {
      get
      {
        return _title;
      }
    }
  }
}
