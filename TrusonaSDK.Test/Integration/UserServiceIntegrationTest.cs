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
      var deviceIdentifier = "YPhZzRDf9tW0Mtla2rj3NRz2OmYD4k88fiD7t0OFuyQ";
      var binding = sut.CreateUserDevice(userIdentifier, deviceIdentifier).Result;
      sut.ActivateUserDevice(binding.ActivationCode).Wait();

      //when
      Action action = () => { sut.DeleteUser(userIdentifier).Wait(); };

      //then
      action.Should().NotThrow();
    }
  }
}
