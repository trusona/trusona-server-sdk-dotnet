//
// OrderedContractResolver.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace TrusonaSDK.HTTP.Client.V2.Serialization
{
  internal class RequestResponseContractResolver : DefaultContractResolver
  {
    public RequestResponseContractResolver()
    {
      NamingStrategy = new SnakeCaseNamingStrategy();
    }

    protected override IList<JsonProperty> CreateProperties(
      Type type, MemberSerialization memberSerialization)
    {
      return base.CreateProperties(type, memberSerialization)
                 .OrderBy(p => p.PropertyName)
                 .ToList();
    }
  }
}