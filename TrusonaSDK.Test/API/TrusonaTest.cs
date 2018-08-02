//
// TrusonaTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using TrusonaSDK.API.Model;
using TrusonaSDK.HTTP.Client;
using TrusonaSDK.HTTP;
using System.Net.Http;
using System.Text;
using System.Net;
using Xunit;
using FluentAssertions;
using Moq;
using TrusonaSDK.API.Test;

namespace TrusonaSDK.API
{
  public class TrusonaTest
  {
    #region Mock Setup
    const string validToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJpc3MiOiJ0cnVhZG1pbi5hcGkudHJ1c29uYS5jb20iLCJzdWIiOiIwZjAzNDhmMC00NmQ2LTQ3YzktYmE0ZC0yZTdjZDdmODJlM2UiLCJhdWQiOiJhcGkudHJ1c29uYS5jb20iLCJleHAiOjE1MTk4ODU0OTgsImlhdCI6MTQ4ODMyNzg5OCwianRpIjoiNzg4YWYwNzAtNDBiOS00N2MxLWE3ZmUtOGUwZmE1NWUwMDE1IiwiYXRoIjoiUk9MRV9UUlVTVEVEX1JQX0NMSUVOVCJ9.2FNvjG9yB5DFEcNijk8TryRtKVffiDARRcRIb75Z_Pp85MxW63rhzdLFIN6PtQ1Tzb8lHPPM_4YOe-feeLOzWw";

    readonly Mock<IEnvironment> _mockEnvironment;
    readonly Mock<IHttpClientWrapper> _mockHttpClient;
    readonly ServiceFactory _serviceFactory;

    readonly Trusona sut;

    public TrusonaTest()
    {
      this._mockEnvironment = new Mock<IEnvironment>();

      _mockEnvironment.Setup(x => x.EndpointUrl)
                .Returns(new Uri("https://jones.net"));

      _mockEnvironment.Setup(x => x.CredentialProvider)
                      .Returns(new ApiCredentials(validToken, "jones"));

      this._mockHttpClient = new Mock<IHttpClientWrapper>();
      this._serviceFactory = new ServiceFactory(_mockEnvironment.Object, _mockHttpClient.Object);
      this.sut = TestTrusonaFactory.InjectServiceFactory(_serviceFactory);
    }

    private void SetupMock(
      string content = @"{}",
      HttpStatusCode statusCode = HttpStatusCode.OK)
    {
      var mockResponseMessage = new HttpResponseMessage()
      {
        Content = new StringContent(
          content: content,
          encoding: Encoding.UTF8,
          mediaType: Headers.MEDIA_TYPE_JSON_VALUE
        ),
        StatusCode = statusCode
      };

      _mockHttpClient.Setup(x => x.HandleRequest(
        It.IsAny<HttpRequestMessage>(), It.IsAny<ApiCredentials>()))
                     .ReturnsAsync(mockResponseMessage);
    }

    #endregion

    [Fact]
    public void CreateTrusonafication_should_return_a_result_when_successful()
    {
      //given
      var trusonafication = Trusonafication.Essential()
                                           .DeviceIdentifier("blah")
                                           .Action("poop")
                                           .Resource("your pool")
                                           .Build();

      SetupMock(@"{
        ""id"": ""96ea5830-8e5e-42c5-9cbb-8a941d2ff7f9"",
        ""status"": ""ACCEPTED""
      }");

      //when
      var res = sut.CreateTrusonafication(trusonafication).Result;

      //then
      res.Id.Should()
         .Be(Guid.Parse("96ea5830-8e5e-42c5-9cbb-8a941d2ff7f9"));
      res.Status.Should()
         .Be(TrusonaficationStatus.ACCEPTED);
    }

    [Fact]
    public void CreateTrusonafication_should_handle_exceptions()
    {
      //given
      var trusonafication = Trusonafication.Essential()
                                           .DeviceIdentifier("blah")
                                           .Action("poop")
                                           .Resource("your pool")
                                           .Build();

      SetupMock(string.Empty, HttpStatusCode.BadRequest);

      //when
      Action action = () => { var res = sut.CreateTrusonafication(trusonafication).Result; };

      //then
      action.Should()
        .Throw<TrusonaException>();
    }

