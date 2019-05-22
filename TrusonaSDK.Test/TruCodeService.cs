//
// TruCode.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2019 
//
//

using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using TrusonaSDK.HTTP;

namespace TrusonaSDK.Test
{
  public class TruCodeService
  {
    private readonly HttpClient client = new HttpClient();

    public IDictionary<string, string> CreateTruCode()
    {
      var body = new Dictionary<string, string>() {
        { "relying_party_id", "0f0348f0-46d6-47c9-ba4d-2e7cd7f82e3e" }
      };

      var requestBody = new StringContent(
        content: JsonConvert.SerializeObject(body),
        encoding: Encoding.UTF8,
        mediaType: Headers.MEDIA_TYPE_JSON_VALUE
      );

      var response = client.PostAsync("https://api.staging.trusona.net/api/v2/trucodes", requestBody).Result;

      response.EnsureSuccessStatusCode();

      string responseBody = response.Content.ReadAsStringAsync().Result;
      return JsonConvert.DeserializeObject<IDictionary<string, string>>(responseBody);
    }

    public void PairTruCode(string payload, string identifier)
    {
      var body = new Dictionary<string, string>() {
        { "payload", payload },
        { "identifier", identifier }
      };

      var requestBody = new StringContent(
        content: JsonConvert.SerializeObject(body),
        encoding: Encoding.UTF8,
        mediaType: Headers.MEDIA_TYPE_JSON_VALUE
      );

      var response = client.PostAsync("https://api.staging.trusona.net/api/v2/paired_trucodes", requestBody).Result;

      response.EnsureSuccessStatusCode();
    }

  }
}
