//
// Buster.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2019 
//
//
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using TrusonaSDK.HTTP;

namespace TrusonaSDK.Test
{
  public class Buster
  {
    private readonly HttpClient client = new HttpClient();
    private readonly string busterUrl = "https://buster.staging.trusona.net";

    public Buster()
    {
      var environment = Environment.GetEnvironmentVariables();
      var busterUsername = (string) environment["BUSTER_USERNAME"];
      var busterPassword = (string) environment["BUSTER_PASSWORD"];

      var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", busterUsername, busterPassword));
      client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
    }

    public IDictionary<string, string> CreateDevice()
    {
      var body = new Dictionary<string, string>() {
        { "relying_party_id", "0f0348f0-46d6-47c9-ba4d-2e7cd7f82e3e" }
      };

      var requestBody = new StringContent(
        content: JsonConvert.SerializeObject(body),
        encoding: Encoding.UTF8,
        mediaType: Headers.MEDIA_TYPE_JSON_VALUE
      );

      var response = client.PostAsync($"{busterUrl}/faux_devices", requestBody).Result;

      response.EnsureSuccessStatusCode();

      string responseBody = response.Content.ReadAsStringAsync().Result;
      return JsonConvert.DeserializeObject<IDictionary<string, string>>(responseBody);
    }

    public IDictionary<string, string> SyncDevice(string id)
    {
      var response = client.GetAsync($"{busterUrl}/faux_devices/{id}").Result;

      response.EnsureSuccessStatusCode();

      string responseBody = response.Content.ReadAsStringAsync().Result;
      return JsonConvert.DeserializeObject<IDictionary<string, string>>(responseBody);
    }

    public IDictionary<string, string> AcceptTrusonafication(string id, string trusonaficationId)
    {
      var body = new Dictionary<string, string>() {
        { "trusonafication_id", trusonaficationId }
      };

      var requestBody = new StringContent(
        content: JsonConvert.SerializeObject(body),
        encoding: Encoding.UTF8,
        mediaType: Headers.MEDIA_TYPE_JSON_VALUE
      );

      var response = client.PostAsync($"{busterUrl}/faux_devices/{id}/trusonafication_responses", requestBody).Result;

      response.EnsureSuccessStatusCode();

      string responseBody = response.Content.ReadAsStringAsync().Result;
      return JsonConvert.DeserializeObject<IDictionary<string, string>>(responseBody);
    }

    public string GetCallbackUrl(string id)
    {
      return $"{busterUrl}/callbacks/{id}";
    }

    public IDictionary<string, object> GetCallback(string id)
    {
      var response = client.GetAsync($"{busterUrl}/callbacks/{id}").Result;
      string responseBody = response.Content.ReadAsStringAsync().Result;
      return JsonConvert.DeserializeObject<IDictionary<string, object>>(responseBody);

    }
  }
}
