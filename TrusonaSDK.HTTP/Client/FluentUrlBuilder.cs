//
// ServiceUrlBuilder.cs
//
// Author:
//       David Kopack <d@trusona.com>
//
// Copyright (c) 2018 Trusona, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
