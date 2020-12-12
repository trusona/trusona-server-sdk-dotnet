//
// UserExtension.cs
//
// Author:
//       alwold <>
//
// Copyright (c) 2018 
//
//
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.V2.Service;
using static TrusonaSDK.API.Trusona;

namespace TrusonaSDK.API
{
  /// <summary>
  /// User extension.
  /// </summary>
  public static class UserExtension
  {
    /// <summary>
    /// Delete a user by their user identifier
    /// </summary>
    /// <param name="trusona">Trusona API.</param>
    /// <param name="userIdentifier">User identifier.</param>
    public static async Task DeleteUser(this Trusona trusona, string userIdentifier)
    {
      try
      {
        await trusona.UserService.DeleteUserAsync(userIdentifier);
      }
      catch (TrusonaServiceException ex)
      {
        HandleServiceException(ex, DefaultErrorHandler);
        throw ex;
      }
    }
  }
}
