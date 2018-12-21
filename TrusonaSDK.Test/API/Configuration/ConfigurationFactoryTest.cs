//
// ConfigurationFactoryTest.cs
//
// Author:
//       delduggel <>
//
// Copyright (c) 2018 
//
//
using FluentAssertions;
using TrusonaSDK.API;
using TrusonaSDK.API.Configuration;
using Xunit;

namespace TrusonaSDK.Test.API.Configuration
{
  public class ConfigurationFactoryTest
  {
    ConfigurationFactory sut;

    public ConfigurationFactoryTest() => sut = new ConfigurationFactory();

    [Theory]
    [InlineData(null, "https://api.trusona.net/")]
    [InlineData(TrusonaEnvironment.PRODUCTION, "https://api.trusona.net/")]
    [InlineData(TrusonaEnvironment.UAT, "https://api.staging.trusona.net/")]
    [InlineData(TrusonaEnvironment.AP_PRODUCTION, "https://api.ap.trusona.net/")]
    [InlineData(TrusonaEnvironment.AP_UAT, "https://api.staging.ap.trusona.net/")]
    public void GetConfiguration_should_set_endpoint_based_on_environment(TrusonaEnvironment environment, string expectedUrl)
    {
      var res = sut.GetConfiguration(environment, null, null);

      res.EndpointUrl.AbsoluteUri.Should().Be(expectedUrl);
    }

    [Fact]
    public void GetConfiguration_should_set_the_token() {
      var res = sut.GetConfiguration(TrusonaEnvironment.UAT, "token", null);

      res.CredentialProvider.Token.Should().Be("token");
    }

    [Fact]
    public void GetConfiguration_should_set_the_secret()
    {
      var res = sut.GetConfiguration(TrusonaEnvironment.UAT, null, "secret");

      res.CredentialProvider.Secret.Should().Be("secret");
    }
  }
}
