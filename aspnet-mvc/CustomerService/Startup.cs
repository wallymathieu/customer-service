using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Customers
{
    public class Startup
    {
        private SwaggerConfig _swagger=new SwaggerConfig();

        class SwaggerConfig
        {
            ///
            public void Configure(IApplicationBuilder app, IHostingEnvironment env)
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint
                app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                    c.EnableDeepLinking();
                });
            }

            ///
            public virtual void ConfigureServices(IServiceCollection services)
            {
                services.AddSwaggerGen(options => { });

                services.ConfigureSwaggerGen(options =>
                {
                    var webAssembly = typeof(Startup).GetTypeInfo().Assembly;
                    var informationalVersion =
                        (webAssembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute))
                            as AssemblyInformationalVersionAttribute[])?.First()?.InformationalVersion;

                    options.SwaggerDoc("v1", new Info
                    {
                        Version = informationalVersion ?? "dev",
                        Title = "API",
                        Description = "Some API",
                        TermsOfService = "See license agreement"
                    });


                    //Determine base path for the application.
                    var basePath = PlatformServices.Default.Application.ApplicationBasePath;

                    //Set the comments path for the swagger json and ui.
                    var xmlPath = Path.Combine(basePath, typeof(Startup).Assembly.GetName().Name + ".xml");
                    if (File.Exists(xmlPath))
                        options.IncludeXmlComments(xmlPath);
                });
            }
        }
        
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public virtual void ConfigureServices(IServiceCollection services)
        {
            _swagger.ConfigureServices(services);
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddMvc(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.InputFormatters.Add(new XmlSerializerInputFormatter(config));
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            _swagger.Configure(app, env);

            app.UseMvcWithDefaultRoute();
        }
    }
}
