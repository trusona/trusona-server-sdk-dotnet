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

namespace TrusonaSDK.HTTP.Client
{
  public class FluentUrlBuilder : UriBuilder
  {
    private List<string> _pathParts;
    private List<Tuple<string, object>> _queryParams;

    public FluentUrlBuilder(Uri baseUrl) : base(baseUrl)
    {
      this._pathParts = new List<string>();
      this._queryParams = new List<Tuple<string, object>>();
    }

    public FluentUrlBuilder AppendPath(string path)
    {
      _pathParts.Add(path);
      Path = string.Join("/", _pathParts.Where(p => !string.IsNullOrEmpty(p)));
      return this;
    }

    public FluentUrlBuilder AppendQueryParam(string key, object value)
    {
      _queryParams.Add(new Tuple<string, object>(key, value));
      Query = string.Join("&", _queryParams.Select(p => string.Join("=", new object[] { key, value })));
      return this;
    }

    public FluentUrlBuilder AppendQueryParams(List<Tuple<string, object>> queryParams)
    {
      if (queryParams != null)
      {
        foreach (var pair in queryParams)
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
