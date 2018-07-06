//
// IAuthTokenParser.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 
using System;
namespace TrusonaSDK.HTTP.Client.Security
{
  public interface IAuthTokenParser
  {
    ParsedToken ParseToken(string token);
  }
}
