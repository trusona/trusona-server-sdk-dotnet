//
// MockedServiceTest.cs
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
using System.Net;
using System.Net.Http;
using Moq;
using TrusonaSDK.HTTP;
using TrusonaSDK.HTTP.Client;
using TrusonaSDK.HTTP.Client.V2.Service;

namespace TrusonaSDK
{
  public abstract class MockedServiceTest<T> where T : HttpService
  {
    private const string validToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJpc3MiOiJ0cnVhZG1pbi5hcGkudHJ1c29uYS5jb20iLCJzdWIiOiIwZjAzNDhmMC00NmQ2LTQ3YzktYmE0ZC0yZTdjZDdmODJlM2UiLCJhdWQiOiJhcGkudHJ1c29uYS5jb20iLCJleHAiOjE1MTk4ODU0OTgsImlhdCI6MTQ4ODMyNzg5OCwianRpIjoiNzg4YWYwNzAtNDBiOS00N2MxLWE3ZmUtOGUwZmE1NWUwMDE1IiwiYXRoIjoiUk9MRV9UUlVTVEVEX1JQX0NMSUVOVCJ9.2FNvjG9yB5DFEcNijk8TryRtKVffiDARRcRIb75Z_Pp85MxW63rhzdLFIN6PtQ1Tzb8lHPPM_4YOe-feeLOzWw";

    private readonly Mock<IEnvironment> _mockEnvironment;
    private readonly Mock<IHttpClientWrapper> _mockHttpClient;

    protected readonly T sut;

    protected IHttpClientWrapper HttpClientWrapper
    {
      get { return _mockHttpClient.Object; }
    }

    protected MockedServiceTest()
    {
      this._mockEnvironment = new Mock<IEnvironment>();
      this._mockHttpClient = new Mock<IHttpClientWrapper>();

      _mockEnvironment.Setup(x => x.EndpointUrl)
                      .Returns(new Uri("https://jones.net"));

      _mockEnvironment.Setup(x => x.CredentialProvider)
                      .Returns(new ApiCredentials(validToken, "jones"));

      this.sut = (T)Activator.CreateInstance(
        typeof(T),
        _mockEnvironment.Object,
        HttpClientWrapper);
    }

    protected void SetupMock(
      string content = @"{}",
      HttpStatusCode statusCode = HttpStatusCode.OK)
    {
      var mockResponseMessage = new HttpResponseMessage()
      {
        StatusCode = statusCode,
        Content = new StringContent(content)
      };

      _mockHttpClient.Setup(x => x.HandleRequest(
        It.IsAny<HttpRequestMessage>(), It.IsAny<ApiCredentials>()))
                     .ReturnsAsync(mockResponseMessage);
    }
  }
}
