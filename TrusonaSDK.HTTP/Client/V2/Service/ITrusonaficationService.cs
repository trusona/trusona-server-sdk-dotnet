//
// ITrusonaficationService.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public interface ITrusonaficationService
  {
    Task<TrusonaficationResponse> CreateTrusonaficationAsync(TrusonaficationRequest request);

    Task<TrusonaficationResponse> GetTrusonaficationAsync(Guid id);

    TrusonaficationResponse CreateTrusonafication(TrusonaficationRequest request);

    TrusonaficationResponse GetTrusonafication(Guid id);
  }
}
