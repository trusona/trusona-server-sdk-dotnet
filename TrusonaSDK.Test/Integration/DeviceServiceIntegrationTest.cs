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
  public class DeviceServiceIntegrationTest : IntegrationServiceTest
  {
    [Fact]
    [Trait("Category", "Integration")]
    public void GetDevice_should_return_a_valid_response()
    {
      //given
      var deviceIdentifier = "kC_9iF_CNcJqdU4PvJspx6okdQnxJsYNteL0EJG_O-c";

      //when
      var res = sut.GetDevice(deviceIdentifier).Result;

      //then
      res.Active
         .Should()
         .BeTrue();
      res.ActivatedAt.Value
         .Should()
         .Be(DateTime.Parse("2018-01-12 21:36:17.833"));
    }
  }
}
