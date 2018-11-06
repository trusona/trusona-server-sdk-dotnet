//
// TrusonaficationServiceIntegrationTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using Xunit;
using FluentAssertions;
using TrusonaSDK.API;
using TrusonaSDK.API.Model;

namespace TrusonaSDK.Integration
{
  public class TrusonaficationServiceIntegrationTest : IntegrationServiceTest
  {
    //[Fact]
    [Trait("Category", "Integration")]
    public void CreateTrusonafication_should_return_a_valid_resonse()
    {
      //given
      var trusonafication = Trusonafication.Essential()
                                           .DeviceIdentifier("Z-GgoO2julAOEjJ2KqH34K24B0m-K6Rvx0uQqgv2nxU")
                                           .Action("sit")
                                           .Resource("your lap")
                                           .ExpiresAt(DateTime.Now.AddSeconds(10))
                                           .Build();

      //when
      var res = sut.CreateTrusonafication(trusonafication).Result;

      //then
      res.Status
         .Should()
         .Be(TrusonaficationStatus.IN_PROGRESS);
    }

    //[Fact]
    [Trait("Category", "Integration")]
    public void GetTrusonaficationResult_should_return_a_valid_response()
    {
      //given
      var trusonafication = Trusonafication.Essential()
                                           .DeviceIdentifier("Z-GgoO2julAOEjJ2KqH34K24B0m-K6Rvx0uQqgv2nxU")
                                           .Action("sit")
                                           .Resource("your lap")
                                           .ExpiresAt(DateTime.Now.AddSeconds(1))
                                           .Build();

      var trusonaficationId = sut.CreateTrusonafication(trusonafication).Result.Id;

      //when
      var res = sut.GetTrusonaficationResult(trusonaficationId).Result;

      res.Status
         .Should()
         .Be(TrusonaficationStatus.EXPIRED);
    }
  }
}