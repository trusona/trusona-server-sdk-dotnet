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
  public sealed class ManagedUserTrusonafication
  {
    #region Constructors

    private ManagedUserTrusonafication() { }

    #endregion

    #region Properties

    public string Email
    {
      get;
      internal set;
    }

    public int DesiredLevel
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

    #endregion

    #region Builder Steps

    public interface IIdentifierStep
    {
      /// <summary>
      /// Sets the email address of the user to be authenticated.
      /// </summary>
      /// <returns>The next step required to finish building the trusonafication.</returns>
      /// <param name="email">Email.</param>
      IActionStep Email(string email);
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
      ManagedUserTrusonafication Build();
    }

    #endregion

    #region Step Builder

    private abstract class Builder : IIdentifierStep, IActionStep, IResourceStep, IFinalizeStep
    {
      #region Fields

      protected readonly ManagedUserTrusonafication _trusonafication;

      #endregion

      #region Constructor

      protected Builder(int desiredLevel)
      {
        _trusonafication = new ManagedUserTrusonafication();
        _trusonafication.DesiredLevel = desiredLevel;
      }

      #endregion

      #region IdentifierStep

      public virtual IActionStep Email(string email)
      {
        _trusonafication.Email = email;
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

      public ManagedUserTrusonafication Build()
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
    }

    public static IIdentifierStep Essential() => new EssentialBuilder();

    #endregion

    #region Executive Builder

    private class ExecutiveBuilder : Builder
    {
      public ExecutiveBuilder() : base(desiredLevel: 3) { }
    }

    public static IIdentifierStep Executive() => new ExecutiveBuilder();

    #endregion
  }
}