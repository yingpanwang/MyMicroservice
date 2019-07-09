using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using MyMicroservice.Ocelot.Config;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Swashbuckle.AspNetCore.Swagger;

namespace MyMicroservice.Ocelot
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
            var identityBuilder = services.AddAuthentication();
            services.AddAuthentication("Bearer");
            IdentityServerConfig identityServerConfig = new IdentityServerConfig();
            Configuration.Bind("IdentityServerConfig", identityServerConfig);
            if (identityServerConfig != null)
            {
                foreach (var resource in identityServerConfig.Resources)
                {
                    identityBuilder.AddIdentityServerAuthentication(resource.Key, options =>
                     {
                         options.Authority = $"{identityServerConfig.AuthorityIP}:{identityServerConfig.Port}";
                         options.RequireHttpsMetadata = false;
                         options.ApiName = resource.Key;
                         options.SupportedTokens = SupportedTokens.Both;
                     });
                }
            }

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
                    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                    var xmlPath = Path.Combine(basePath,"Microservice.Ocelot.xml");
                    if (File.Exists(xmlPath))
                    {
                        opt.IncludeXmlComments(xmlPath);
                    }
                });
            }
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

           // app.UseAuthentication();
            app.UseMvc();
            //启用中间件服务生成Swagger作为JSON终结点
            //app.UseSwagger(opt =>
            //{
            //    opt.RouteTemplate = "{documentName}/swagger.json";
            //});
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                var downApis = Configuration["Swagger:DownApis"].Split(',');
                foreach (var api in downApis)
                {
                    c.SwaggerEndpoint($"/{api}/swagger.json", api);
                }
            });

            app.UseOcelot().Wait();
        }
    }
}