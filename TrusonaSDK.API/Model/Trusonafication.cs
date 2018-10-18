//
// Trusonafication.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
namespace TrusonaSDK.API.Model
{
  public sealed class Trusonafication
  {
    #region Constructors

    private Trusonafication()
    {
      this.UserPresence = true;
      this.Prompt = true;
    }

    #endregion

    #region Properties

    public string DeviceIdentifier
    {
      get;
      internal set;
    }

    public string UserIdentifier
    {
      get;
      internal set;
    }

    public string EmailAddress
    {
      get;
      internal set;
    }

    public int DesiredLevel
    {
      get;
      internal set;
    }

    public Guid? TruCodeId
    {
      get;
      internal set;
    }

    public string Action
    {
      get;
      internal set;
    }

    public string Resource
    {
      get;
      internal set;
    }

    public DateTime? ExpiresAt
    {
      get;
      internal set;
    }

    public string CallbackUrl
    {
      get;
      internal set;
    }

    public bool UserPresence
    {
      get;
      internal set;
    }

    public bool Prompt
    {
      get;
      internal set;
    }

    public bool ShowIdentityDocument
    {
      get;
      internal set;
    }

    #endregion

    #region Builder Steps

    public interface IIdentifierStep
    {
      /// <summary>
      /// Sets the device identifier of the user to be authenticated.
      /// </summary>
      /// <returns>The next step required to finish building the trusonafication.</returns>
      /// <param name="deviceIdentifier">Device identifier.</param>
      IActionStep DeviceIdentifier(String deviceIdentifier);

      /// <summary>
      /// Sets the TruCode ID that was scanned by a Trusona enabled device.
      /// The TruCode ID will be used to look up the device identifier that performed the scan.
      /// </summary>
      /// <returns>The next step required to finish building the trusonafication.</returns>
      /// <param name="truCodeId">Tru code identifier.</param>
      IActionStep TruCode(Guid truCodeId);

      /// <summary>
      /// Sets the user identifier of the user to be authenticated.
      /// </summary>
      /// <returns>The next step required to finish building the trusonafication.</returns>
      /// <param name="userIdentifier">User identifier.</param>
      IActionStep UserIdentifier(String userIdentifier);
    }

    public interface IActionStep
    {
      /// <summary>
      /// Sets the action that the user is attempting to perform.
      /// </summary>
      /// <returns>The next step required to finish building the trusonafication.</returns>
      /// <param name="action">Action.</param>
      IResourceStep Action(String action);
    }

    public interface IResourceStep
    {
      /// <summary>
      /// Sets the resource that the user is taking action on.
      /// </summary>
      /// <returns>The next step required to finish building the trusonafication.</returns>
      /// <param name="resource">Resource.</param>
      IFinalizeStep Resource(string resource);
    }

    public interface IFinalizeStep
    {
      /// <summary>
      /// Sets the flag on whether to require the user's approval to false.
      /// </summary>
      /// <returns>The next step required to finish building the trusonafication.</returns>
      IFinalizeStep WithoutPrompt();

      /// <summary>
      /// Sets the flag on whether or not the user is required to prove their presence to false.
      /// The user proves their presence by proving they can perform the action required to unlock their phone,
      /// using whatever mechanism (PIN, pattern, biometric, etc) the user has configured for their device.
      /// If no mechanism has been configured the user will not be able to meet this requirement.
      /// </summary>
      /// <returns>The next step required to finish building the trusonafication.</returns>
      IFinalizeStep WithoutUserPresence();

      /// <summary>
      /// Sets the url to call when this trusonafication has either been accepted or rejected. 
      /// Trusonafications that expire will not invoke the callback URL.
      /// </summary>
      /// <returns>The next step required to finish building the trusonafication.</returns>
      /// <param name="callbackUrl">Callback URL.</param>
      IFinalizeStep CallbackUrl(string callbackUrl);

      /// <summary>
      /// Sets the time when this authentication request should expire. It cannot be responded to after it expires.
      /// </summary>
      /// <returns>The next step required to finish building the trusonafication.</returns>
      /// <param name="expiresAt">Expires at.</param>
      IFinalizeStep ExpiresAt(DateTime expiresAt);

      /// <summary>
      /// Returns the trusonafication that was configured by the builder.
      /// </summary>
      /// <returns>The Trusonafication.</returns>
      Trusonafication Build();
    }

    #endregion

    #region Step Builder

    private abstract class Builder : IIdentifierStep, IActionStep, IResourceStep, IFinalizeStep
    {
      #region Fields

      protected readonly Trusonafication _trusonafication;

      #endregion

      #region Constructor

      protected Builder(int desiredLevel)
      {
        _trusonafication = new Trusonafication();
        _trusonafication.DesiredLevel = desiredLevel;
      }

      #endregion

      #region IdentifierStep

      public virtual IActionStep DeviceIdentifier(string deviceIdentifier)
      {
        _trusonafication.DeviceIdentifier = deviceIdentifier;
        return this;
      }

      public virtual IActionStep TruCode(Guid truCodeId)
      {
        _trusonafication.TruCodeId = truCodeId;
        return this;
      }

      public virtual IActionStep UserIdentifier(string userIdentifier)
      {
        _trusonafication.UserIdentifier = userIdentifier;
        return this;
      }

      #endregion

      #region ActionStep

      public virtual IResourceStep Action(string action)
      {
        _trusonafication.Action = action;
        return this;
      }

      #endregion

      #region ResourceStep

      public virtual IFinalizeStep Resource(string resource)
      {
        _trusonafication.Resource = resource;
        return this;
      }

      #endregion

      #region FinalizeStep

      public virtual IFinalizeStep CallbackUrl(string callbackUrl)
      {
        _trusonafication.CallbackUrl = callbackUrl;
        return this;
      }

      public virtual IFinalizeStep ExpiresAt(DateTime expiresAt)
      {
        _trusonafication.ExpiresAt = expiresAt.ToUniversalTime();
        return this;
      }

      public virtual IFinalizeStep WithoutPrompt()
      {
        _trusonafication.Prompt = false;
        return this;
      }

      public virtual IFinalizeStep WithoutUserPresence()
      {
        _trusonafication.UserPresence = false;
        return this;
      }

      public Trusonafication Build()
      {
        return _trusonafication;
      }

      #endregion
    }

    #endregion

    #region Essential Builder

    private class EssentialBuilder : Builder
    {
      public EssentialBuilder() : base(desiredLevel: 2) { }

      public override IFinalizeStep WithoutUserPresence()
      {
        _trusonafication.DesiredLevel = 1;
        return base.WithoutUserPresence();
      }

    }

    public static IIdentifierStep Essential() => new EssentialBuilder();

    #endregion

    #region Executive Builder

    private class ExecutiveBuilder : Builder
    {
      public ExecutiveBuilder() : base(desiredLevel: 3)
      {
        _trusonafication.ShowIdentityDocument = true;
      }
    }

    public static IIdentifierStep Executive() => new ExecutiveBuilder();

    #endregion
  }
}
