[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Nintex.UrlShortener.UI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Nintex.UrlShortener.UI.App_Start.NinjectWebCommon), "Stop")]
namespace Nintex.UrlShortener.UI.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Nintex.UrlShortener.Model.Entities;
    using Nintex.UrlShortener.DataAccess;
    using Nintex.UrlShortener.DataAccess.BuisnessLayer;

    /// <summary>
    /// NINJECT Web Common
    /// </summary>
    public static class NinjectWebCommon
    {
        /// <summary>
        /// Boot STRAPPER
        /// </summary>
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// start NINJECT Web Common
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);

                // Install our Ninject-based IDependencyResolver into the Web API config
                DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Bind<Model.ApplicationDbContext>().ToSelf().InRequestScope();
            kernel.Bind<UrlShortenerContext>().ToSelf().InRequestScope();
            kernel.Bind<IRepository>().To<EFRepository<UrlShortenerContext>>().InRequestScope();
            kernel.Bind<IReadOnlyRepository>().To<EFReadOnlyRepository<UrlShortenerContext>>().InRequestScope();
            kernel.Bind<IUrlShortenerService>().To<UrlShortenerService>().InRequestScope();

        }

        /// <summary>
        /// NINJECT Dependency Resolver
        /// </summary>
        private class NinjectDependencyResolver : IDependencyResolver
        {
            /// <summary>
            /// internal KERNAL
            /// </summary>
            private readonly IKernel kernel;

            /// <summary>
            /// Initializes a new instance of the <see cref="NinjectDependencyResolver"/> class.
            /// </summary>
            /// <param name="kernel">interface KERNAL</param>
            public NinjectDependencyResolver(IKernel kernel)
            {
                this.kernel = kernel;
            }

            /// <summary>
            /// Get Service
            /// </summary>
            /// <param name="serviceType">service Type</param>
            /// <returns>object service Type</returns>
            public object GetService(System.Type serviceType)
            {
                return this.kernel.TryGet(serviceType);
            }

            /// <summary>
            /// Get Services
            /// </summary>
            /// <param name="serviceType">service Type</param>
            /// <returns>list of object</returns>
            public IEnumerable<object> GetServices(System.Type serviceType)
            {
                try
                {
                    return this.kernel.GetAll(serviceType);
                }
                catch (Exception)
                {
                    return new List<object>();
                }
            }
        }
    }
}