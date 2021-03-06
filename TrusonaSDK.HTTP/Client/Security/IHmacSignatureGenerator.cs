//
// HmacSignatureGenerator.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.HTTP.Client.Security
{
  public interface IHmacSignatureGenerator
  {
    string GetSignature(IHmacMessage message);
  }
}
