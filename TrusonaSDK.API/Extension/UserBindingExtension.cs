//
// UserBindingExtension.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2019 
//
//
using System.Net;
using System.Threading.Tasks;
using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Service;
using static TrusonaSDK.API.Trusona;

namespace TrusonaSDK.API
{
  public static class UserBindingExtension
  {
    /// <summary>
    /// Binds the provided userIdentifier to the Trusona user that scanned the provided secure QR code ID.
    /// </summary>
    /// <param name="trusona">Trusona API.</param>
    /// <param name="userIdentifier">User identifier.</param>
    /// <param name="truCodeId">The secure QR code ID that the user scanned.</param>
    public static async Task CreateUserBinding(this Trusona trusona, string userIdentifier, string truCodeId)
    {
      UserBindingRequest request = new UserBindingRequest()
      {
        UserIdentifier = userIdentifier,
        TruCodeId = truCodeId
      };

      try
      {
        await trusona.UserBindingService.CreateUserBindingAsync(request);
      }
      catch (TrusonaServiceException ex)
      {
        HandleServiceException(ex, (httpStatus, requestId) =>
        {
          switch (httpStatus)
          {
            case HttpStatusCode.Conflict:
              throw new UserAlreadyBoundException("A different userIdentifier has already been bound to this user.");
            case (HttpStatusCode)424:
              throw new TruCodeNotPairedException("The provided truCodeId was not scanned by a Trusona user and we could not determine which user to bind the identifier to.");
            default:
              DefaultErrorHandler(httpStatus, requestId);
              break;
          }
        });
      }
    }
  }
}
