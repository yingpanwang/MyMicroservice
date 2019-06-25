using Autofac;
using Microservice.IdentityServer4.DataProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;
using Module = Autofac.Module;

namespace Microservice.Common.Ioc
{
    public class DefaultModule : Module
    {
        public static IConfiguration Configuration { get; set; }
        public DefaultModule()
        {
        }

        //重写Autofac管道Load方法，在这里注册注入
        protected override void Load(ContainerBuilder builder)
        {
            var connectionString = Configuration.GetConnectionString("MSSQL");

            // 首先注册 options，供 DbContext 服务初始化使用
            //builder.Register(c =>
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<ISTDbContext>();
            //    optionsBuilder.UseSqlServer(connectionString, opt =>
            //    {
            //        opt.MigrationsAssembly("Microservice.IdentityServer4.DataProvider");
            //    });
            //    return optionsBuilder.Options;
            //}).InstancePerLifetimeScope();

            //builder.RegisterType<ISTDbContext>().AsSelf().InstancePerLifetimeScope();

            //注册Repository中的对象,Repository中的类要以Repository结尾，否则注册失败
            builder.RegisterAssemblyTypes(GetAssemblyByName("Microservice.IdentityServer4.Repositories")).Where(a => a.Name.EndsWith("Repository")).AsImplementedInterfaces();
            //注册Service中的对象,Service中的类要以Service结尾，否则注册失败
            builder.RegisterAssemblyTypes(GetAssemblyByName("Microservice.IdentityServer4.Services")).Where(a => a.Name.EndsWith("Service")).AsImplementedInterfaces();
        }
        /// <summary>
        /// 根据程序集名称获取程序集
        /// </summary>
        /// <param name="AssemblyName">程序集名称</param>
        /// <returns></returns>
        public static Assembly GetAssemblyByName(String AssemblyName)
        {
            return Assembly.Load(AssemblyName);
        }
    }
}