    [Fact]
    public void GetTrusonaficationResult_should_return_a_result_when_successful()
    {
      //given
      SetupMock(@"{
        ""id"": ""96ea5830-8e5e-42c5-9cbb-8a941d2ff7f9"",
        ""status"": ""ACCEPTED""
      }");

      //when
      var res = sut.GetTrusonaficationResult(Guid.Parse("96ea5830-8e5e-42c5-9cbb-8a941d2ff7f9")).Result;

      //then
      res.Id.Should()
         .Be(Guid.Parse("96ea5830-8e5e-42c5-9cbb-8a941d2ff7f9"));
      res.Status.Should()
         .Be(TrusonaficationStatus.ACCEPTED);
    }

    [Fact]
    public void GetPairedTruCode_should_return_a_result_when_successful()
    {
      //given
      var trucodeId = Guid.Parse("5C1D16FD-E46F-4866-8A2F-27DEAB3249F4");

      SetupMock(@"{
        ""id"": ""5C1D16FD-E46F-4866-8A2F-27DEAB3249F4"",
        ""identifier"": ""jones""
      }");

      //when
      var res = sut.GetPairedTruCode(trucodeId).Result;

      //then
      res.Should()
        .NotBeNull();

      res.Id.Should()
         .Be(trucodeId);

      res.Identifier.Should()
         .Be("jones");
    }

    [Fact]
    public void CreateUserDevice_should_return_a_result_when_successful()
    {
      //given
      SetupMock(@"{
        ""id"": ""5C1D16FD-E46F-4866-8A2F-27DEAB3249F4"",
        ""user_identifier"": ""jones"",
        ""device_identifier"": ""tacos""
      }");

      //when
      var res = sut.CreateUserDevice("jones", "tacos").Result;

      //then
      res.Id.Should()
         .Be(Guid.Parse("5C1D16FD-E46F-4866-8A2F-27DEAB3249F4"));

      res.ActivationCode.Should()
         .Be("5c1d16fd-e46f-4866-8a2f-27deab3249f4");

      res.UserIdentifier.Should()
         .Be("jones");

      res.DeviceIdentifier.Should()
         .Be("tacos");
    }

    [Fact]
    public void CreateUserDevice_should_handle_conflict_with_exception()
    {
      //given
      SetupMock(@"{}", HttpStatusCode.Conflict);

      //when
      Action action = () => { var res = sut.CreateUserDevice("jones", "tacos").Result; };

      //then
      action.Should()
            .Throw<DeviceAlreadyBoundException>();
    }

    [Fact]
    public void CreateUserDevice_should_handle_failed_dependency_with_exception()
    {
      //given
      SetupMock(@"{}", (HttpStatusCode)424);

      //when
      Action action = () => { var res = sut.CreateUserDevice("jones", "tacos").Result; };

      //then
      action.Should()
            .Throw<DeviceNotFoundException>();
    }

    [Fact]
    public void CreateUserDevice_should_handle_exceptions()
    {
      //given
      SetupMock(@"{}", HttpStatusCode.BadRequest);

      //when
      Action action = () => { var res = sut.CreateUserDevice("jones", "tacos").Result; };

      //then
      action.Should()
            .Throw<TrusonaException>();
    }

    [Fact]
    public void GetWebSdkConfig_should_return_a_result()
    {
      var res = sut.GetWebSdkConfig();

      res.Should()
         .Be(@"{""truCodeUrl"": ""https://jones.net"",""relyingPartyId"": ""0f0348f0-46d6-47c9-ba4d-2e7cd7f82e3e""}");
    }

    [Fact]
    public void GetDevice_should_return_should_return_a_result_when_successful()
    {
      //given
      SetupMock(@"{
        ""activated_at"": ""2018-01-23T23:28:45Z"",
        ""is_active"": true
      }");

      //when
      var res = sut.GetDevice("jones").Result;

      //then
      res.Should()
         .NotBeNull();

      res.Active.Should()
         .BeTrue();
    }

    [Fact]
    public void GetDevice_should_handle_exceptions()
    {
      //given
      SetupMock(@"{}", HttpStatusCode.BadRequest);

      //when
      Action action = () => { var res = sut.GetDevice("jones").Result; };

      //then
      action.Should()
            .Throw<TrusonaException>();
    }

    [Fact]
    public void DeleteUser_should_return_no_content()
    {
      // given
      SetupMock(statusCode: HttpStatusCode.NoContent);

      // when
      Action action = () => { sut.DeleteUser("foo"); };

      // then
      action.Should().NotThrow();
    }

    [Fact]
    public void DeleteUser_should_throw_on_error()
    {
      // given
      SetupMock(statusCode: HttpStatusCode.NotFound);

      // when
      Action action = () => { sut.DeleteUser("foo").Wait(); };

      // then
      action.Should().Throw<TrusonaException>();
    }
  }
}