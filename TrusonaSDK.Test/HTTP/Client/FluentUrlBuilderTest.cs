//
// ServiceUrlBuilderTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using Xunit;
using FluentAssertions;

namespace TrusonaSDK.HTTP.Client
{
  public class FluentUrlBuilderTest
  {
    private readonly FluentUrlBuilder sut;

    public FluentUrlBuilderTest()
    {
      sut = new FluentUrlBuilder(new Uri("https://jones.net"));
    }

    [Fact]
    public void FluentUrlBuilder_should_be_implicitly_convert_to_uri()
    {
      var res = (Uri)sut;
      res.Should()
         .BeOfType<Uri>();
    }

    [Fact]
    public void Build_should_be_valid()
    {
      sut.Build().AbsoluteUri
         .Should()
          .Be("https://jones.net/");
    }

    [Theory]
    [InlineData("tacos")]
    [InlineData("/tacos")]
    public void AppendPath_should_generate_valid_uri(string value)
    {
      sut
        .AppendPath(value)
        .Build().AbsoluteUri
        .Should()
          .Be("https://jones.net/tacos");
    }

    [Theory]
    [InlineData("/tacos", "someId")]
    public void AppendPath_should_generate_valid_uri_when_invoked_multiple_times(string value, string id)
    {
      sut
        .AppendPath(value)
        .AppendPath(id)
        .Build().AbsoluteUri
        .Should()
          .Be("https://jones.net/tacos/someId");
    }

    [Theory]
    [InlineData("/tacos", null)]
    public void AppendPath_should_generate_valid_uri_when_invoked_multiple_times_with_null_value(string value, string id)
    {
      sut
        .AppendPath(value)
        .AppendPath(id)
        .Build().AbsoluteUri
        .Should()
          .Be("https://jones.net/tacos");
    }

    [Theory]
    [InlineData("tacos", "bell")]
    public void AppendQueryParam_should_generate_valid_uri(string key, string value)
    {
      sut
        .AppendQueryParam(key, value)
        .Build().AbsoluteUri
        .Should()
          .Be("https://jones.net/?tacos=bell");
    }
  }
}