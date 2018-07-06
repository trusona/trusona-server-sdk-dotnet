//
// TrusonaficationServiceTest.cs
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
  public class TrusonaficationServiceTest : MockedServiceTest<TrusonaficationService>
  {
    [Fact]
    public void CreateTrusonafication_should_return_a_trusonafication_response()
    {
      //given
      SetupMock();

      //when
      var res = sut.CreateTrusonafication(new TrusonaficationRequest());

      //then
      res.Should()
         .BeOfType<TrusonaficationResponse>();
    }

    [Fact]
    public void GetTrusonafication_should_return_a_trusonafication_response()
    {
      //given
      SetupMock();

      //when
      var res = sut.GetTrusonafication(Guid.NewGuid());

      //then
      res.Should()
         .BeOfType<TrusonaficationResponse>();
    }
  }
}