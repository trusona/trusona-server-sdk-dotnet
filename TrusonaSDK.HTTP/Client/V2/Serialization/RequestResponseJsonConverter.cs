//
// JsonConverter.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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