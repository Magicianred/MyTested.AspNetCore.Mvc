﻿namespace MyTested.AspNetCore.Mvc.Test
{
    using Internal.Session;
    using Internal.Services;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.DependencyInjection;
    using Setups.Common;
    using Setups.Startups;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void DefaultConfigurationWithSessionShouldSetMockSession()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.NotNull(session);
            Assert.IsAssignableFrom<SessionStoreMock>(session);

            MyApplication.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void ExplicitMockSessionShouldOverrideIt()
        {
            MyApplication
                .StartsFrom<SessionDataStartup>()
                .WithServices(services =>
                {
                    services.ReplaceSession();
                });

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.NotNull(session);
            Assert.IsAssignableFrom<SessionStoreMock>(session);

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DefaultConfigurationShouldNotSetMockSession()
        {
            MyApplication.IsUsingDefaultConfiguration();

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.Null(session);
        }

        [Fact]
        public void CustomSessionShouldOverrideTheMockOne()
        {
            MyApplication.StartsFrom<SessionDataStartup>();

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.NotNull(session);
            Assert.IsAssignableFrom<CustomSessionStore>(session);

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
