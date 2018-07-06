//
// JsonConverter.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TrusonaSDK.HTTP.Client.V2.Serialization
{
  public class RequestResponseJsonConverter : IRequestResponseSerializer
  {
    private static readonly JsonSerializerSettings settings = new JsonSerializerSettings()
    {
      ContractResolver = new RequestResponseContractResolver(),
      Formatting = Formatting.Indented,
      NullValueHandling = NullValueHandling.Ignore
    };

    public RequestResponseJsonConverter() { }

    public static string Serialize(object obj)
    {
      return JsonConvert.SerializeObject(obj, settings);
    }

    public static T Deserialize<T>(string json)
    {
      return JsonConvert.DeserializeObject<T>(json, settings);
    }

    public string SerializeRequest(object obj)
    {
      return Serialize(obj);
    }

    public T DeserializeResponse<T>(string json)
    {
      return Deserialize<T>(json);
    }
  }
}