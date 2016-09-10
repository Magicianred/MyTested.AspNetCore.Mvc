﻿namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Internal.TestContexts;
    using Utilities;

    public class HttpTestPlugin : BaseTestPlugin, IDefaultRegistrationPlugin, IServiceRegistrationPlugin, IShouldPassForPlugin
    {
        private readonly Type baseHttpContextType = typeof(HttpContext);
        private readonly Type baseHttpRequestType = typeof(HttpRequest);
        private readonly Type baseHttpResponseType = typeof(HttpResponse);

        public long Priority => -9000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate =>
            serviceCollection => serviceCollection
                .AddMvcCore()
                .AddFormatterMappings()
                .AddJsonFormatters();

        public Action<IServiceCollection> ServiceRegistrationDelegate =>
            serviceCollection => serviceCollection.AddStringInputFormatter();

        public object TryGetValue(Type type, ComponentTestContext testContext)
            => Reflection.AreAssignable(baseHttpContextType, type) // HttpContext
                ? testContext.HttpContext
                : Reflection.AreAssignable(baseHttpRequestType, type) // HttpRequest
                ? testContext.HttpRequest
                : Reflection.AreAssignable(baseHttpResponseType, type) // HttpResponse
                ? (object)testContext.HttpResponse
                : null;
    }
}
