//
// TrusonaException.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using TrusonaSDK.HTTP.Client.V2.Service;

namespace TrusonaSDK.API
{
  public class TrusonaException : Exception
  {

    public string RequestId
    {
      get;
      private set;
    }

    public TrusonaException(string message) : base(message)
    { }

    public TrusonaException(string message, TrusonaServiceException innerException) : base(message, innerException)
    {
      RequestId = innerException.RequestId;
    }

    public TrusonaException(string message, string requestId) : base(message)
    {
      RequestId = requestId;
    }

    public override string Message => String.Format("{0} [{1}]", base.Message, RequestId);
  }
}
