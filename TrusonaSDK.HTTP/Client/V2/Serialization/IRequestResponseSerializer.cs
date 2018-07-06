//
// IRequestResponseSerializer.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;

namespace TrusonaSDK.HTTP.Client.V2.Serialization
{
  public interface IRequestResponseSerializer
  {
    string SerializeRequest(object obj);

    T DeserializeResponse<T>(string json);
  }
}