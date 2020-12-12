//
// InterceptingHttpClientWrapperTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;
using TrusonaSDK.HTTP.Client;
using TrusonaSDK.HTTP.Client.Interceptor;
using Xunit;
using Moq;
using System.Net.Http;

namespace TrusonaSDK.HTTP.Client
{
  public class InterceptingHttpClientWrapperTest
  {
    static readonly List<IHttpInterceptor> interceptors = new List<IHttpInterceptor>();

    readonly InterceptingHttpClientWrapper sut;

    public InterceptingHttpClientWrapperTest() => sut = new InterceptingHttpClientWrapper(interceptors);

    [Fact]
    public void HandleRequest_should_invoke_interceptors()
    {
      //given
      var requestMessage = new HttpRequestMessage()
      {
        Method = HttpMethod.Get,
        RequestUri = new Uri("https://api.staging.trusona.net/")
      };

      var mockInterceptor = new Mock<IHttpInterceptor>();
      interceptors.Add(mockInterceptor.Object);

      //when
      sut.HandleRequest(requestMessage, null)
         .GetAwaiter()
         .GetResult();

      //then
      mockInterceptor.Verify(x => x.InterceptRequest(requestMessage, null), Times.Once());
      mockInterceptor.Verify(x => x.InterceptResponse(It.IsAny<HttpResponseMessage>(), null), Times.Once);
    }
  }
}