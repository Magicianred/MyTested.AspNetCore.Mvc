﻿namespace MyTested.Mvc.Tests.Setups
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.Serialization.Formatters;
    using Common;
    using Microsoft.AspNet.FileProviders;
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Http.Internal;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Services;

    public static class TestObjectFactory
    {
        public const string MediaType = "application/json";
        
        public static Action<IServiceCollection> GetCustomServicesRegistrationAction()
        {
            return services =>
            {
                services.AddTransient<IInjectedService, InjectedService>();
                services.AddTransient<IAnotherInjectedService, AnotherInjectedService>();
            };
        }

        public static Action<IServiceCollection> GetCustomServicesWithOptionsRegistrationAction()
        {
            return services =>
            {
                services.AddTransient<IInjectedService, InjectedService>();
                services.AddTransient<IAnotherInjectedService, AnotherInjectedService>();
                
                services.Configure<MvcOptions>(options =>
                {
                    options.MaxModelValidationErrors = 10;
                });
            };
        }

        public static HttpRequest GetCustomHttpRequestMessage()
        {
            return new DefaultHttpContext().Request;
        }
        
        public static Uri GetUri()
        {
            return new Uri("http://somehost.com/someuri/1?query=Test");
        }

        public static Action<string, string> GetFailingValidationActionWithTwoParameteres()
        {
            return (x, y) => { throw new NullReferenceException(string.Format("{0} {1}", x, y)); };
        }

        public static Action<string, string, string> GetFailingValidationAction()
        {
            return (x, y, z) => { throw new NullReferenceException(string.Format("{0} {1} {2}", x, y, z)); };
        }

        public static RequestModel GetNullRequestModel()
        {
            return null;
        }

        public static RequestModel GetValidRequestModel()
        {
            return new RequestModel
            {
                Integer = 2,
                RequiredString = "Test"
            };
        }

        public static RequestModel GetRequestModelWithErrors()
        {
            return new RequestModel();
        }

        public static List<ResponseModel> GetListOfResponseModels()
        {
            return new List<ResponseModel>
            {
                new ResponseModel { IntegerValue = 1, StringValue = "Test" },
                new ResponseModel { IntegerValue = 2, StringValue = "Another Test" }
            };
        }

        public static IFileProvider GetFileProvider()
        {
            return new CustomFileProvider();
        }

        public static JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                Culture = CultureInfo.InvariantCulture,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateParseHandling = DateParseHandling.DateTimeOffset,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented,
                MaxDepth = 2,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                PreserveReferencesHandling = PreserveReferencesHandling.Arrays,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                TypeNameHandling = TypeNameHandling.None
            };
        }
    }
}
