//
// Configuration.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.HTTP
{
  public interface IConfiguration
  {
    Uri EndpointUrl { get; }
    ICredentialProvider CredentialProvider { get; }
  }
}
