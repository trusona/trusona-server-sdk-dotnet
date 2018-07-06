//
// TrusonaServiceException.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Net.Http;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public class TrusonaServiceException : Exception
  {
    private const string message = "Status code did not indicate success";

    public HttpResponseMessage HttpResponse
    {
      get;
      private set;
    }

    public string RequestId
    {
      get;
      private set;
    }

    public TrusonaServiceException(Exception innerException,
                                   HttpResponseMessage httpResponse, string requestId)
      : base(message, innerException)
    {
      this.HttpResponse = httpResponse;
      this.RequestId = requestId;
    }
  }
}
