using System;
using Microsoft.EntityFrameworkCore;
using Vic.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Vic.Core.Data
{

    public class TradeDbContextFactory : IDesignTimeDbContextFactory<TradeDbContext>
    {
        public IConfigurationRoot Configuration { get; }
        public TradeDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TradeDbContext>();
            //var sqlConnectionString = Configuration.GetConnectionString("Default");
            //if (string.IsNullOrEmpty(sqlConnectionString))
            //{
            //    sqlConnectionString = "Data Source=192.168.20.103;Initial Catalog=VicCore;Integrated Security=False;User ID=sa;Password=Ijwe856dfk7UHJ56d;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //}
            //optionsBuilder.UseSqlServer(sqlConnectionString);

            return new TradeDbContext(optionsBuilder.Options);
        }
    }

    public class ReadOnlyTradeDbContextFactory : IDesignTimeDbContextFactory<ReadOnlyTradeDbContext>
    {
        public IConfigurationRoot Configuration { get; }
        public ReadOnlyTradeDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReadOnlyTradeDbContext>();
            //var sqlConnectionString = Configuration.GetConnectionString("Default");
            //if (string.IsNullOrEmpty(sqlConnectionString))
            //{
            //    sqlConnectionString = "Data Source=192.168.20.103;Initial Catalog=VicCore;Integrated Security=False;User ID=sa;Password=Ijwe856dfk7UHJ56d;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //}
            //optionsBuilder.UseSqlServer(sqlConnectionString);

            return new ReadOnlyTradeDbContext(optionsBuilder.Options);
        }
    }

    public class TradeDbContext : DbContext
    {
        public TradeDbContext(DbContextOptions<TradeDbContext> options) : base(options)
        {
        }

        #region 后台管理
        public DbSet<Department> Departments { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }
        #endregion

        #region 平台配置
        public DbSet<MailConfig> MailConfig { get; set; }
        public DbSet<MailTemplate> MailTemplate { get; set; }
        public DbSet<PlateSetting> PlateSetting { get; set; }
        public DbSet<ShortMsgConfig> ShortMsgConfig { get; set; }
        public DbSet<ShortMsgTemplate> ShortMsgTemplate { get; set; }
        public DbSet<VerifyCode> VerifyCode { get; set; }
        public DbSet<SysLog> SysLog { get; set; }
        public DbSet<PushService> PushService { get; set; }
        public DbSet<LanguageType> LanguageType { get; set; }
        public DbSet<SiteSetting> SiteSetting { get; set; }
        #endregion

        #region 类容管理
        public DbSet<Article> Article { get; set; }
        public DbSet<ArticleType> ArticleType { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<UserMenu> HomeMenu { get; set; }
        #endregion

        #region 业务层
        public DbSet<CustomerUser> CustomerUser { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //UserRole关联配置
            builder.Entity<UserRole>()
              .HasKey(ur => new { ur.UserId, ur.RoleId });

            //文章
            builder.Entity<Article>().Property(p => p.Content).HasColumnType("ntext");


            //RoleMenu关联配置
            builder.Entity<RoleMenu>()
              .HasKey(rm => new { rm.RoleId, rm.MenuId });

            builder.Entity<Role>()
                        .HasMany(t => t.RoleMenus);

            builder.Entity<User>()
                       .HasMany(t => t.UserRoles);

            //启用Guid主键类型扩展
            //builder.HasPostgresExtension("uuid-ossp");

            base.OnModelCreating(builder);
        }
    }

    public class ReadOnlyTradeDbContext : DbContext
    {
        public ReadOnlyTradeDbContext(DbContextOptions<ReadOnlyTradeDbContext> options) : base(options)
        {
        }

        #region 后台管理
        public DbSet<Department> Departments { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }
        #endregion

        #region 平台配置
        public DbSet<MailConfig> MailConfig { get; set; }
        public DbSet<MailTemplate> MailTemplate { get; set; }
        public DbSet<PlateSetting> PlateSetting { get; set; }
        public DbSet<ShortMsgConfig> ShortMsgConfig { get; set; }
        public DbSet<ShortMsgTemplate> ShortMsgTemplate { get; set; }
        public DbSet<VerifyCode> VerifyCode { get; set; }
        public DbSet<SysLog> SysLog { get; set; }
        public DbSet<PushService> PushService { get; set; }
        public DbSet<LanguageType> LanguageType { get; set; }
        public DbSet<SiteSetting> SiteSetting { get; set; }
        #endregion

        #region 类容管理
        public DbSet<Article> Article { get; set; }
        public DbSet<ArticleType> ArticleType { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<UserMenu> HomeMenu { get; set; }
        #endregion

        #region 业务层
        public DbSet<CustomerUser> CustomerUser { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //UserRole关联配置
            builder.Entity<UserRole>()
              .HasKey(ur => new { ur.UserId, ur.RoleId });

            //文章
            builder.Entity<Article>().Property(p => p.Content).HasColumnType("ntext");


            //RoleMenu关联配置
            builder.Entity<RoleMenu>()
              .HasKey(rm => new { rm.RoleId, rm.MenuId });

            builder.Entity<Role>()
                        .HasMany(t => t.RoleMenus);

            builder.Entity<User>()
                       .HasMany(t => t.UserRoles);

            //启用Guid主键类型扩展
            //builder.HasPostgresExtension("uuid-ossp");

            base.OnModelCreating(builder);
        }
    }
}
