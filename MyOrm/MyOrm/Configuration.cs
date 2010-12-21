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
    public class MyOrmSection : ConfigurationSection
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

    public static class Configuration
    {
        /// <summary>
        /// ������
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
        /// Ĭ�����ݿ�����
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
        /// Ĭ�ϵı���Ϣ�ṩ��
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
        /// �Ƿ�ʹ���Զ������Command�������򿪹ر����ݿ⡢��������
        /// </summary>
        public static bool UseAutoCommand
        {
            get { return ConfigSection.UseAutoCommand; }
        }
    }
}
