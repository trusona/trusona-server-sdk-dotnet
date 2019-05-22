//
// UserBindingServiceTest.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2019 
//
//
using Moq;
using System.Net;
using System.Net.Http;
using TrusonaSDK.HTTP;
using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Service;
using Xunit;

namespace TrusonaSDK.Test.HTTP.Client.V2.Service
{
  public class UserBindingServiceTest : MockedServiceTest<UserBindingService>
  {

    [Fact]
    public void CreateUserBinding_should_send_a_post_to_api_v2_user_bindings()
    {
      //given
      var userBinding = new UserBindingRequest()
      {
        TruCodeId = System.Guid.NewGuid().ToString(),
        UserIdentifier = "taco123"
      };

      SetupMock(statusCode: HttpStatusCode.NoContent);

      //when
      sut.CreateUserBinding(userBinding);

      //then
      MockHttpClient.Verify(x => x.HandleRequest(
        It.Is<HttpRequestMessage>(req =>
          req.Method == HttpMethod.Post
          && req.RequestUri == new System.Uri("https://jones.net/api/v2/user_bindings")
          //&& req.Content.ToString().Contains(userBinding.TruCodeId.ToString())
        ),
        It.IsAny<ApiCredentials>()
      ), Times.Exactly(1));
    }
  }
}
