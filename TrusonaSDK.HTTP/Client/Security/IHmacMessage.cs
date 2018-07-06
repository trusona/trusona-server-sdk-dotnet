//
// HmacMessage.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.HTTP.Client.Security
{
  public interface IHmacMessage
  {
    string BodyDigest { get; }

    string ContentType { get; }

    string Date { get; }

    string Method { get; }

    string RequestUri { get; }
  }
}