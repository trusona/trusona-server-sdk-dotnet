//
// UserServiceIntegrationTest.cs
//
// Author:
//       alwold <>
//
// Copyright (c) 2018 
//
//
using System;
using Xunit;
using TrusonaSDK.API;
using FluentAssertions;

namespace TrusonaSDK.Test.Integration
{
  public class UserServiceIntegrationTest : IntegrationServiceTest
  {
    //[Fact]
    [Trait("Category", "Integration")]
    public void DeleteUser_should_return_a_valid_response()
    {
      //given
      var userIdentifier = "abc123";
      var deviceIdentifier = "kC_9iF_CNcJqdU4PvJspx6okdQnxJsYNteL0EJG_O-c";
      sut.CreateUserDevice(userIdentifier, deviceIdentifier).Wait();

      //when
      Action action = () => { sut.DeleteUser(userIdentifier).Wait(); };

      //then
      action.Should().NotThrow();
    }
  }
}
