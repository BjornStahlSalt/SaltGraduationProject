using Fiddle.Models;
using Fiddle.Exceptions;

namespace Fiddle;

public class FiddleClient 
{
  private readonly HttpClient _httpClient = new HttpClient();

  private const string FIDDLE_BASE_URL = "https://dotnetfiddle.net/Home";

  public async Task<FiddleResponse> Run(string code)
  {
    var payload = new FiddlePayload(code);
    
    HttpResponseMessage response = await _httpClient.PostAsJsonAsync(FIDDLE_BASE_URL + "/Run", payload);
    if(!response.IsSuccessStatusCode)
    {
      throw new FiddleClientError($"Client returned status code {response.StatusCode}");
    }

    var content = await response.Content.ReadFromJsonAsync<FiddleResponse>();
    if(content == null)
    {
      throw new FiddleClientError("Content is null");
    }
    
    return content;
  }
}