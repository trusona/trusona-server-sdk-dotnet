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
  public class UserIntegrationTest : IntegrationTest
  {
    [Fact]
    [Trait("Category", "Integration")]
    public void DeleteUser_should_return_a_valid_response()
    {
      //given
      var deviceIdentifier = buster.CreateDevice()["id"];
      var userIdentifier = "abc123";
      var binding = sut.CreateUserDevice(userIdentifier, deviceIdentifier).Result;
      sut.ActivateUserDevice(binding.ActivationCode).Wait();

      //when
      sut.DeleteUser(userIdentifier).Wait();

      //then
      sut.GetDevice(deviceIdentifier).Result.Active.Should().BeFalse();
    }
  }
}
