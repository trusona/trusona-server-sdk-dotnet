//
// TrusonaficationRequest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using Newtonsoft.Json;

namespace TrusonaSDK.HTTP.Client.V2.Request
{
  public class TrusonaficationRequest : BaseRequestResponse
  {
    public string DeviceIdentifier { get; set; }
    public string UserIdentifier { get; set; }
    [JsonProperty("trucode_id")]
    public string TruCodeId { get; set; }
    public string Email { get; set; }
    public int DesiredLevel { get; set; }
    public string Action { get; set; }
    public string Resource { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public string CallbackUrl { get; set; }
    public bool UserPresence { get; set; }
    public bool Prompt { get; set; }
    public bool ShowIdentityDocument { get; set; }

    public TrusonaficationRequest() { }
  }
}