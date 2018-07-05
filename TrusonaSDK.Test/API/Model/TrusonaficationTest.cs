//
// TrusonaficationTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using TrusonaSDK.API.Model;
using Xunit;
using FluentAssertions;

namespace TrusonaSDK.API.Model
{
  public class TrusonaficationTest
  {
    [Fact]
    public void Essential_shold_be_valid_with_device_identifier()
    {
      var sut = Trusonafication.Essential()
                               .DeviceIdentifier("someDevice")
                               .Action("poop")
                               .Resource("your pool")
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(2);
      sut.DeviceIdentifier.Should().Be("someDevice");
      sut.UserIdentifier.Should().BeNull();
      sut.Action.Should().Be("poop");
      sut.Resource.Should().Be("your pool");
      sut.Prompt.Should().BeTrue();
      sut.UserPresence.Should().BeTrue();
      sut.ShowIdentityDocument.Should().BeFalse();
    }

    [Fact]
    public void Essential_shold_be_valid_with_user_identifier()
    {
      var sut = Trusonafication.Essential()
                               .UserIdentifier("jones")
                               .Action("poop")
                               .Resource("your pool")
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(2);
      sut.DeviceIdentifier.Should().BeNull();
      sut.UserIdentifier.Should().Be("jones");
      sut.Action.Should().Be("poop");
      sut.Resource.Should().Be("your pool");
      sut.Prompt.Should().BeTrue();
      sut.UserPresence.Should().BeTrue();
      sut.ShowIdentityDocument.Should().BeFalse();
    }

    [Fact]
    public void Essential_shold_be_valid_with_finalize_options()
    {
      var sampleDate = DateTime.Now;
      var sut = Trusonafication.Essential()
                               .UserIdentifier("jones")
                               .Action("poop")
                               .Resource("your pool")
                               .CallbackUrl("https://earl.grey.com")
                               .ExpiresAt(sampleDate)
                               .WithoutPrompt()
                               .WithoutUserPresence()
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(1);
      sut.CallbackUrl.Should().Be("https://earl.grey.com");
      sut.ExpiresAt.Should().Be(sampleDate.ToUniversalTime());
      sut.Prompt.Should().BeFalse();
      sut.UserPresence.Should().BeFalse();
      sut.ShowIdentityDocument.Should().BeFalse();
    }

    [Fact]
    public void Executive_shold_be_valid_with_device_identifier()
    {
      var sut = Trusonafication.Executive()
                               .DeviceIdentifier("someDevice")
                               .Action("poop")
                               .Resource("your pool")
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(3);
      sut.DeviceIdentifier.Should().Be("someDevice");
      sut.UserIdentifier.Should().BeNull();
      sut.Action.Should().Be("poop");
      sut.Resource.Should().Be("your pool");
      sut.Prompt.Should().BeTrue();
      sut.UserPresence.Should().BeTrue();
      sut.ShowIdentityDocument.Should().BeTrue();
    }

    [Fact]
    public void Executive_shold_be_valid_with_user_identifier()
    {
      var sut = Trusonafication.Executive()
                               .UserIdentifier("jones")
                               .Action("poop")
                               .Resource("your pool")
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(3);
      sut.DeviceIdentifier.Should().BeNull();
      sut.UserIdentifier.Should().Be("jones");
      sut.Action.Should().Be("poop");
      sut.Resource.Should().Be("your pool");
      sut.Prompt.Should().BeTrue();
      sut.UserPresence.Should().BeTrue();
      sut.ShowIdentityDocument.Should().BeTrue();
    }

    [Fact]
    public void Executive_shold_be_valid_with_finalize_options()
    {
      var sampleDate = DateTime.Now;
      var sut = Trusonafication.Executive()
                               .UserIdentifier("jones")
                               .Action("poop")
                               .Resource("your pool")
                               .CallbackUrl("https://earl.grey.com")
                               .ExpiresAt(sampleDate)
                               .WithoutPrompt()
                               .WithoutUserPresence()
                               .Build();
      //then
      sut.DesiredLevel.Should().Be(3);
      sut.CallbackUrl.Should().Be("https://earl.grey.com");
      sut.ExpiresAt.Should().Be(sampleDate.ToUniversalTime());
      sut.Prompt.Should().BeFalse();
      sut.UserPresence.Should().BeFalse();
      sut.ShowIdentityDocument.Should().BeTrue();
    }
  }
}
