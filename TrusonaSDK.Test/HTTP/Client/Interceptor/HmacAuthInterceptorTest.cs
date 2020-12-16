//
// HmacAuthInterceptorTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using Xunit;
using FluentAssertions;
using TrusonaSDK.HTTP.Client.Interceptor;
using System.Net.Http;
using TrusonaSDK.HTTP;
using TrusonaSDK.HTTP.Client.Security;

namespace TrusonaSDK.HTTP.Client.Interceptor
{
  public class HmacAuthInterceptorTest
  {
    HmacAuthInterceptor sut;

    public HmacAuthInterceptorTest() => sut = new HmacAuthInterceptor();

    [Fact]
    public void InterceptRequest_should_add_hmac_headers_when_auth_is_not_present()
    {
      //given
      var message = new HttpRequestMessage();

      //when
      sut.InterceptRequest(
        message: message,
        credentialProvider: null
      );

      message.Headers.Authorization
             .Should()
             .BeNull();

      message.Headers.Contains(Headers.X_SIGNATURE)
             .Should()
             .BeFalse();
      message.Headers.Contains(Headers.X_DATE)
             .Should()
             .BeFalse();
    }

    [Fact]
    public void InterceptRequest_should_add_hmac_headers_when_auth_is_present()
    {
      //given
      var message = new HttpRequestMessage();
      var credentials = new ApiCredentials(token: "someToken", secret: "someSecret");

      //when
      sut.InterceptRequest(
        message: message,
        credentialProvider: credentials
      );

      //then
      message.Headers.Authorization.ToString()
             .Should()
             .StartWith("TRUSONA");

      message.Headers.Contains(Headers.X_DATE)
             .Should()
             .BeTrue();
    }

    [Fact]
    public void InterceptResponse_should_not_validate_response_signature_when_auth_is_not_present()
    {
      //given
      var message = new HttpResponseMessage();

      //when
      Action act = () => sut.InterceptResponse(message, null);

      //then
      act
        .Should()
        .NotThrow();
    }

    [Fact]
    public void InterceptResponse_should_validate_response_signature_when_auth_is_present()
    {
      //given
      var message = new HttpResponseMessage();
      message.Headers.Add(Headers.X_SIGNATURE, "MjBmYmFkMzI3NTA2MWE3N2ViZDA1ZGFmNTU4NWMyYWIyZjVlNDY0NzIyNDVmNGJkZTdjMThhNDgwNzBmYjg2Mg==");
      var credentials = new ApiCredentials("jones", "tacos");

      //when
      Action act = () => sut.InterceptResponse(message, credentials);

      //then
      act
        .Should()
        .NotThrow();
    }

    [Fact]
    public void InterceptResponse_should_throw_exception_if_signature_is_present_but_credentials_are_absent()
    {
      //given
      var message = new HttpResponseMessage();
      message.Headers.Add(Headers.X_SIGNATURE, "MjBmYmFkMzI3NTA2MWE3N2ViZDA1ZGFmNTU4NWMyYWIyZjVlNDY0NzIyNDVmNGJkZTdjMThhNDgwNzBmYjg2Mg==");

      //when
      Action act = () => sut.InterceptResponse(message, null);

      //then
      act
        .Should()
        .Throw<HmacSignatureException>();
    }

    [Fact]
    public void InterceptResponse_should_raise_exception_when_signature_is_invalid()
    {
      //given
      var message = new HttpResponseMessage()
      {
        Content = new StringContent("JONES")
      };

      message.Headers.Add(Headers.X_SIGNATURE, "MjBmYmFkMzI3NTA2MWE3N2ViZDA1ZGFmNTU4NWMyYWIyZjVlNDY0NzIyNDVmNGJkZTdjMThhNDgwNzBmYjg2Mg==");
      var credentials = new ApiCredentials("jones", "tacos");

      //when
      Action act = () => sut.InterceptResponse(message, credentials);

      //then
      act
        .Should()
        .Throw<HmacSignatureException>();
    }
  }
}