//
// HmacAuthInterceptorTest.cs
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