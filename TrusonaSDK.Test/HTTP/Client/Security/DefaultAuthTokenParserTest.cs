﻿//
// DefaultAuthTokenParserTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using Xunit;
using TrusonaSDK.HTTP.Client.Security;
namespace TrusonaSDK.HTTP.Security
{
  public class DefaultAuthTokenParserTest
  {
    IAuthTokenParser sut;

    public DefaultAuthTokenParserTest() => sut = new DefaultAuthTokenParser();

    [Fact]
    public void ParseToken_should_return_null_when_token_is_invalid()
    {
      //when
      var res = sut.ParseToken("asdf");

      //then
      Assert.Null(res);
    }

    [Fact]
    public void ParseToken_should_return_a_parsed_token()
    {
      //given
      var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiJ9.eyJpc3MiOiJ0cnVhZG1pbi5hcGkudHJ1c29uYS5jb20iLCJzdWIiOiIwZjAzNDhmMC00NmQ2LTQ3YzktYmE0ZC0yZTdjZDdmODJlM2UiLCJhdWQiOiJhcGkudHJ1c29uYS5jb20iLCJleHAiOjE1MTk4ODU0OTgsImlhdCI6MTQ4ODMyNzg5OCwianRpIjoiNzg4YWYwNzAtNDBiOS00N2MxLWE3ZmUtOGUwZmE1NWUwMDE1IiwiYXRoIjoiUk9MRV9UUlVTVEVEX1JQX0NMSUVOVCJ9.2FNvjG9yB5DFEcNijk8TryRtKVffiDARRcRIb75Z_Pp85MxW63rhzdLFIN6PtQ1Tzb8lHPPM_4YOe-feeLOzWw";

      //when
      var res = sut.ParseToken(token);

      //then
      Assert.Equal(Guid.Parse("788af070-40b9-47c1-a7fe-8e0fa55e0015"), res.Id);
      Assert.Equal("ROLE_TRUSTED_RP_CLIENT", res.Authorities);
      Assert.Equal("api.trusona.com", res.Audience);
      Assert.Equal(1488327898, res.IssuedAt);
      Assert.Equal(1519885498, res.ExpiresAt);
      Assert.Equal("truadmin.api.trusona.com", res.Issuer);
      Assert.Equal(Guid.Parse("0f0348f0-46d6-47c9-ba4d-2e7cd7f82e3e"), res.Subject);
    }
  }
}