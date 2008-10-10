using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using MyOrm.Metadata;

namespace MyOrm
{
    public class MyOrmConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// 默认数据库连接配置
        /// </summary>
        [ConfigurationProperty("ConnectionSettings")]
        public ConnectionStringSettings ConnectionSettings
        {
            get { return (ConnectionStringSettings)this["ConnectionSettings"]; }
            set { this["ConnectionSettings"] = value; }
        }

        [ConfigurationProperty("TableInfoProvider", DefaultValue = "MyOrm.Metadata.AttibuteTableInfoProvider", IsRequired = true)]
        public string TableInfoProvider
        {
            get { return (string)this["TableInfoProvider"]; }
            set { this["TableInfoProvider"] = value; }
        }
    }
    /// <summary>
    /// 配置
    /// </summary>
    public static class Configuration
    {
        private static IDbConnection defaultConnection;
        private static TableInfoProvider tableInfoProvider;

        /// <summary>
        /// 默认数据库连接
        /// </summary>
        public static IDbConnection DefaultConnection
        {
            get
            {

                if (defaultConnection == null)
                {
                    MyOrmConfigurationSection config = ConfigurationManager.GetSection("MyOrm") as MyOrmConfigurationSection;
                    defaultConnection = DbProviderFactories.GetFactory(config.ConnectionSettings.ProviderName).CreateConnection();
                    defaultConnection.ConnectionString = config.ConnectionSettings.ConnectionString;
                }
                return defaultConnection;
            }
            set
            {
                defaultConnection = value;
            }
        }

        /// <summary>
        /// 表信息Provider
        /// </summary>
        public static TableInfoProvider TableInfoProvider
        {
            get
            {
                if (tableInfoProvider == null)
                {
                    MyOrmConfigurationSection config = ConfigurationManager.GetSection("MyOrm") as MyOrmConfigurationSection;
                    tableInfoProvider = Activator.CreateInstance(Type.GetType(config.TableInfoProvider, true)) as TableInfoProvider;
                }
                return tableInfoProvider;
            }
        }
    }
}
