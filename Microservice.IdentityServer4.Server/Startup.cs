using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microservice.Common.Ioc;
using Microservice.Common.ApiMiddlewares;
using Microservice.IdentityServer4.DataProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Microservice.IdentityServer4.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var currentDb = Configuration["CurrentDatabase"];
            var connStr = Configuration.GetConnectionString(string.IsNullOrEmpty(currentDb) ? "MSSQL" : currentDb);
            services.AddDbContext<ISTDbContext>(opt =>
            {
                opt.UseSqlServer(connStr);
            }, ServiceLifetime.Singleton);
            //services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
            //services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IUserService, UserService>();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddIdentityServer()
              .AddDeveloperSigningCredential()
              .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResourceResources())
              .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
              .AddInMemoryClients(IdentityServerConfig.GetClients())
              .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
              .AddProfileService<ProfileService>();
            
            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAllOrigin", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });
            return RegisterAutofac(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddNLog();
            app.UseCors("AllowAllOrigin");
            app.UseResponseCollection();
            app.UseIdentityServer();
            app.UseMvc();
        }
        private IServiceProvider RegisterAutofac(IServiceCollection services)
        {

            //实例化Autofac容器
            var builder = new ContainerBuilder();
            
            //将Services中的服务填充到Autofac中
            builder.Populate(services);
            //新模块组件注册    
            DefaultModule.Configuration = Configuration;
            builder.RegisterModule<DefaultModule>();
            
            //创建容器
            var Container = builder.Build();
            //第三方IOC接管 core内置DI容器 
            return new AutofacServiceProvider(Container);
        }

    }
}