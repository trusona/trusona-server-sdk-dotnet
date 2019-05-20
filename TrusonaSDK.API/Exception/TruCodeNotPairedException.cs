//
// TruCodeNotPairedException.cs
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
  public class TruCodeNotPairedException : TrusonaException
  {
    public TruCodeNotPairedException(String message): base(message)
    {
    }
  }
}
