using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace MyOrm.Common
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Utility
    {
        public static T GetAttribute<T>(MemberInfo memberInfo) where T : System.Attribute
        {
            object[] atts = memberInfo.GetCustomAttributes(typeof(T), true);
            return atts.Length > 0 ? (T)atts[0] : null;
        }

        /// <summary>
        /// 数据类型转换为DbType
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <returns></returns>
        public static DbType GetDbType(Type type)
        {
            if (Nullable.GetUnderlyingType(type) != null) type = Nullable.GetUnderlyingType(type);
            if (type == typeof(Byte)) return DbType.Byte;
            else if (type == typeof(Byte[])) return DbType.Binary;
            else if (type == typeof(Boolean)) return DbType.Boolean;
            else if (type == typeof(DateTime)) return DbType.DateTime;
            else if (type == typeof(Decimal)) return DbType.Decimal;
            else if (type == typeof(Double)) return DbType.Double;
            else if (type == typeof(Guid)) return DbType.Guid;
            else if (type == typeof(Int16)) return DbType.Int16;
            else if (type.IsEnum || type == typeof(Int32)) return DbType.Int32;
            else if (type == typeof(Int64)) return DbType.Int64;
            else if (type == typeof(SByte)) return DbType.SByte;
            else if (type == typeof(Single)) return DbType.Single;
            else if (type == typeof(String)) return DbType.String;
            else if (type == typeof(TimeSpan)) return DbType.Time;
            else if (type == typeof(UInt16)) return DbType.UInt16;
            else if (type == typeof(UInt32)) return DbType.UInt32;
            else if (type == typeof(UInt64)) return DbType.UInt64;
            else if (type == typeof(DateTimeOffset)) return DbType.DateTimeOffset;
            else return DbType.Object;
        }

        public static int GetDefaultLength(DbType columnType)
        {
            switch (columnType)
            {
                case DbType.String: 
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.StringFixedLength: return 255;
                default: return 0;
            }
        }
    }
}
