//
// UserBindingRequestTest.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2019 
//
//
using System;
using TrusonaSDK.HTTP.Client.V2.Request;

namespace TrusonaSDK.Test.HTTP.Client.V2.Request
{
  public class UserBindingRequestTest : RequestResponseTest<UserBindingRequest>
  {
    public override UserBindingRequest Sut => new UserBindingRequest()
    {
      TruCodeId = "3827D5E5-B6C1-49F8-865E-72794D10BEF4",
      UserIdentifier = "datUser"
    };

    public override string Json => @"{
  ""trucode_id"": ""3827D5E5-B6C1-49F8-865E-72794D10BEF4"",
  ""user_identifier"": ""datUser""
}";
  }
}
