//
// DeviceServiceTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using Xunit;
using FluentAssertions;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public class DeviceServiceTest : MockedServiceTest<DeviceService>
  {
    [Fact]
    public void GetDevice_should_return_a_device_response()
    {
      //given
      SetupMock();

      //when
      var res = sut.GetDevice("jonesTacos");

      //then
      res.Should()
         .BeOfType<DeviceResponse>();
    }
  }
}