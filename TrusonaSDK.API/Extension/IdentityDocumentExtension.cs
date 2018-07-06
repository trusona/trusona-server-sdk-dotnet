//
// IdentityDocumentApiExtension.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
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