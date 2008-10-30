using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using MyOrm.Metadata;

namespace MyOrm
{
    /// <summary>
    /// MyOrm配置项
    /// </summary>
    public class MyOrmConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// 默认数据库连接配置
        /// </summary>
        [ConfigurationProperty("DefaultConnection")]
        public ConnectionStringSettings DefaultConnection
        {
            get { return (ConnectionStringSettings)this["DefaultConnection"]; }
            set { this["DefaultConnection"] = value; }
        }

        /// <summary>
        /// 表信息提供类型名
        /// </summary>
        [ConfigurationProperty("Provider", DefaultValue = "MyOrm.Metadata.AttibuteTableInfoProvider, MyOrm.Attribute")]
        public string TableInfoProvider
        {
            get { return (string)this["Provider"]; }
            set { this["Provider"] = value; }
        }

        /// <summary>
        /// 是否使用自动管理的Command，包括打开关闭数据库、设置事务
        /// </summary>
        [ConfigurationProperty("UseAutoCommand", DefaultValue = false)]
        public bool UseAutoCommand
        {
            get { return (bool)this["UseAutoCommand"]; }
            set { this["UseAutoCommand"] = value; }
        }
    }
    /// <summary>
    /// 配置
    /// </summary>
    public static class Configuration
    {
        private static IDbConnection defaultConnection;
        private static TableInfoProvider tableInfoProvider;
        private static MyOrmConfigurationSection configSection;

        /// <summary>
        /// 配置项
        /// </summary>
        public static MyOrmConfigurationSection ConfigSection
        {
            get
            {
                if (configSection == null) configSection = ConfigurationManager.GetSection("MyOrm") as MyOrmConfigurationSection;
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
                    ConnectionStringSettings connectionSetting = ConfigSection.DefaultConnection;
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
