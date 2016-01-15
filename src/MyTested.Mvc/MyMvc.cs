﻿namespace MyTested.Mvc
{
    using System;
    using Builders.Controllers;
    using Internal;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities;

    /// <summary>
    /// Starting point of the testing framework, which provides a way to specify the test case.
    /// </summary>
    public static class MyMvc
    {
        static MyMvc()
        {
            IsUsingDefaultServices();
        }

        public static void IsUsingDefaultServices()
        {
            IsUsing(null);
        }
        
        public static void IsUsing(Action<IServiceCollection> services)
        {
            TestServiceProvider.Setup(services);
        }

        public static void IsUsing<TStartup>() where TStartup : class, new()
        {
            IsUsing<TStartup>(null);
        }
        
        public static void IsUsing<TStartup>(Action<IServiceCollection> services)
            where TStartup : class, new()
        {
            TestServiceProvider.Setup<TStartup>(services);
        }
        
        /// <summary>
        /// Selects controller on which the test will be executed. Controller is instantiated with default constructor.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET MVC 6 controller.</typeparam>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>()
            where TController : Controller
        {
            var controller = Reflection.TryCreateInstance<TController>();
            return Controller(() => controller);
        }

        /// <summary>
        /// Selects controller on which the test will be executed.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET MVC 6 controller.</typeparam>
        /// <param name="controller">Instance of the ASP.NET MVC 6 controller to use.</param>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>(TController controller)
            where TController : Controller
        {
            return Controller(() => controller);
        }

        /// <summary>
        /// Selects controller on which the test will be executed. Controller is instantiated using construction function.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET MVC 6 controller.</typeparam>
        /// <param name="construction">Construction function returning the instantiated controller.</param>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>(Func<TController> construction)
            where TController : Controller
        {
            var controllerInstance = construction();
            return new ControllerBuilder<TController>(controllerInstance);
        }
    }
}
