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
    public class OrmConfigurationSection : ConfigurationSection
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
    /// <summary>
    /// 配置
    /// </summary>
    public static class DefaultConfiguration
    {
        private static IDbConnection defaultConnection;
        private static TableInfoProvider tableInfoProvider;
        private static OrmConfigurationSection configSection;

        /// <summary>
        /// 配置项
        /// </summary>
        public static OrmConfigurationSection ConfigSection
        {
            get
            {
                if (configSection == null) configSection = ConfigurationManager.GetSection("MyOrm") as OrmConfigurationSection;
                return configSection;
            }
        }

        /// <summary>
        /// 默认数据库连接
        /// </summary>
        public static IDbConnection DefaultConnection
        {
            get
            {
                if (defaultConnection == null)
                {
                    ConnectionStringSettings connectionSetting = String.IsNullOrEmpty(ConfigSection.ConnectionStringName) ? ConfigurationManager.ConnectionStrings[0] : ConfigurationManager.ConnectionStrings[ConfigSection.ConnectionStringName];
                    defaultConnection = DbProviderFactories.GetFactory(connectionSetting.ProviderName).CreateConnection();
                    defaultConnection.ConnectionString = connectionSetting.ConnectionString;
                }
                return defaultConnection;
            }
        }

        /// <summary>
        /// 表信息提供者
        /// </summary>
        public static TableInfoProvider TableInfoProvider
        {
            get
            {
                if (tableInfoProvider == null)
                {
                    tableInfoProvider = Activator.CreateInstance(Type.GetType(ConfigSection.TableInfoProvider, true)) as TableInfoProvider;
                }
                return tableInfoProvider;
            }
        }

        /// <summary>
        /// 是否使用自动管理的Command，包括打开关闭数据库、设置事务
        /// </summary>
        public static bool UseAutoCommand
        {
            get
            {
                return ConfigSection.UseAutoCommand;
            }
        }
    }
}
