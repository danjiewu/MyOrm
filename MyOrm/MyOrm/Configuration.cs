using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using MyOrm.Common;

namespace MyOrm
{
    /// <summary>
    /// MyOrm������
    /// </summary>
    public class OrmConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Ĭ�����ݿ���������
        /// </summary>
        [ConfigurationProperty("ConnectionStringName")]
        public string ConnectionStringName
        {
            get { return (string)this["ConnectionStringName"]; }
            set { this["ConnectionStringName"] = value; }
        }

        /// <summary>
        /// ����Ϣ�ṩ������
        /// </summary>
        [ConfigurationProperty("Provider", DefaultValue = "MyOrm.Common.AttibuteTableInfoProvider, MyOrm.Common")]
        public string TableInfoProvider
        {
            get { return (string)this["Provider"]; }
            set { this["Provider"] = value; }
        }

        /// <summary>
        /// �Ƿ�ʹ���Զ������Command�������򿪹ر����ݿ⡢��������
        /// </summary>
        [ConfigurationProperty("UseAutoCommand", DefaultValue = true)]
        public bool UseAutoCommand
        {
            get { return (bool)this["UseAutoCommand"]; }
            set { this["UseAutoCommand"] = value; }
        }
    }
    /// <summary>
    /// ����
    /// </summary>
    public static class DefaultConfiguration
    {
        private static IDbConnection defaultConnection;
        private static TableInfoProvider tableInfoProvider;
        private static OrmConfigurationSection configSection;

        /// <summary>
        /// ������
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
        /// Ĭ�����ݿ�����
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
