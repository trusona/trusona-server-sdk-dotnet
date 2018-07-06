//
// TruCodeApiExtension.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Net;
using System.Threading.Tasks;
using TrusonaSDK.API.Model;
using TrusonaSDK.HTTP.Client.V2.Service;
using static TrusonaSDK.API.Trusona;
namespace TrusonaSDK.API
{
  /// <summary>
  /// Tru code API.
  /// </summary>
  public static class TruCodeExtension
  {
    /// <summary>
    /// Get a TruCode that has been paired.
    /// </summary>
    /// <returns>The paired TruCode if it exists, or null.</returns>
    /// <param name="trusona">Trusona API.</param>
    /// <param name="id">The ID of the TruCode.</param>
    public static async Task<TruCode> GetPairedTruCode(this Trusona trusona, Guid id)
    {
      try
      {
        var response = trusona.TruCodeService.GetPairedTrucodeAsync(id);
        return trusona.mapper.Map<TruCode>(await response);
      }
      catch (TrusonaServiceException ex)
      {
        if (ex.HttpResponse.StatusCode == HttpStatusCode.NotFound)
        {
          return null;
        }

        HandleServiceException(ex, DefaultErrorHandler);
        throw ex;
      }
    }
  }
}