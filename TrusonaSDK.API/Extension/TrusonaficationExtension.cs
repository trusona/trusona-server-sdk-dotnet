//
// TrusonaficationApiExtension.cs
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
        HandleServiceException(ex, DefaultErrorHandler);
        throw ex;
      }
    }

    /// <summary>
    /// Creates a Trusonafication and returns a TrusonaficationResult.
    /// This will block until the Trusonafication is no longer IN_PROGRESS
    /// </summary>
    /// <returns>A TrusonaficationResult describing the current status of the Trusonafication.</returns>
    /// <param name="trusona">Trusona API.</param>
    /// <param name="trusonafication">A ManagedUserTrusonafication.</param>
    public static async Task<TrusonaficationResult> CreateTrusonafication(this Trusona trusona, ManagedUserTrusonafication trusonafication)
    {
      try
      {
        var response = await trusona.TrusonaficationService.CreateTrusonaficationAsync(
          trusona.mapper.Map<TrusonaficationRequest>(trusonafication));

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
  }
}