//
// DefaultHmacSignatureGeneratorTest.cs
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