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
        /// Ĭ�����ݿ���������
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
    /// ����
    /// </summary>
    public static class Configuration
    {
        private static IDbConnection defaultConnection;
        private static TableInfoProvider tableInfoProvider;

        /// <summary>
        /// Ĭ�����ݿ�����
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
        /// ����ϢProvider
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
