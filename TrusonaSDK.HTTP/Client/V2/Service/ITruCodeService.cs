//
// TruCodeService.cs
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
  public interface ITruCodeService
  {
    Task<TruCodeResponse> GetPairedTrucodeAsync(Guid trucodeId);

    TruCodeResponse GetPairedTrucode(Guid trucodeId);
  }
}
