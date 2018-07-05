//
// IdentityDocumentServiceTest.cs
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
using TrusonaSDK.HTTP.Client.V2.Service;
using Xunit;
using FluentAssertions;
using TrusonaSDK.HTTP.Client.V2.Response;
using System.Collections.Generic;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public class IdentityDocumentServiceTest : MockedServiceTest<IdentityDocumentService>
  {
    [Fact]
    public void GetIdentityDocument_should_return_an_identity_document_response()
    {
      //given
      SetupMock();

      //when
      var res = sut.GetIdentityDocument(Guid.NewGuid());

      //then
      res.Should()
         .BeOfType<IdentityDocumentResponse>();
    }

    [Fact]
    public void FindIdentityDocuments_should_return_a_collection_of_identity_document_responses()
    {
      //given
      SetupMock(@"[{}, {}]");

      //when
      var res = sut.FindIdentityDocuments("tacos");

      //then
      res.Should()
         .BeOfType<List<IdentityDocumentResponse>>();
      res.Should()
         .HaveCount(2);
    }
  }
}