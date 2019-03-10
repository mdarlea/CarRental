namespace CarRental.Api
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
	using AutoMapper;
	using CarRental.Api.Extensions;
	using CarRental.Api.Models;
	using CarRental.Data.UnitOfWork;
	using CarRental.Identity.Api.Extensions;
	using FluentValidation.AspNetCore;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Diagnostics;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using Microsoft.Extensions.Logging;
	
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
			services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

			services.AddCors();
			services.AddSecurity(Configuration);

			services.AddDbContext<CarRentalUnitOfWork>(optionsBuilder => {
				optionsBuilder					
					.UseMySQL(Configuration.GetConnectionString("CarRentalDataSource"));
			}, ServiceLifetime.Scoped);

			services.AddAutoMapper();

			services.AddMvc()
				.AddFluentValidation(fv => {
					fv.RegisterValidatorsFromAssemblyContaining<AddressModel>();
					// fv.RegisterValidatorsFromAssemblyContaining<CreateEventCommand>();
				}); ;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
		{
			loggerFactory.AddLog4Net();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

				app.UseCors(builder => builder
				.WithOrigins("https://localhost:4200")
				.AllowCredentials()
				.AllowAnyHeader()
				.AllowAnyMethod());
			}

			app.UseExceptionHandler(
				builder =>
				{
					builder.Run(
						async context =>
						{
							context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
							context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

							var error = context.Features.Get<IExceptionHandlerFeature>();
							if (error != null)
							{
								context.Response.AddApplicationError(error.Error.Message);
								await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
							}
						});
				});


			app.UseAuthentication();
			app.UseDefaultFiles();			
			app.UseMvc();
		}
	}
}
