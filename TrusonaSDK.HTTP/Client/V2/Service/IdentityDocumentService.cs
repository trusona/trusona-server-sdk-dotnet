//
// IdentityDocumentService.cs
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
