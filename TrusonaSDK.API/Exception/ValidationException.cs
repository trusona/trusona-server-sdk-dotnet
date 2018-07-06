//
// ValidationException.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.API
{
  public class ValidationException : TrusonaException
  {
    public ValidationException(string message, string requestId) : base(message, requestId)
    {
    }
  }
}
