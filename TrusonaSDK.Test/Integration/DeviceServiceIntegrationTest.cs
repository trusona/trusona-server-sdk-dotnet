//
// DeviceServiceIntegrationTest.cs
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
using TrusonaSDK.API;
using Xunit;
using FluentAssertions;
namespace TrusonaSDK.Integration
{
  public class DeviceServiceIntegrationTest : IntegrationServiceTest
  {
    [Fact]
    [Trait("Category", "Integration")]
    public void GetDevice_should_return_a_valid_response()
    {
      //given
      var deviceIdentifier = "kC_9iF_CNcJqdU4PvJspx6okdQnxJsYNteL0EJG_O-c";

      //when
      var res = sut.GetDevice(deviceIdentifier).Result;

      //then
      res.Active
         .Should()
         .BeTrue();
      res.ActivatedAt.Value
         .Should()
         .Be(DateTime.Parse("2018-01-12 21:36:17.833"));
    }
  }
}
