//
// IIdentityDocumentService.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public interface IIdentityDocumentService
  {
    Task<IdentityDocumentResponse> GetIdentityDocumentAsync(Guid id);

    Task<List<IdentityDocumentResponse>> FindIdentityDocumentsAsync(string userIdentifier);

    IdentityDocumentResponse GetIdentityDocument(Guid id);

    List<IdentityDocumentResponse> FindIdentityDocuments(string userIdentifier);
  }
}