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
        [ConfigurationProperty("DefaultConnection")]
        public ConnectionStringSettings DefaultConnection
        {
            get { return (ConnectionStringSettings)this["DefaultConnection"]; }
            set { this["DefaultConnection"] = value; }
        }

        [ConfigurationProperty("Provider", DefaultValue = "MyOrm.Metadata.AttibuteTableInfoProvider, MyOrm.Attribute", IsRequired = true)]
        public string TableInfoProvider
        {
            get { return (string)this["Provider"]; }
            set { this["Provider"] = value; }
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
                    defaultConnection = DbProviderFactories.GetFactory(config.DefaultConnection.ProviderName).CreateConnection();
                    defaultConnection.ConnectionString = config.DefaultConnection.ConnectionString;
                }
                return defaultConnection;
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
