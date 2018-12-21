//
// MockedServiceTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
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

    private readonly Mock<Configuration> _mockEnvironment;
    private readonly Mock<IHttpClientWrapper> _mockHttpClient;

    protected readonly T sut;

    protected IHttpClientWrapper HttpClientWrapper
    {
      get { return _mockHttpClient.Object; }
    }

    protected MockedServiceTest()
    {
      this._mockEnvironment = new Mock<Configuration>();
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
