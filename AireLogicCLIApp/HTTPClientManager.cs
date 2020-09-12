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

    /// <summary>
    /// Constructor to generate HTTPClientManager object
    /// </summary>
    /// <param name="baseAddress">The base address of the end point</param>
    public HTTPClientManager(string baseAddress)
    {
      _httpClient = new HttpClient
      {
        BaseAddress = new Uri(baseAddress)
      };

      // set application User-Agent and set to Json.
      _httpClient.DefaultRequestHeaders.Add("User-Agent", "C# App/v0.1 (mailto:drjmiller1992@gmail.com)");
      _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
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
        responseData = await response.Content.ReadAsStringAsync();
      }
      return responseData;
    }
  }
}
