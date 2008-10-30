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
        [ConfigurationProperty("Provider", DefaultValue = "MyOrm.Metadata.AttibuteTableInfoProvider, MyOrm.Attribute")]
        public string TableInfoProvider
        {
            get { return (string)this["Provider"]; }
            set { this["Provider"] = value; }
        }

        /// <summary>
        /// �Ƿ�ʹ���Զ������Command�������򿪹ر����ݿ⡢��������
        /// </summary>
        [ConfigurationProperty("UseAutoCommand", DefaultValue = false)]
        public bool UseAutoCommand
        {
            get { return (bool)this["UseAutoCommand"]; }
            set { this["UseAutoCommand"] = value; }
        }
    }
    /// <summary>
    /// ����
    /// </summary>
    public static class Configuration
    {
        private static IDbConnection defaultConnection;
        private static TableInfoProvider tableInfoProvider;
        private static MyOrmConfigurationSection configSection;

        /// <summary>
        /// ������
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
        /// Ĭ�����ݿ�����
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
        /// ����Ϣ�ṩ��
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
        /// �Ƿ�ʹ���Զ������Command�������򿪹ر����ݿ⡢��������
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
