using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using MyOrm.Common;

namespace MyOrm
{
    /// <summary>
    /// MyOrm配置项
    /// </summary>
    public class MyOrmSection : ConfigurationSection
    {
        /// <summary>
        /// 默认数据库连接配置
        /// </summary>
        [ConfigurationProperty("ConnectionStringName")]
        public string ConnectionStringName
        {
            get { return (string)this["ConnectionStringName"]; }
            set { this["ConnectionStringName"] = value; }
        }

        /// <summary>
        /// 表信息提供类型名
        /// </summary>
        [ConfigurationProperty("Provider", DefaultValue = "MyOrm.Common.AttibuteTableInfoProvider, MyOrm.Common")]
        public string TableInfoProvider
        {
            get { return (string)this["Provider"]; }
            set { this["Provider"] = value; }
        }

        /// <summary>
        /// 是否使用自动管理的Command，包括打开关闭数据库、设置事务
        /// </summary>
        [ConfigurationProperty("UseAutoCommand", DefaultValue = true)]
        public bool UseAutoCommand
        {
            get { return (bool)this["UseAutoCommand"]; }
            set { this["UseAutoCommand"] = value; }
        }
    }

    public static class Configuration
    {
        /// <summary>
        /// 配置项
        /// </summary>
        public static MyOrmSection ConfigSection
        {
            get
            {
                return ConfigurationManager.GetSection("MyOrm") as MyOrmSection;
            }
        }

        private static IDbConnection defaultConnection;
        /// <summary>
        /// 默认数据库连接
        /// </summary>
        public static IDbConnection DefaultConnection
        {
            get
            {
                if (defaultConnection == null)
                {
                    string connectionStringName = ConfigSection.ConnectionStringName;
                    ConnectionStringSettings connectionSetting = String.IsNullOrEmpty(connectionStringName) ? ConfigurationManager.ConnectionStrings[0] : ConfigurationManager.ConnectionStrings[connectionStringName];
                    defaultConnection = DbProviderFactories.GetFactory(connectionSetting.ProviderName).CreateConnection();
                    defaultConnection.ConnectionString = connectionSetting.ConnectionString;
                }
                return defaultConnection;
            }
            set { defaultConnection = value; }
        }

        private static TableInfoProvider defaultProvider;
        /// <summary>
        /// 默认的表信息提供者
        /// </summary>
        public static TableInfoProvider DefaultProvider
        {
            get
            {
                if (defaultProvider == null)
                {
                    defaultProvider = (TableInfoProvider)Activator.CreateInstance(Type.GetType(ConfigSection.TableInfoProvider, true, true));
                }
                return defaultProvider;
            }
            set { defaultProvider = value; }
        }

        /// <summary>
        /// 是否使用自动管理的Command，包括打开关闭数据库、设置事务
        /// </summary>
        public static bool UseAutoCommand
        {
            get { return ConfigSection.UseAutoCommand; }
        }
    }
}
