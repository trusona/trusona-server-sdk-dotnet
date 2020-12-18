//
// ServiceHttpClient.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using TrusonaSDK.HTTP.Client.V2.Serialization;

namespace TrusonaSDK.HTTP.Client.V2.Service
{
  public abstract class HttpService
  {
    private readonly IRequestResponseSerializer _serializer;
    private readonly IHttpClientWrapper _clientWrapper;
    private readonly Uri _endpointUrl;

    protected HttpService(
      IRequestResponseSerializer serializer,
      IHttpClientWrapper clientWrapper,
      Uri endpointUrl)
    {
      this._serializer = serializer;
      this._clientWrapper = clientWrapper;
      this._endpointUrl = endpointUrl;
    }

    protected static T BlockAsyncForResult<T>(Task<T> task) => task.GetAwaiter().GetResult();

    protected static void BlockAsyncForResult(Task task) => task.GetAwaiter().GetResult();

    protected async Task<T> Get<T>(string resource,
                                   string id = null,
                                   ICredentialProvider credentialProvider = null,
                                   List<Tuple<string, object>> queryParams = null)
    {
      var message = new HttpRequestMessage()
      {
        Method = HttpMethod.Get,
        RequestUri = MemberUrl(resource, id)
          .AppendQueryParams(queryParams)
      };

      var response = await _clientWrapper.HandleRequest(message, credentialProvider);

      try
      {
        return await HandleContent<T>(response.EnsureSuccessStatusCode().Content);
      }
      catch (HttpRequestException ex)
      {
        throw new TrusonaServiceException(ex, response, TryResolveRequestId(response));
      }
    }

    protected async Task<T> Get<T>(string resource, ICredentialProvider credentialProvider, List<Tuple<string, object>> queryParams)
    {
      var message = new HttpRequestMessage()
      {
        Method = HttpMethod.Get,
        RequestUri = CollectionUrl(resource).AppendQueryParams(queryParams)
      };

      var response = await _clientWrapper.HandleRequest(message, credentialProvider);

      try
      {
        return await HandleContent<T>(response.EnsureSuccessStatusCode().Content);
      }
      catch (HttpRequestException ex)
      {
        throw new TrusonaServiceException(ex, response, TryResolveRequestId(response));
      }
    }

    protected async Task<T> Post<T>(string resource,
                                    object content,
                                    ICredentialProvider credentialProvider = null)
    {
      var message = new HttpRequestMessage()
      {
        Method = HttpMethod.Post,
        RequestUri = CollectionUrl(resource),
        Content = new StringContent(
          content: _serializer.SerializeRequest(content),
          encoding: Encoding.UTF8,
          mediaType: Headers.MEDIA_TYPE_JSON_VALUE
        )
      };

      var response = await _clientWrapper.HandleRequest(message, credentialProvider);

      try
      {
        return await HandleContent<T>(response.EnsureSuccessStatusCode().Content);
      }
      catch (HttpRequestException ex)
      {
        throw new TrusonaServiceException(ex, response, TryResolveRequestId(response));
      }
    }

    protected async Task Post(string resource,
                              object content,
                              ICredentialProvider credentialProvider = null)
    {
      var message = new HttpRequestMessage()
      {
        Method = HttpMethod.Post,
        RequestUri = CollectionUrl(resource),
        Content = new StringContent(
          content: _serializer.SerializeRequest(content),
          encoding: Encoding.UTF8,
          mediaType: Headers.MEDIA_TYPE_JSON_VALUE
        )
      };

      var response = await _clientWrapper.HandleRequest(message, credentialProvider);

      try
      {
        response.EnsureSuccessStatusCode();
      }
      catch (HttpRequestException ex)
      {
        throw new TrusonaServiceException(ex, response, TryResolveRequestId(response));
      }
    }

    protected async Task<T> Patch<T>(string resource,
                                     object content,
                                     string id = null,
                                     ICredentialProvider credentialProvider = null)
    {
      var message = new HttpRequestMessage()
      {
        Method = new HttpMethod("PATCH"),
        RequestUri = MemberUrl(resource, id),
        Content = new StringContent(
          content: _serializer.SerializeRequest(content),
          encoding: Encoding.UTF8,
          mediaType: Headers.MEDIA_TYPE_JSON_VALUE
        )
      };

      var response = await _clientWrapper.HandleRequest(message, credentialProvider);

      try
      {
        return await HandleContent<T>(
          response.EnsureSuccessStatusCode().Content
        );
      }
      catch (HttpRequestException ex)
      {
        throw new TrusonaServiceException(ex, response, TryResolveRequestId(response));
      }
    }

    protected async Task Delete(string resource, string id, ICredentialProvider credentialProvider)
    {
      var message = new HttpRequestMessage
      {
        Method = new HttpMethod("DELETE"),
        RequestUri = MemberUrl(resource, id)
      };
      var response = await _clientWrapper.HandleRequest(message, credentialProvider);

      try
      {
        response.EnsureSuccessStatusCode();
      }
      catch (HttpRequestException ex)
      {
        throw new TrusonaServiceException(ex, response, TryResolveRequestId(response));
      }
    }

    private async Task<T> HandleContent<T>(HttpContent content) => _serializer.DeserializeResponse<T>(await content.ReadAsStringAsync());

    private static string TryResolveRequestId(HttpResponseMessage responseMessage)
      => responseMessage.Headers.TryGetValues(Headers.X_REQUEST_ID, out IEnumerable<string> headerValues) ? headerValues.First() : null;

    private FluentUrlBuilder CollectionUrl(string resource) => new FluentUrlBuilder(_endpointUrl).AppendPath(resource);

    private FluentUrlBuilder MemberUrl(string resource, string id) => CollectionUrl(resource).AppendPath(id ?? "");
  }
}