//
// Trusona.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using AutoMapper;
using TrusonaSDK.API.Configuration;
using TrusonaSDK.API.Model;
using System.Net;
using TrusonaSDK.HTTP.Client.V2.Request;
using TrusonaSDK.HTTP.Client.V2.Service;
using TrusonaSDK.HTTP.Client;
using TrusonaSDK.HTTP.Client.V2.Response;

namespace TrusonaSDK.API
{
  /// <summary>
  /// The Trusona API
  /// </summary>
  public sealed class Trusona
  {
    private const TrusonaEnvironment defaultEnv = TrusonaEnvironment.PRODUCTION;
    private readonly ServiceFactory _serviceFactory;

    private IUserDeviceService _userDeviceService;
    private ITruCodeService _trucodeService;
    private ITrusonaficationService _trusonaficationsService;
    private IWebSdkConfigService _webSdkConfigService;
    private IIdentityDocumentService _identityDocumentService;
    private IDeviceService _deviceService;
    private IUserService _userService;

    internal readonly IMapper mapper;
    internal readonly TimeSpan pollingInterval = TimeSpan.FromSeconds(5);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:TrusonaClient.API.Trusona"/> class.
    /// </summary>
    /// <param name="token">Token.</param>
    /// <param name="secret">Secret.</param>
    /// <param name="environment">Environment.</param>
    public Trusona(string token, string secret, TrusonaEnvironment environment = defaultEnv)
      : this(new ServiceFactory(new ConfigurationFactory().GetConfiguration(environment, token, secret)))
    { }

    internal Trusona(ServiceFactory serviceFactory)
    {
      this._serviceFactory = serviceFactory;
      this.mapper = ConfigureMapper().CreateMapper();
    }

    internal IUserDeviceService UserDeviceService
    {
      get
      {
        if (_userDeviceService == null) { _userDeviceService = _serviceFactory.CreateInstance<UserDeviceService>(); };
        return _userDeviceService;
      }
    }

    internal ITruCodeService TruCodeService
    {
      get
      {
        if (_trucodeService == null) { _trucodeService = _serviceFactory.CreateInstance<TruCodeService>(); }
        return _trucodeService;
      }
    }

    internal ITrusonaficationService TrusonaficationService
    {
      get
      {
        if (_trusonaficationsService == null) { _trusonaficationsService = _serviceFactory.CreateInstance<TrusonaficationService>(); }
        return _trusonaficationsService;
      }
    }

    internal IWebSdkConfigService WebSdkConfigService
    {
      get
      {
        if (_webSdkConfigService == null) { _webSdkConfigService = _serviceFactory.CreateInstance<WebSdkConfigService>(); }
        return _webSdkConfigService;
      }
    }

    internal IIdentityDocumentService IdentityDocumentService
    {
      get
      {
        if (_identityDocumentService == null) { _identityDocumentService = _serviceFactory.CreateInstance<IdentityDocumentService>(); }
        return _identityDocumentService;
      }
    }

    internal IDeviceService DeviceService
    {
      get
      {
        if (_deviceService == null) { _deviceService = _serviceFactory.CreateInstance<DeviceService>(); }
        return _deviceService;
      }
    }

    internal IUserService UserService
    {
      get
      {
        if (_userService == null) { _userService = _serviceFactory.CreateInstance<UserService>(); }
        return _userService;
      }
    }

    internal delegate void ErrorHandler(HttpStatusCode httpStatus, string requestId);
    internal static void DefaultErrorHandler(HttpStatusCode httpStatus, string requestId)
    {
      switch (httpStatus)
      {
        case HttpStatusCode.BadRequest:
          throw new TrusonaException("The Trusona SDK was unable to fulfill your request do to an error with the SDK. Contact Trusona to determine the issue.", requestId);
        case HttpStatusCode.Forbidden:
          throw new TrusonaException("The token and/or secret you are using are invalid. Contact Trusona to get valid Server SDK credentials.", requestId);
        case HttpStatusCode.NotFound:
          throw new TrusonaException("Resource not found", requestId);
        case (HttpStatusCode)422:
          throw new ValidationException("One or more values were missing from the request", requestId);
      }
    }

    internal static void HandleServiceException(TrusonaServiceException serviceException,
                                               ErrorHandler errorHandler)
    {
      errorHandler?.Invoke(serviceException.HttpResponse.StatusCode, serviceException.RequestId);
      throw new TrusonaException("A network related error occurred. You should double check that you can connect to Trusona and try your request again",
        serviceException);
    }

    private static MapperConfiguration ConfigureMapper()
    {
      return new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Trusonafication, TrusonaficationRequest>();
        cfg.CreateMap<TrusonaficationResponse, TrusonaficationResult>();
        cfg.CreateMap<TruCodeResponse, TruCode>();
        cfg.CreateMap<UserDeviceResponse, UserDevice>();
        cfg.CreateMap<IdentityDocumentResponse, IdentityDocument>();
        cfg.CreateMap<DeviceResponse, Device>();
      });
    }
  }
}