//
// IdentityDocumentResponse.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.HTTP.Client.V2.Response
{
  public class IdentityDocumentResponse : BaseRequestResponse
  {
    public Guid Id { get; set; }
    public string Hash { get; set; }
    public string VerificationStatus { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string Type { get; set; }
  }
}
