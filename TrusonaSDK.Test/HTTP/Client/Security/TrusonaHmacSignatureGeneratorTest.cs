//
// DefaultHmacSignatureGeneratorTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using Xunit;
using TrusonaSDK.HTTP.Client.Security;
using Moq;
using FluentAssertions;
namespace TrusonaSDK.HTTP.Client.Security
{
  public class TrusonaHmacSignatureGeneratorTest
  {
    const string secret = "7f1dd753b6fa473d07c99b56d43bd5da3cd928487d5022e1810fab96c70945b01ad2603585542d33a1383b1f14b5880373474ff40c76a38df19052cefeb3a3eb";
    IHmacSignatureGenerator sut;

    public TrusonaHmacSignatureGeneratorTest() => sut = new TrusonaHmacSignatureGenerator(secret);

    [Fact]
    public void GetSignature_should_return_a_signature()
    {
      //given
      var expectedSignature = "YTgwNDgzNGRjNTA0YjBkYWJmNmFlMzU0MjJiNmRmYTRjNjk5NTQxMDk3MGFkN2YzZjlmZTYyMjdlMTlkNjc4Zg==";
      var mockRequest = new Mock<IHmacMessage>();

      mockRequest.Setup(x => x.Method).Returns("GET");
      mockRequest.Setup(x => x.BodyDigest).Returns("d41d8cd98f00b204e9800998ecf8427e");
      mockRequest.Setup(x => x.ContentType).Returns("application/json");
      mockRequest.Setup(x => x.Date).Returns("Tue, 27 Jun 2017 18:03:47 GMT");
      mockRequest.Setup(x => x.RequestUri).Returns("/test/auth?blah=blah");

      //when
      var res = sut.GetSignature(mockRequest.Object);

      //then
      res.Should()
         .Be(expectedSignature);
    }

    [Fact]
    public void Yet_Another_GetSignature_should_return_a_signature()
    {
      //given
      var expectedSignature = "YmQ2MDhlYTQ1MTVkNmU4MTZjNzVlZTlmZThlMzk4MjQ3NTBjZDU3NmFkYWJiYjEyY2YyMTU4ZGFjM2Q4NzcxNA==";
      var mockRequest = new Mock<IHmacMessage>();

      mockRequest.Setup(x => x.Method).Returns("GET");
      mockRequest.Setup(x => x.BodyDigest).Returns("d751713988987e9331980363e24189ce");      
      mockRequest.Setup(x => x.ContentType).Returns("application/json; charset=utf-8");
      mockRequest.Setup(x => x.Date).Returns("Wed, 16 Dec 2020 16:21:39 -0000");
      mockRequest.Setup(x => x.RequestUri).Returns("/api/v2/integrations/86c00180-d641-4a8d-8b1b-6e6064635a20/accounts?identifier=win.VExBQlxBRE1JTi5TLTEtNS0yMS04Nzg4MjU3MjMtMzUxMDYyMjgwMC0yNTExNTU1MDY3");

      sut = new TrusonaHmacSignatureGenerator("c65573c00683247602028417a18404d52f22bd454483771cb837e8a68a2bc8da8554da1ab789881338ae4f85955891213204fb469f4d59005cad38d7e4e97eb0");

      //when
      var res = sut.GetSignature(mockRequest.Object);

      //then
      res.Should()
         .Be(expectedSignature);
    }

  }
}