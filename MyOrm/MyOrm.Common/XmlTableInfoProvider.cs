using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace MyOrm.Common
{
    public class XmlTableInfoProvider : TableInfoProvider
    {
        private static XmlSerializer tableSerializer = new XmlSerializer(typeof(TableNode));
        private static XmlSerializer tableViewSerializer = new XmlSerializer(typeof(TableViewNode));

        private Dictionary<Type, TableDefinition> tableInfoCache = new Dictionary<Type, TableDefinition>();
        private Dictionary<PropertyInfo, ColumnDefinition> columnCache = new Dictionary<PropertyInfo, ColumnDefinition>();
        private Dictionary<Type, TableView> tableViewCache = new Dictionary<Type, TableView>();
        
        public override TableDefinition GetTableDefinition(Type objectType)
        {
            return tableInfoCache[objectType];
        }

        public override TableView GetTableView(Type objectType)
        {
            return tableViewCache[objectType];
        }

        private ColumnDefinition GetColumnDefinition(PropertyInfo property)
        {
            return columnCache[property];
        }

        /// <summary>
        /// 从对象的 XML 表示形式生成该对象。
        /// </summary>
        /// <param name="reader">对象从中进行反序列化的 System.Xml.XmlReader 流。</param>
        public void ReadXml(XmlReader reader)
        {           
        }

        /// <summary>
        /// 将对象转换为其 XML 表示形式。
        /// </summary>
        /// <param name="writer">对象要序列化为的 System.Xml.XmlWriter 流。</param>
        public void WriteXml(XmlWriter writer)
        {
            foreach (TableDefinition table in tableInfoCache.Values)
            {

            }
        }
    }
}
