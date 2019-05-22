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
  public class TrusonaficationIntegrationTest : IntegrationTest
  {
    [Fact]
    [Trait("Category", "Integration")]
    public void CreateTrusonafication_should_create_trusonafications_by_device_identifier()
    {
      //given
      var deviceIdentifier = buster.CreateDevice()["id"];
      var binding = sut.CreateUserDevice("beefstew", deviceIdentifier).Result;
      sut.ActivateUserDevice(binding.ActivationCode).Wait();

      var trusonafication = Trusonafication.Essential()
                                           .DeviceIdentifier(deviceIdentifier)
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

    [Fact]
    [Trait("Category", "Integration")]
    public void CreateTrusonafication_should_create_trusonafications_by_user_identifier()
    {
      //given
      var userIdentifier = "TacoMan3000";
      var deviceIdentifier = buster.CreateDevice()["id"];
      var binding = sut.CreateUserDevice(userIdentifier, deviceIdentifier).Result;
      sut.ActivateUserDevice(binding.ActivationCode).Wait();

      var trusonafication = Trusonafication.Essential()
                                           .UserIdentifier(userIdentifier)
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

    [Fact]
    [Trait("Category", "Integration")]
    public void GetTrusonaficationResult_should_return_a_valid_response()
    {
      var deviceIdentifier = buster.CreateDevice()["id"];
      var binding = sut.CreateUserDevice("TacoMan3000", deviceIdentifier).Result;
      sut.ActivateUserDevice(binding.ActivationCode).Wait();

      //given
      var trusonafication = Trusonafication.Essential()
                                           .DeviceIdentifier(deviceIdentifier)
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

    [Fact]
    [Trait("Category", "Integration")]
    public void GetTrusonaficationResult_of_an_accepted_trusonafication_should_include_the_bound_identifier()
    {
      //given
      var userIdentifier = "TacoMan3000";
      var deviceIdentifier = buster.CreateDevice()["id"];
      var binding = sut.CreateUserDevice(userIdentifier, deviceIdentifier).Result;
      sut.ActivateUserDevice(binding.ActivationCode).Wait();

      //given
      var trusonafication = Trusonafication.Essential()
                                           .DeviceIdentifier(deviceIdentifier)
                                           .Action("sit")
                                           .Resource("your lap")
                                           .Build();

      var trusonaficationId = sut.CreateTrusonafication(trusonafication).Result.Id;
      buster.AcceptTrusonafication(deviceIdentifier, trusonaficationId.ToString());

      //when
      var res = sut.GetTrusonaficationResult(trusonaficationId).Result;

      res.Status.Should()
         .Be(TrusonaficationStatus.ACCEPTED);
      res.BoundUserIdentifier.Should()
         .Be(userIdentifier);
    }

  }
}