using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using MyOrm.Xml;

namespace MyOrm.Metadata
{
    /// <summary>
    /// ����Xml�����ļ��ṩ����Ϣ
    /// </summary>
    public class XmlTableInfoProvider : TableInfoProvider
    {
        public override TableInfo GetTableInfo(Type objectType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override TableJoinInfo[] GetTableJoins(Type objectType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ColumnInfo GetColumnInfo(PropertyInfo property)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
