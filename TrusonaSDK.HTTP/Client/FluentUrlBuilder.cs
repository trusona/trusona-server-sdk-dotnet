//
// ServiceUrlBuilder.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrusonaSDK.HTTP.Client
{
  public class FluentUrlBuilder : UriBuilder
  {
    private readonly List<string> _pathParts;
    private readonly List<Tuple<string, string>> _queryParams;

    public FluentUrlBuilder(Uri baseUrl) : base(baseUrl)
    {
      _pathParts = new List<string>();
      _queryParams = new List<Tuple<string, string>>();
    }

    public FluentUrlBuilder AppendPath(string path)
    {
      _pathParts.Add(path);
      Path = string.Join("/", _pathParts.Where(p => !string.IsNullOrEmpty(p)));
      return this;
    }

    public FluentUrlBuilder AppendQueryParam(string key, object value)
    {
      string safeKey = UrlEncode(key);
      string safeValue = UrlEncode(value.ToString());

      _queryParams.Add(new Tuple<string, string>(safeKey, safeValue));
      string data = "";

      foreach (Tuple<string, string> pair in _queryParams)
      {
        data += $"{pair.Item1}={pair.Item2}&";
      }

      Query = data.Substring(0, data.Length - 1);
      return this;
    }

    private string UrlEncode(string value) => HttpUtility.UrlEncode(value);

    public FluentUrlBuilder AppendQueryParams(List<Tuple<string, object>> queryParams)
    {
      if (queryParams != null)
      {
        foreach (Tuple<string, object> pair in queryParams)
        {
          AppendQueryParam(pair.Item1, pair.Item2);
        }
      }
      return this;
    }

    public Uri Build()
    {
      return this.Uri;
    }

    public static implicit operator Uri(FluentUrlBuilder builder)
    {
      return builder.Build();
    }
  }
}
