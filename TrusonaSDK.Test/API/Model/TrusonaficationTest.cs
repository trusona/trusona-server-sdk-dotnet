//
// TrusonaficationTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//       Nikolas Mangu-Thitu <n@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;

using TrusonaSDK.API.Model;
using Xunit;
using FluentAssertions;

namespace TrusonaSDK.API.Model
{
  public class TrusonaficationTest
  {
    [Fact]
    public void Essential_should_be_valid_with_device_identifier()
    {
      var sut = Trusonafication.Essential()
                               .DeviceIdentifier("someDevice")
                               .Action("sit")
                               .Resource("your lap")
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(2);
      sut.DeviceIdentifier.Should().Be("someDevice");
      sut.UserIdentifier.Should().BeNull();
      sut.Action.Should().Be("sit");
      sut.Resource.Should().Be("your lap");
      sut.Prompt.Should().BeTrue();
      sut.UserPresence.Should().BeTrue();
      sut.ShowIdentityDocument.Should().BeFalse();
    }

    [Fact]
    public void Essential_should_be_valid_with_user_identifier()
    {
      var sut = Trusonafication.Essential()
                               .UserIdentifier("jones")
                               .Action("sit")
                               .Resource("your lap")
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(2);
      sut.DeviceIdentifier.Should().BeNull();
      sut.UserIdentifier.Should().Be("jones");
      sut.Action.Should().Be("sit");
      sut.Resource.Should().Be("your lap");
      sut.Prompt.Should().BeTrue();
      sut.UserPresence.Should().BeTrue();
      sut.ShowIdentityDocument.Should().BeFalse();
    }

    [Fact]
    public void Essential_should_be_valid_with_email_address()
    {
      var sut = Trusonafication.Essential()
                               .EmailAddress("jones@taco.com")
                               .Action("sit")
                               .Resource("your lap")
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(2);
      sut.DeviceIdentifier.Should().BeNull();
      sut.EmailAddress.Should().Be("jones@taco.com");
      sut.Action.Should().Be("sit");
      sut.Resource.Should().Be("your lap");
      sut.Prompt.Should().BeTrue();
      sut.UserPresence.Should().BeTrue();
      sut.ShowIdentityDocument.Should().BeFalse();
    }

    [Fact]
    public void Essential_should_be_valid_with_finalize_options()
    {
      var sampleDate = DateTime.Now;
      var sut = Trusonafication.Essential()
                               .UserIdentifier("jones")
                               .Action("sit")
                               .Resource("your lap")
                               .ExpiresAt(sampleDate)
                               .WithoutPrompt()
                               .WithoutUserPresence()
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(1);
      sut.ExpiresAt.Should().Be(sampleDate.ToUniversalTime());
      sut.Prompt.Should().BeFalse();
      sut.UserPresence.Should().BeFalse();
      sut.ShowIdentityDocument.Should().BeFalse();
    }

    [Fact]
    public void Essential_should_be_valid_with_custom_fields()
    {
      var sampleDate = DateTime.Now;

      var sut = Trusonafication.Essential()
                               .UserIdentifier("jones")
                               .Action("sit")
                               .Resource("your lap")
                               .ExpiresAt(sampleDate)
                               .WithoutPrompt()
                               .WithoutUserPresence()
                               .WithCustomFields(new Dictionary<string, object> { { "african", "tiger" }, { "taco", 1 } })
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(1);
      sut.ExpiresAt.Should().Be(sampleDate.ToUniversalTime());
      sut.Prompt.Should().BeFalse();
      sut.UserPresence.Should().BeFalse();
      sut.ShowIdentityDocument.Should().BeFalse();
      sut.CustomFields.GetValueOrDefault("african", null).Should().Be("tiger");
      sut.CustomFields.GetValueOrDefault("taco", null).Should().Be(1);
    }

    [Fact]
    public void Executive_should_be_valid_with_device_identifier()
    {
      var sut = Trusonafication.Executive()
                               .DeviceIdentifier("someDevice")
                               .Action("sit")
                               .Resource("your lap")
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(3);
      sut.DeviceIdentifier.Should().Be("someDevice");
      sut.UserIdentifier.Should().BeNull();
      sut.Action.Should().Be("sit");
      sut.Resource.Should().Be("your lap");
      sut.Prompt.Should().BeTrue();
      sut.UserPresence.Should().BeTrue();
      sut.ShowIdentityDocument.Should().BeTrue();
    }

    [Fact]
    public void Executive_should_be_valid_with_user_identifier()
    {
      var sut = Trusonafication.Executive()
                               .UserIdentifier("jones")
                               .Action("sit")
                               .Resource("your lap")
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(3);
      sut.DeviceIdentifier.Should().BeNull();
      sut.UserIdentifier.Should().Be("jones");
      sut.Action.Should().Be("sit");
      sut.Resource.Should().Be("your lap");
      sut.Prompt.Should().BeTrue();
      sut.UserPresence.Should().BeTrue();
      sut.ShowIdentityDocument.Should().BeTrue();
    }

    [Fact]
    public void Executive_should_be_valid_with_email_address()
    {
      var sut = Trusonafication.Executive()
                               .EmailAddress("jones@taco.com")
                               .Action("sit")
                               .Resource("your lap")
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(3);
      sut.DeviceIdentifier.Should().BeNull();
      sut.EmailAddress.Should().Be("jones@taco.com");
      sut.Action.Should().Be("sit");
      sut.Resource.Should().Be("your lap");
      sut.Prompt.Should().BeTrue();
      sut.UserPresence.Should().BeTrue();
      sut.ShowIdentityDocument.Should().BeTrue();
    }

    [Fact]
    public void Executive_should_be_valid_with_finalize_options()
    {
      var sampleDate = DateTime.Now;
      var sut = Trusonafication.Executive()
                               .UserIdentifier("jones")
                               .Action("sit")
                               .Resource("your lap")
                               .ExpiresAt(sampleDate)
                               .WithoutPrompt()
                               .WithoutUserPresence()
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(3);
      sut.ExpiresAt.Should().Be(sampleDate.ToUniversalTime());
      sut.Prompt.Should().BeFalse();
      sut.UserPresence.Should().BeFalse();
      sut.ShowIdentityDocument.Should().BeTrue();
    }
  }
}
