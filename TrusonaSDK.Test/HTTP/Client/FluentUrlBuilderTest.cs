//
// ServiceUrlBuilderTest.cs
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