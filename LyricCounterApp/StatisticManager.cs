using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AireLogicCLIApp
{
  public class StatisticManager
  {
    private Artist _artist;
    private double _average;
    public StatisticManager(Artist artist)
    {
      _artist = artist;
      _average = -1.0;
    }

    /// <summary>
    /// Calculate the average number of words in an artists song
    /// </summary>
    /// <returns>The average</returns>
    public double CalculateAverageNumberOfWordsInSong()
    {
      double accumulation = 0.0;
      double numberOfSongs = 0.0;

      foreach (Album album in _artist.Albums)
      {
        album.TrackList.ForEach(t =>
        {
          if (t.Lyrics != null)
          {
            accumulation += (double)StringHelper.WordCount(t.Lyrics);
            numberOfSongs++;
          }
        });
      }

      double average = (double)accumulation / (double)numberOfSongs;
      _average = average;
      return average;
    }

    /// <summary>
    /// Calculate the Standard Deviation for the artist
    /// </summary>
    /// <returns>The standard deviation</returns>
    public double CalculateStandardDeviation()
    {
      // if we havn't calculated the average yet, then do so.
      _average = _average == -1.0 ? CalculateAverageNumberOfWordsInSong() : _average;
      double population = 0.0;

      // calculate the numerator 
      double numeratorTotal = 0.0;
      foreach (Album album in _artist.Albums)
      {
        foreach(Track t in album.TrackList)
        {
          if (t.Lyrics != null)
          {
            population += 1;

            double numberOfWords = (double)StringHelper.WordCount(t.Lyrics);

            numeratorTotal += Math.Pow(numberOfWords - _average, 2);
          }
        }
      }

      double standardDeviation = Math.Sqrt(numeratorTotal / population);

      return standardDeviation;

    }
  }
}
