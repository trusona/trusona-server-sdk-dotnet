//
// IUserBindingService.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2019 
//
//
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.V2.Request;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public interface IUserBindingService
  {
    void CreateUserBinding(UserBindingRequest request);
    Task CreateUserBindingAsync(UserBindingRequest request);
  }
}
