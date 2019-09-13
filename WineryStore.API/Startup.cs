using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using WineryStore.Persistence;
using WineryStore.Persistence.Datastore;

namespace WineryStore.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(builder =>
				{
					builder.WithOrigins("http://localhost:3000")
					.AllowAnyHeader()
					.AllowAnyHeader();
				});
			});

			services.AddAutoMapper(typeof(Startup));

			//https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2#service-lifetimes
			services.AddScoped<IWineryRepository, WineryRepository>();
			services.AddScoped<IWineRepository, WineRepository>();
			//services.AddScoped<IWineryDataStore, InMemoryWineryDataStore>();
			//services.AddScoped<IWineDataStore, InMemoryWineDataStore>();
			services.AddScoped<IWineryDataStore, WineryDataStore>();
			services.AddScoped<IWineDataStore, WineDataStore>();

			//Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 2.1.1
			services.AddDbContext<WineryContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("WineryConnection")
			));

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseCors();
			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}

	public class DesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
	{
		public T CreateDbContext(string[] args)
		{
			IConfigurationRoot configurationRoot = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var builder = new DbContextOptionsBuilder<T>();
			builder.UseSqlServer(configurationRoot.GetConnectionString("WineryConnection"));

			var dbContext = (T)Activator.CreateInstance(typeof(T), builder.Options);
			return dbContext;
		}
	}

	public class WineryDesignTimeDbContextFactory : DesignTimeDbContextFactory<WineryContext> { }

}