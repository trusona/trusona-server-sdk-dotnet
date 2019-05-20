//
// UserAlreadyBoundException.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2019 
//
//
using System;

namespace TrusonaSDK.API
{
  public class UserAlreadyBoundException : TrusonaException
  {
    public UserAlreadyBoundException(String message) : base(message)
    {
    }
  }
}
