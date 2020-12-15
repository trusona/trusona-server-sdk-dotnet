//
// DefaultAuthTokenParser.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using Newtonsoft.Json;
using System.Text;
namespace TrusonaSDK.HTTP.Client.Security
{
  public class DefaultAuthTokenParser : IAuthTokenParser
  {
    const char SEP_CHAR = '.';

    public ParsedToken ParseToken(string token)
    {
      string[] parts = token.Split(SEP_CHAR);
      if (parts.Length != 3)
      {
        return null;
      }

      string decodedData = Encoding.UTF8.GetString(
        Convert.FromBase64String(ensureBase64Padding(parts[1]))
      );

      return JsonConvert.DeserializeObject<ParsedToken>(decodedData);
    }

    private string ensureBase64Padding(string input)
    {
      int mod4 = input.Length % 4;
      if (mod4 > 0)
      {
        input += new string('=', 4 - mod4);
      }
      return input;
    }
  }
}