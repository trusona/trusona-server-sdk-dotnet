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
  }
}