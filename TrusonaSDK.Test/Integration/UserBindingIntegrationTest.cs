﻿//
// UserBindingIntegrationTest.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2019 
//
//
using FluentAssertions;
using System;
using TrusonaSDK.API;
using TrusonaSDK.Test;
using Xunit;

namespace TrusonaSDK.Test.Integration
{
  public class UserBindingIntegrationTest : IntegrationServiceTest
  {

    // Uses historical UAT data where there is a user with Trusona ID = 699882827 and TruBank ID = taco

    [Fact]
    [Trait("Category", "Integration")]
    public void CreateUserBinding_should_be_able_to_create_binding_when_trucode_paired_with_trusona_id()
    {
      //given
      var truCodeIdentifier = "trusonaId:699882827";
      var userIdentifier = "taco";

      var truCodeService = new TruCodeService();
      var truCode = truCodeService.CreateTruCode();
      truCodeService.PairTruCode(truCode["payload"], truCodeIdentifier);

      //when
      Action action = () => { sut.CreateUserBinding(userIdentifier, truCode["id"]).Wait(); };

      //then
      action.Should().NotThrow();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void CreateUserBinding_should_be_able_to_create_binding_when_trucode_paired_with_device_identifier()
    {
      //given
      var truCodeIdentifier = "tWTwlYLmffwSrXXKHcmA9kSQd0jMoQKislFsAKhX8DI";
      var userIdentifier = "taco";

      var truCodeService = new TruCodeService();
      var truCode = truCodeService.CreateTruCode();
      truCodeService.PairTruCode(truCode["payload"], truCodeIdentifier);

      //when
      Action action = () => { sut.CreateUserBinding(userIdentifier, truCode["id"]).Wait(); };

      //then
      action.Should().NotThrow();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void CreateUserBinding_should_throw_validation_exception_if_user_identifier_is_blank()
    {
      //given
      var truCodeIdentifier = "tWTwlYLmffwSrXXKHcmA9kSQd0jMoQKislFsAKhX8DI";

      var truCodeService = new TruCodeService();
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
    public void CreateUserBinding_should_throw_user_already_bound_exception_if_user_identifier_is_different()
    {
      //given
      var truCodeIdentifier = "tWTwlYLmffwSrXXKHcmA9kSQd0jMoQKislFsAKhX8DI";
      var userIdentifier = "tacoshrimp";

      var truCodeService = new TruCodeService();
      var truCode = truCodeService.CreateTruCode();
      truCodeService.PairTruCode(truCode["payload"], truCodeIdentifier);

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

      var truCodeService = new TruCodeService();
      var truCode = truCodeService.CreateTruCode();
      //truCodeService.PairTruCode(truCode["payload"], truCodeIdentifier);

      //when
      Action action = () => { sut.CreateUserBinding(userIdentifier, truCode["id"]).Wait(); };

      //then
      action.Should().NotThrow();
    }
  }
}