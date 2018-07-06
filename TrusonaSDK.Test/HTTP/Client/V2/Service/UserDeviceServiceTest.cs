//
// UserDeviceServiceTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using TrusonaSDK.HTTP.Client.V2.Service;
using Xunit;
using FluentAssertions;
using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public class UserDeviceServiceTest : MockedServiceTest<UserDeviceService>
  {

    [Fact]
    public void CreateUserDevice_should_return_a_user_device_response()
    {
      //given
      var userDevice = new UserDeviceRequest() { };

      SetupMock();

      //when
      var res = sut.CreateUserDevice(userDevice);

      //then

      res.Should()
         .BeOfType<UserDeviceResponse>();
    }

    [Fact]
    public void UpdateUserDevice_should_return_an_updated_user_device_response()
    {
      //given
      var deviceId = "01342F41-6694-4AA4-90C1-7BE480E53033";
      var userDevice = new UserDeviceUpdateRequest() { };

      SetupMock(@"{
        ""device_identifier"": ""01342F41-6694-4AA4-90C1-7BE480E53033""
      }");

      //when
      var res = sut.UpdateUserDevice(deviceId, userDevice);

      //then

      res.Should()
         .BeOfType<UserDeviceResponse>();

      res.DeviceIdentifier
         .Should()
         .Be(deviceId);
    }
  }
}