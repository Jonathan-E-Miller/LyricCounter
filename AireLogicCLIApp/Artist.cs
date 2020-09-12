using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AireLogicCLIApp
{
  public class Artist
  {
    public string Name { get; set; }
    public List<Album> Albums { get; set; }


    public Artist()
    {
      Albums = new List<Album>();
    }
  }
}
