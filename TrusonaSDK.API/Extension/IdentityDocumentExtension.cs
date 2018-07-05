//
// IdentityDocumentApiExtension.cs
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
using System.Linq;
using System.Threading.Tasks;
using TrusonaSDK.API.Model;
using TrusonaSDK.HTTP.Client.V2.Service;
using static TrusonaSDK.API.Trusona;
namespace TrusonaSDK.API
{
  /// <summary>
  /// Identity documents API.
  /// </summary>
  public static class IdentityDocumentExtension
  {
    /// <summary>
    /// Finds the identity documents.
    /// </summary>
    /// <returns>The identity documents.</returns>
    /// <param name="trusona">Trusona API.</param>
    /// <param name="userIdentifier">User identifier.</param>
    public static async Task<IEnumerable<IdentityDocument>> FindIdentityDocuments(this Trusona trusona, string userIdentifier)
    {
      try
      {
        var response = await trusona.IdentityDocumentService.FindIdentityDocumentsAsync(userIdentifier);
        var result = response.Select(r => trusona.mapper.Map<IdentityDocument>(r));
        return result;
      }
      catch (TrusonaServiceException ex)
      {
        HandleServiceException(ex, DefaultErrorHandler);
        throw ex;
      }
    }

    /// <summary>
    /// Gets the identity document.
    /// </summary>
    /// <returns>The identity document.</returns>
    /// <param name="trusona">Trusona API.</param>
    /// <param name="id">Identifier.</param>
    public static async Task<IdentityDocument> GetIdentityDocument(this Trusona trusona, Guid id)
    {
      try
      {
        var response = await trusona.IdentityDocumentService.GetIdentityDocumentAsync(id);
        var result = trusona.mapper.Map<IdentityDocument>(response);
        return result;
      }
      catch (TrusonaServiceException ex)
      {
        HandleServiceException(ex, DefaultErrorHandler);
        throw ex;
      }
    }
  }
}