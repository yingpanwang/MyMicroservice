#region 类信息

/**************************************************************************
*
*   =================================
*   CLR版本    ：4.0.30319.42000
*   命名空间    ：Microservice.IdentityServer4.Entity
*   文件名称    ：ISTDbContext.cs
*   =================================
*   创 建 者    ：王盈攀
*   创建日期    ：2019-6-7 22:22:48
*   邮箱        1029853412@qq.com
*   功能描述    ：
*   使用说明    ：
*   =================================
*   修改者    ：
*   修改日期    ：
*   修改内容    ：
*   =================================
*
***************************************************************************/

#endregion 类信息

using JetBrains.Annotations;
using Microservice.IdentityServer4.DataProvider;
using Microservice.IdentityServer4.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.IdentityServer4.DataProvider
{
    public class ISTDbContext : DbContext
    {
        public ISTDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}