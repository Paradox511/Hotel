﻿using Application;
using Application.Features.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Persistence;
using System.Reflection;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Application;


namespace WebAPI
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
			services.AddApplication();

			services.AddPersistence(Configuration);
			// services.AddPersistence(Configuration);
			services.AddControllers();
			#region Swagger
			services.AddSwaggerGen(c =>
			{
				c.IncludeXmlComments(string.Format(@"{0}\OnionArchitecture.xml", System.AppDomain.CurrentDomain.BaseDirectory));
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "OnionArchitecture",
				});

			});
			#endregion
			services.AddDbContext<HotelDBContext>(options =>
				 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly);
				cfg.RegisterServicesFromAssembly(typeof(Application.DependencyInjection).Assembly);
			});
			services.AddScoped(typeof(CreateCommand<>), typeof(CreateCommand<>));
			services.AddScoped(typeof(DeleteCommand<>), typeof(DeleteCommand<>));
			services.AddScoped(typeof(UpdateCommand<>), typeof(UpdateCommand<>));



			#region API Versioning
			// Add API Versioning to the Project
			services.AddApiVersioning(config =>
			{
				// Specify the default API Version as 1.0
				config.DefaultApiVersion = new ApiVersion(1, 0);
				// If the client hasn't specified the API version in the request, use the default API version number
				config.AssumeDefaultVersionWhenUnspecified = true;
				// Advertise the API versions supported for the particular endpoint
				config.ReportApiVersions = true;
			});
			#endregion

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			#region Swagger
			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();
			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnionArchitecture");
			});
			#endregion
		}
	}
}