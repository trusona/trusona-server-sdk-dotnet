//
// IdentityDocumentService.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.V2.Response;
using TrusonaSDK.HTTP.Client.V2.Serialization;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public class IdentityDocumentService : HttpService, IIdentityDocumentService
  {
    private readonly ICredentialProvider _credentialProvider;

    public IdentityDocumentService(IEnvironment environment, IHttpClientWrapper clientWrapper)
      : base(new RequestResponseJsonConverter(), clientWrapper, environment.EndpointUrl)
    {
      this._credentialProvider = environment.CredentialProvider;
    }

    public List<IdentityDocumentResponse> FindIdentityDocuments(string userIdentifier)
    {
      return BlockAsyncForResult(
        FindIdentityDocumentsAsync(userIdentifier)
      );
    }

    public Task<List<IdentityDocumentResponse>> FindIdentityDocumentsAsync(string userIdentifier)
    {
      var queryParams = new List<Tuple<string, object>>()
      {
        new Tuple<string, object>("user_identifier", userIdentifier)
      };

      return Get<List<IdentityDocumentResponse>>(
        resource: "/api/v2/identity_documents",
        queryParams: queryParams,
        credentialProvider: _credentialProvider
      );
    }

    public IdentityDocumentResponse GetIdentityDocument(Guid id)
    {
      return BlockAsyncForResult(
        GetIdentityDocumentAsync(id)
      );
    }

    public Task<IdentityDocumentResponse> GetIdentityDocumentAsync(Guid id)
    {
      return Get<IdentityDocumentResponse>(
        id: id.ToString(),
        resource: "/api/v2/identity_documents",
        credentialProvider: _credentialProvider
      );
    }
  }
}
