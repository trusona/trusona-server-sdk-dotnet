//
// ResponseHmacMessageTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Net.Http;
using System.Text;
using TrusonaSDK.HTTP.Client.Security;
using Xunit;
using FluentAssertions;
using System.Net;
using TrusonaSDK.HTTP;

namespace TrusonaSDK.HTTP.Client.Security
{
  public class ResponseHmacMessageTest
  {
    readonly HttpRequestMessage requestMessage;
    readonly HttpResponseMessage responseMessage;
    readonly IHmacMessage sut;

    public ResponseHmacMessageTest()
    {
      requestMessage = new HttpRequestMessage()
      {
        RequestUri = new Uri("http://jones.net/bar?fizz=buzz"),
        Method = HttpMethod.Post,
      };

      responseMessage = new HttpResponseMessage()
      {
        RequestMessage = requestMessage,
        Content = new StringContent(
          content: @"{""foo"":""bar""}",
          mediaType: "application/json",
          encoding: Encoding.UTF8
        ),
        StatusCode = HttpStatusCode.OK,
      };

      responseMessage.Headers.Add(Headers.X_DATE, "01/02/2018T12:23:33Z");

      sut = new ResponseHmacMessage(responseMessage);
    }

    [Fact]
    public void BodyDigest_should_return_the_md5_of_the_body_of_the_response()
    {
      sut.BodyDigest
         .Should()
         .Be("9bb58f26192e4ba00f01e2e7b136bbd8");
    }

    [Fact]
    public void BodyDigest_should_return_the_md5_of_empty_string_when_there_is_no_body()
    {
      responseMessage.Content = null;
      sut.BodyDigest
         .Should()
         .Be("d41d8cd98f00b204e9800998ecf8427e");
    }

    [Fact]
    public void ContentType_should_return_the_content_type_of_the_response()
    {
      sut.ContentType
         .Should()
         .Be("application/json;charset=utf-8");
    }

    [Fact]
    public void Date_should_return_the_value_of_the_date_header()
    {
      sut.Date
         .Should()
         .Be("01/02/2018T12:23:33Z");
    }

    [Fact]
    public void Method_should_return_the_http_method_of_the_request()
    {
      sut.Method
         .Should()
         .Be("POST");
    }

    [Fact]
    public void RequestUri_should_return_the_uri_of_the_request()
    {
      sut.RequestUri
         .Should()
         .Be("/bar?fizz=buzz");
    }
  }
}