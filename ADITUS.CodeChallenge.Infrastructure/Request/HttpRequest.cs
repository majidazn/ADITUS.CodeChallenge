using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace ADITUS.CodeChallenge.Infrastructure.Request
{
  public class HttpRequest
  {
    private static readonly HttpClient client = new HttpClient();
    public HttpRequest()
    {


    }

    public static async Task<T> GetRequestAsync<T>(string url)
    {
      try
      {
        // Send GET request
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        // Read response content as string
        string responseBody = await response.Content.ReadAsStringAsync();

        // Deserialize JSON string to specified type
        var jsonResponse = JsonSerializer.Deserialize<T>(responseBody);

        return jsonResponse;
      }
      catch (HttpRequestException e)
      {
        Console.WriteLine($"Request error: {e.Message}");
        return default(T);
      }
    }
  }
}
