using System.Net;
using TrusonaSDK.HTTP.Client.V2.Service;
using Xunit;

namespace TrusonaSDK.Test.HTTP.Client.V2.Service
{
  public class UserServiceTest : MockedServiceTest<UserService>
  {
    [Fact]
    public void DeleteShouldSucceed()
    {
      //given
      SetupMock(statusCode: HttpStatusCode.NoContent);

      //when
      sut.DeleteUserAsync("foo").Wait();

    }
  }
}
