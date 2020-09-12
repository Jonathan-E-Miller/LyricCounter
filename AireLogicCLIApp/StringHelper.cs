using System;
using System.Collections.Generic;
using System.Text;

namespace AireLogicCLIApp
{
  public static class StringHelper
  {
    /// <summary>
    /// Count the number of words in a given string. Special characters are ignored.
    /// White space and a new line triggers an increment in the counter.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static int WordCount(string input)
    {
      int wordCount = 0;
      if (input != null)
      {
        bool inWord = false;
        wordCount = 0;

        foreach (char c in input)
        {
          if ((char.IsWhiteSpace(c) || c.Equals('\n')) && inWord == true)
          {
            wordCount++;
            inWord = false;
          }
          else if (char.IsLetterOrDigit(c))
          {
            inWord = true;
          }
        }
        if (inWord)
        {
          wordCount++;
        }
      }
      return wordCount;
    }
  }
}
