//
// TrusonaficationApiExtension.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Threading;
using System.Threading.Tasks;

using TrusonaSDK.API.Model;
using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Service;

using static TrusonaSDK.API.Trusona;

namespace TrusonaSDK.API
{
  public static class TrusonaficationExtension
  {
    /// <summary>
    /// Creates a Trusonafication and returns a TrusonaficationResult with the current status of the Trusonafication.
    /// See TrusonaficationStatus for the possible statuses.
    /// </summary>
    /// <returns>A TrusonaficationResult describing the current status of the Trusonafication.</returns>
    /// <param name="trusona">Trusona API.</param>
    /// <param name="trusonafication">A Trusonafication.</param>
    public static async Task<TrusonaficationResult> CreateTrusonafication(this Trusona trusona, Trusonafication trusonafication)
    {
      try
      {
        var response = await trusona.TrusonaficationService.CreateTrusonaficationAsync(
          trusona.mapper.Map<TrusonaficationRequest>(trusonafication));

        var result = trusona.mapper.Map<TrusonaficationResult>(response);
        return result;
      }
      catch (TrusonaServiceException ex)
      {
        if (ex.HttpResponse.StatusCode.Equals(422))
        {
          // TODO: Additional logic here?
        }
        HandleServiceException(ex, DefaultErrorHandler);
        throw ex;
      }
    }

    /// <summary>
    /// Gets a TrusonaficationResult for a given Trusonafication ID.
    /// This will block until the Trusonafication is no longer IN_PROGRESS
    /// </summary>
    /// <returns>A TrusonaficationResult</returns>
    /// <param name="trusona">Trusona API.</param>
    /// <param name="trusonaficationId">Trusonafication identifier.</param>
    public static async Task<TrusonaficationResult> GetTrusonaficationResult(this Trusona trusona, Guid trusonaficationId)
    {
      try
      {
        var response = await trusona.TrusonaficationService.GetTrusonaficationAsync(trusonaficationId);

        var result = trusona.mapper.Map<TrusonaficationResult>(response);

        while (TrusonaficationStatus.IN_PROGRESS == result.Status)
        {
          Thread.Sleep(trusona.pollingInterval);
          response = await trusona.TrusonaficationService.GetTrusonaficationAsync(result.Id);
          result = trusona.mapper.Map<TrusonaficationResult>(response);
        }

        return result;
      }
      catch (TrusonaServiceException ex)
      {
        HandleServiceException(ex, DefaultErrorHandler);
        throw ex;
      }
    }

    /// <summary>
    /// Gets a Trusonafication for a given Trusonafication ID.
    /// </summary>
    /// <returns>A TrusonaficationResult</returns>
    /// <param name="trusona">Trusona API.</param>
    /// <param name="trusonaficationId">Trusonafication identifier.</param>
    public static async Task<Trusonafication> GetTrusonafication(this Trusona trusona, Guid trusonaficationId)
    {
      try
      {
        var response = await trusona.TrusonaficationService.GetTrusonaficationAsync(trusonaficationId);
        return trusona.mapper.Map<Trusonafication>(response);
      }
      catch (TrusonaServiceException ex)
      {
        HandleServiceException(ex, DefaultErrorHandler);
        throw ex;
      }
    }
  }
}