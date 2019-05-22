//
// UserDeviceIntegrationTest.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2019 
//
//
using System;
using FluentAssertions;
using TrusonaSDK.API;
using Xunit;

namespace TrusonaSDK.Test.Integration
{
  public class UserDeviceIntegrationTest : IntegrationServiceTest
  {
    private readonly Buster buster = new Buster();

    [Fact]
    [Trait("Category", "Integration")]
    public void CreateUserDevice_should_bind_a_user_to_an_inactive_device_and_include_an_activation_code()
    {
      //given
      var userIdentifier = "taco";
      var deviceId = buster.CreateDevice()["id"];

      //when
      var userDevice = sut.CreateUserDevice(userIdentifier, deviceId).Result;

      //then
      userDevice.ActivationCode.Should().NotBeNull();
      userDevice.Active.Should().BeFalse();
      userDevice.DeviceIdentifier.Should().Equals(deviceId);
      userDevice.UserIdentifier.Should().Equals(userIdentifier);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void CreateUserDevice_should_throw_device_not_found_exception_if_device_does_not_exist()
    {
      //when
      Action action = () => { sut.CreateUserDevice("taco", Guid.NewGuid().ToString()).Wait(); };

      //then
      action.Should().Throw<DeviceNotFoundException>();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void CreateUserDevice_should_throw_device_already_bound_exception_if_already_bound_to_a_different_identifier()
    {
      //given
      var deviceId = buster.CreateDevice()["id"];
      sut.CreateUserDevice("id1", deviceId).Wait();

      //when
      Action action = () => { sut.CreateUserDevice("id2", deviceId).Wait(); };

      //then
      action.Should().Throw<DeviceAlreadyBoundException>();
    }

  }
}
