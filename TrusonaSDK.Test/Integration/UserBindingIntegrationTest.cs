//
// UserBindingIntegrationTest.cs
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
  public class UserBindingIntegrationTest : IntegrationTest
  {
    private readonly TruCodeService truCodeService = new TruCodeService();

    [Fact]
    [Trait("Category", "Integration")]
    public void CreateUserBinding_should_throw_validation_exception_if_user_identifier_is_blank()
    {
      //given
      var truCodeIdentifier = "tWTwlYLmffwSrXXKHcmA9kSQd0jMoQKislFsAKhX8DI";

      var truCode = truCodeService.CreateTruCode();
      truCodeService.PairTruCode(truCode["payload"], truCodeIdentifier);

      //when
      Action action = () => { sut.CreateUserBinding("", truCode["id"]).Wait(); };

      //then
      action.Should().Throw<ValidationException>();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void CreateUserBinding_should_throw_validation_exception_if_trucode_id_is_blank()
    {
      //given
      var userIdentifier = "taco";

      //when
      Action action = () => { sut.CreateUserBinding(userIdentifier, "").Wait(); };

      //then
      action.Should().Throw<ValidationException>();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void CreateUserBinding_should_throw_user_already_bound_exception_if_user_already_has_binding()
    {
      //given
      var userIdentifier = "taco";
      var deviceId = buster.CreateDevice()["id"];
      var deviceBinding = sut.CreateUserDevice(userIdentifier, deviceId).Result;
      sut.ActivateUserDevice(deviceBinding.ActivationCode).Wait();

      var truCode = truCodeService.CreateTruCode();
      truCodeService.PairTruCode(truCode["payload"], deviceId);

      //when
      Action action = () => { sut.CreateUserBinding(userIdentifier, truCode["id"]).Wait(); };

      //then
      action.Should().Throw<UserAlreadyBoundException>();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void CreateUserBinding_should_throw_trucode_not_paried_exception_when_trucode_is_not_paired()
    {
      //given
      var userIdentifier = "taco";

      var truCode = truCodeService.CreateTruCode();
      //truCodeService.PairTruCode(truCode["payload"], truCodeIdentifier);

      //when
      Action action = () => { sut.CreateUserBinding(userIdentifier, truCode["id"]).Wait(); };

      //then
      action.Should().Throw<TruCodeNotPairedException>();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void CreateUserBinding_should_throw_trucode_not_paried_exception_when_trucode_is_paired_by_non_tilted_device()
    {
      //given
      var truCodeIdentifier = "trusonaId:000000000"; // User won't exist
      var userIdentifier = "taco";

      var truCode = truCodeService.CreateTruCode();
      truCodeService.PairTruCode(truCode["payload"], truCodeIdentifier);

      //when
      Action action = () => { sut.CreateUserBinding(userIdentifier, truCode["id"]).Wait(); };

      //then
      action.Should().Throw<TruCodeNotPairedException>();
    }

    // Couldn't test the last two scenarios using Buster, had to use my own trusonaId and deviceIdentifier
    // in UAT and delete my bindings before running each one separately

    //[Fact]
    [Trait("Category", "Integration")]
    public void CreateUserBinding_should_be_able_to_create_binding_when_trucode_paired_with_trusona_id()
    {
      //given
      var truCodeIdentifier = "trusonaId:386505723";
      var userIdentifier = "tacoshrimp";

      var truCode = truCodeService.CreateTruCode();
      truCodeService.PairTruCode(truCode["payload"], truCodeIdentifier);

      //when
      Action action = () => { sut.CreateUserBinding(userIdentifier, truCode["id"]).Wait(); };

      //then
      action.Should().NotThrow();
    }

    //[Fact]
    [Trait("Category", "Integration")]
    public void CreateUserBinding_should_be_able_to_create_binding_when_trucode_paired_with_device_identifier()
    {
      //given
      var truCodeIdentifier = "-l46fx7MLjckyH63X4OGgxSmbwCVH9p6h_6pkGKap7A";
      var userIdentifier = "tacoshrimp";

      var truCode = truCodeService.CreateTruCode();
      truCodeService.PairTruCode(truCode["payload"], truCodeIdentifier);

      //when
      Action action = () => { sut.CreateUserBinding(userIdentifier, truCode["id"]).Wait(); };

      //then
      action.Should().NotThrow();
    }
  }
}
