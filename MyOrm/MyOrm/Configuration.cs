using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using MyOrm.Metadata;

namespace MyOrm
{
    /// <summary>
    /// MyOrm������
    /// </summary>
    public class MyOrmConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Ĭ�����ݿ���������
        /// </summary>
        [ConfigurationProperty("DefaultConnection")]
        public ConnectionStringSettings DefaultConnection
        {
            get { return (ConnectionStringSettings)this["DefaultConnection"]; }
            set { this["DefaultConnection"] = value; }
        }

        /// <summary>
        /// ����Ϣ�ṩ������
        /// </summary>
        [ConfigurationProperty("Provider", DefaultValue = "MyOrm.Metadata.AttibuteTableInfoProvider, MyOrm.Attribute", IsRequired = true)]
        public string TableInfoProvider
        {
            get { return (string)this["Provider"]; }
            set { this["Provider"] = value; }
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
                    defaultConnection = DbProviderFactories.GetFactory(config.DefaultConnection.ProviderName).CreateConnection();
                    defaultConnection.ConnectionString = config.DefaultConnection.ConnectionString;
                }
                return defaultConnection;
            }
        }

        /// <summary>
        /// ����Ϣ�ṩ��
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
