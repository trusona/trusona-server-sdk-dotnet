//
// DeviceServiceIntegrationTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using TrusonaSDK.API;
using Xunit;
using FluentAssertions;

namespace TrusonaSDK.Integration
{
  public class DeviceIntegrationTest : IntegrationTest
  {
    [Fact]
    [Trait("Category", "Integration")]
    public void GetDevice_should_return_a_valid_response()
    {
      //given
      var deviceIdentifier = buster.CreateDevice()["id"];

      //when
      var res = sut.GetDevice(deviceIdentifier).Result;

      //then
      res.Active.Should().BeFalse();
      res.ActivatedAt.Should().BeNull();

      //when
      var activationCode = sut.CreateUserDevice("userId", deviceIdentifier).Result.ActivationCode;
      sut.ActivateUserDevice(activationCode).Wait();
      res = sut.GetDevice(deviceIdentifier).Result;

      //then
      res.Active.Should().BeTrue();
      res.ActivatedAt.Should().BeAfter(DateTime.UtcNow.AddSeconds(-5));
      res.ActivatedAt.Should().BeBefore(DateTime.UtcNow.AddSeconds(5));
    }
  }
}
