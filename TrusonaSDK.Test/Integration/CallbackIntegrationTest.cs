//
// CallbackIntegrationTest.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2019 
//
//
using System;
using System.Threading;
using Xunit;
using TrusonaSDK.API;
using TrusonaSDK.API.Model;

namespace TrusonaSDK.Integration
{
  public class CallbackIntegrationTest : IntegrationTest
  {
    [Fact]
    [Trait("Category", "Integration")]
    public void Callback_url_should_get_called_when_trusonafication_is_accepted()
    {
      //given
      var userIdentifier = "TacoMan3000";
      var deviceIdentifier = buster.CreateDevice()["id"];
      var binding = sut.CreateUserDevice(userIdentifier, deviceIdentifier).Result;
      sut.ActivateUserDevice(binding.ActivationCode).Wait();

      var callbackId = Guid.NewGuid().ToString();

      //given
      var trusonafication = Trusonafication.Essential()
                                           .DeviceIdentifier(deviceIdentifier)
                                           .Action("eat")
                                           .Resource("your taco")
                                           .CallbackUrl(buster.GetCallbackUrl(callbackId))
                                           .Build();

      var trusonaficationId = sut.CreateTrusonafication(trusonafication).Result.Id;

      //when
      buster.AcceptTrusonafication(deviceIdentifier, trusonaficationId.ToString());

      //then
      AssertEventuallyTrue(10000, () => buster.GetCallback(callbackId)["status"].Equals("ACCEPTED"));
    }

    public void AssertEventuallyTrue(int timeout, Func<bool> action)
    {
      DateTime startTime = DateTime.UtcNow;
      bool success = false;

      do
      {
        TimeSpan elapsedTime = DateTime.UtcNow - startTime;

        if (elapsedTime.TotalMilliseconds > timeout)
        {
          throw new TimeoutException();
        }
        else
        {
          Thread.Sleep(500);
        }

        try
        {
          success = action();
        }
        catch { }


      } while (!success);
    }
  }
}
