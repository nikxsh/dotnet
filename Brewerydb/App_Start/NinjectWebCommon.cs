using Brewerydb.Adaptor;
using Brewerydb.Contracts;
using Brewerydb.External;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using System.Web.Http;
using System;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(YourProjectName.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(YourProjectName.Web.App_Start.NinjectWebCommon), "Stop")]

namespace YourProjectName.Web.App_Start
{
	public static class NinjectWebCommon
	{
		private static readonly Bootstrapper bootstrapper = new Bootstrapper();

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			bootstrapper.Initialize(CreateKernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
		{
			bootstrapper.ShutDown();
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

				RegisterServices(kernel);

				// Install our Ninject-based IDependencyResolver into the Web API config
				GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

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
			kernel.Bind<IBeerInfoAdapter>().To<BeerInfoAdapter>();
			kernel.Bind<IWebClient>().To<WebClient>();
		}
	}
}