//
// RequestResponseSpec.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using Xunit;
using FluentAssertions;
using TrusonaSDK.HTTP.Client.V2;
using TrusonaSDK.HTTP.Client.V2.Serialization;

namespace TrusonaSDK
{
  public abstract class RequestResponseTest<T> where T : BaseRequestResponse
  {
    public abstract T Sut { get; }
    public abstract string Json { get; }

    const string whitespace = " ";

    [Fact]
    public virtual void _should_be_serializable()
    {
      var res = RequestResponseJsonConverter.Serialize(Sut);
      res.Should()
         .BeEquivalentTo(Json);
    }

    [Fact]
    public virtual void _should_be_deserializable()
    {
      var res = RequestResponseJsonConverter.Deserialize<T>(Json);
      res.Should()
         .BeEquivalentTo(Sut);
    }
  }
}