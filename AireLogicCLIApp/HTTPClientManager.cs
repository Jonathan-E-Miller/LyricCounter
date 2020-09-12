using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AireLogicCLIApp
{
  public class HTTPClientManager
  {
    private HttpClient _httpClient;
    private int _delay;

    /// <summary>
    /// Constructor to generate HTTPClientManager object
    /// </summary>
    /// <param name="baseAddress">The base address of the end point</param>
    /// <param name="delay">Required delay in ms</param>
    public HTTPClientManager(string baseAddress, int? delayMs)
    {
      _httpClient = new HttpClient
      {
        BaseAddress = new Uri(baseAddress)
      };

      // set application User-Agent and set to Json.
      _httpClient.DefaultRequestHeaders.Add("User-Agent", "C# App/v0.1 (mailto:drjmiller1992@gmail.com)");
      _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");

      // delayMs is optional so check we have a value.
      if (delayMs.HasValue)
      {
        // set the delay member
        _delay = delayMs.Value;
      }
      else
      {
        // default to zero.
        _delay = 0;
      }
    }

    /// <summary>
    /// Make a Web Request and return the Json String.
    /// Will return null if invalid or error.
    /// </summary>
    /// <param name="endpoint">The end point to call the request on</param>
    /// <returns></returns>
    public async Task<string> GetJsonResponse(string endpoint)
    {
      string responseData = null;
      using (var response = await _httpClient.GetAsync(endpoint))
      {
        if (response.IsSuccessStatusCode)
        {
          responseData = await response.Content.ReadAsStringAsync();
        }
      }

      if (_delay > 0)
      {
        await Task.Delay(_delay);
      }
      return responseData;
    }
  }
}
