//
// TruCodeServiceTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using Xunit;
using FluentAssertions;
using TrusonaSDK.HTTP.Client.V2.Service;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public class TruCodeServiceTest : MockedServiceTest<TruCodeService>
  {
    [Fact]
    public void GetTruCodeResponse_should_return_a_trucode_response()
    {
      //given
      var trucodeId = Guid.Parse("2C455E1A-DD21-46AE-B457-815A5CA0C66E");

      SetupMock(@"{
        ""id"": ""2C455E1A-DD21-46AE-B457-815A5CA0C66E""
      }");

      //when
      var res = sut.GetPairedTrucode(trucodeId);

      //then
      res
        .Should()
        .BeOfType<TruCodeResponse>();

      res.Id
         .Should()
         .Be(trucodeId);
    }
  }
}