using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MyOrm
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// 数据类型转换为DbType
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <returns></returns>
        public static DbType ConvertToDbType(Type type)
        {
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
            else if (type == typeof(DateTime)) return DbType.Time;
            else if (type == typeof(UInt16)) return DbType.UInt16;
            else if (type == typeof(UInt32)) return DbType.UInt32;
            else if (type == typeof(UInt64)) return DbType.UInt64;
            else if (type == typeof(DateTimeOffset)) return DbType.DateTimeOffset;
            else return DbType.Object;
        }
    }
}
