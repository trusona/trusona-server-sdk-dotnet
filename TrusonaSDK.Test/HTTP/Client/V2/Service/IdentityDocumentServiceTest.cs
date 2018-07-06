//
// IdentityDocumentServiceTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
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