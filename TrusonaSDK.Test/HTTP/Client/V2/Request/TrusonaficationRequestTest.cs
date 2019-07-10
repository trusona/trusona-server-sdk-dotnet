//
// TrusonaficationRequestTest.cs
//
// Author:
//       David Kopack <d@trusona.com>
//       Nikolas Mangu-Thitu <n@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;

using Xunit;
using FluentAssertions;

using TrusonaSDK.HTTP.Client.V2;
using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Serialization;

namespace TrusonaSDK.HTTP.Client.V2.Request
{
  public class TrusonaficationRequestTest : RequestResponseTest<TrusonaficationRequest>
  {
    public static Dictionary<string, object> TestCustomFields()
    {
      Dictionary<string, object> customFields = new Dictionary<string, object>();
      customFields.Add("african", "tiger");
      customFields.Add("taco", 1);

      return customFields;
    }

    public override TrusonaficationRequest Sut => new TrusonaficationRequest()
    {
      DeviceIdentifier = "datDevice",
      UserIdentifier = "datUser",
      TruCodeId = "3827D5E5-B6C1-49F8-865E-72794D10BEF4",
      EmailAddress = "jones@tacos.com",
      DesiredLevel = 2,
      Action = "partay",
      Resource = "yourhauz",
      ExpiresAt = DateTime.Parse("2018-01-23T23:28:45Z").ToUniversalTime(),
      UserPresence = false,
      Prompt = false,
      ShowIdentityDocument = true
    };

    public TrusonaficationRequest SutWithCustomFields => new TrusonaficationRequest()
    {
      DeviceIdentifier = "datDevice",
      UserIdentifier = "datUser",
      TruCodeId = "3827D5E5-B6C1-49F8-865E-72794D10BEF4",
      EmailAddress = "jones@tacos.com",
      DesiredLevel = 2,
      Action = "partay",
      Resource = "yourhauz",
      ExpiresAt = DateTime.Parse("2018-01-23T23:28:45Z").ToUniversalTime(),
      UserPresence = false,
      Prompt = false,
      ShowIdentityDocument = true,
      CustomFields = TestCustomFields()
    };

    public override string Json => @"{
  ""action"": ""partay"",
  ""desired_level"": 2,
  ""device_identifier"": ""datDevice"",
  ""email"": ""jones@tacos.com"",
  ""expires_at"": ""2018-01-23T23:28:45Z"",
  ""prompt"": false,
  ""resource"": ""yourhauz"",
  ""show_identity_document"": true,
  ""trucode_id"": ""3827D5E5-B6C1-49F8-865E-72794D10BEF4"",
  ""user_identifier"": ""datUser"",
  ""user_presence"": false
}";

    public string JsonWithCustomFields => @"{
  ""action"": ""partay"",
  ""custom_fields"": {
    ""african"": ""tiger"",
    ""taco"": 1
  },
  ""desired_level"": 2,
  ""device_identifier"": ""datDevice"",
  ""email"": ""jones@tacos.com"",
  ""expires_at"": ""2018-01-23T23:28:45Z"",
  ""prompt"": false,
  ""resource"": ""yourhauz"",
  ""show_identity_document"": true,
  ""trucode_id"": ""3827D5E5-B6C1-49F8-865E-72794D10BEF4"",
  ""user_identifier"": ""datUser"",
  ""user_presence"": false
}";

    [Fact]
    public virtual void _with_custom_fields_should_be_serializable()
    {
      var res = RequestResponseJsonConverter.Serialize(SutWithCustomFields);
      res.Should()
         .BeEquivalentTo(JsonWithCustomFields);
    }

    [Fact]
    public virtual void _with_custom_fields_should_be_deserializable()
    {
      var res = RequestResponseJsonConverter.Deserialize<TrusonaficationRequest>(JsonWithCustomFields);
      res.Should()
         .BeEquivalentTo(SutWithCustomFields);
    }
  }
}