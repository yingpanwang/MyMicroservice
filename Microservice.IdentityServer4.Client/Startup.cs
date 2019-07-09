using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microservice.IdentityServer4.Client.RPCApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using WebApiClient;

namespace Microservice.IdentityServer4.Client
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
            services
                .AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5001";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api1";
                });
            // 配置Swagger
            if (Configuration["Swagger:Enable"] == bool.TrueString)
            {
                services.AddSwaggerGen(opt =>
                {
                    opt.SwaggerDoc(Configuration["Swagger:Name"],
                        new Info
                        {
                            Title = Configuration["Swagger:Title"],
                            Version = Configuration["Swagger:Version"]
                        });
                    var basePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;
                    var xmlPath = Path.Combine(basePath + "Microservice.IdentityServer4.Client.xml");
                    if (File.Exists(xmlPath))
                    {
                        opt.IncludeXmlComments(xmlPath);
                    }
                });
            }
            HttpApi.Register<IUserApi>().ConfigureHttpApiConfig(c =>
            {
                c.HttpHost = new Uri("http://localhost:5001/");
                c.FormatOptions.DateTimeFormat = DateTimeFormats.ISO8601_WithMillisecond;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger(opt =>
            {
                opt.RouteTemplate = "{documentName}/swagger.json";
            });
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/{Configuration["Swagger:Name"]}/swagger.json", Configuration["Swagger:Name"]);
            });

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}