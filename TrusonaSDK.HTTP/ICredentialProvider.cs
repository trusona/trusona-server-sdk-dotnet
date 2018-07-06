//
// ICredentialProvider.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.HTTP
{
  public interface ICredentialProvider
  {
    string Token { get; }
    string Secret { get; }
  }
}
