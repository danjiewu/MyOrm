using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace MyOrm.Common
{
    /// <summary>
    /// ������
    /// </summary>
    public static class Utility
    {
        private static Dictionary<Type, DbType> typeToDbTypeCache = new Dictionary<Type, DbType>();

        static Utility()
        {
            typeToDbTypeCache[typeof(Enum)] = DbType.Int32;
            typeToDbTypeCache[typeof(Byte)] = DbType.Byte;
            typeToDbTypeCache[typeof(Byte[])] = DbType.Binary;
            typeToDbTypeCache[typeof(Boolean)] = DbType.Boolean;
            typeToDbTypeCache[typeof(DateTime)] = DbType.DateTime;
            typeToDbTypeCache[typeof(Decimal)] = DbType.Decimal;
            typeToDbTypeCache[typeof(Double)] = DbType.Double;
            typeToDbTypeCache[typeof(Guid)] = DbType.Guid;
            typeToDbTypeCache[typeof(Int16)] = DbType.Int16;
            typeToDbTypeCache[typeof(Int32)] = DbType.Int32;
            typeToDbTypeCache[typeof(Int64)] = DbType.Int64;
            typeToDbTypeCache[typeof(SByte)] = DbType.SByte;
            typeToDbTypeCache[typeof(Single)] = DbType.Single;
            typeToDbTypeCache[typeof(String)] = DbType.String;
            typeToDbTypeCache[typeof(TimeSpan)] = DbType.Time;
            typeToDbTypeCache[typeof(UInt16)] = DbType.UInt16;
            typeToDbTypeCache[typeof(UInt32)] = DbType.UInt32;
            typeToDbTypeCache[typeof(UInt64)] = DbType.UInt64;
            typeToDbTypeCache[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
        }

        public static void RegisterDbType(Type type, DbType dbType)
        {
            typeToDbTypeCache[type] = dbType;
        }
        /// <summary>
        /// ��ȡ�Զ���Attribute
        /// </summary>
        /// <typeparam name="T">�Զ���Attribute������</typeparam>
        /// <param name="memberInfo">��Ҫ��ȡ�Զ���Attribute��MemberInfo</param>
        /// <returns></returns>
        public static T GetAttribute<T>(MemberInfo memberInfo) where T : System.Attribute
        {
            object[] atts = memberInfo.GetCustomAttributes(typeof(T), true);
            return atts.Length > 0 ? (T)atts[0] : null;
        }

        /// <summary>
        /// ��������ת��ΪDbType
        /// </summary>
        /// <param name="type">��������</param>
        /// <returns></returns>
        public static DbType GetDbType(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (!typeToDbTypeCache.ContainsKey(type) && type.IsEnum) type = typeof(Enum);
            if (typeToDbTypeCache.ContainsKey(type)) return typeToDbTypeCache[type];
            return DbType.Object;
        }

        /// <summary>
        /// ��ȡָ���������͵�Ĭ�ϳ���
        /// </summary>
        /// <param name="columnType">���ݿ��е���������</param>
        /// <returns></returns>
        public static int GetDefaultLength(DbType columnType)
        {
            switch (columnType)
            {
                case DbType.String:
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.StringFixedLength: return 255;
                case DbType.Byte:
                case DbType.Boolean: return 1;
                case DbType.Single:
                case DbType.Int32: return 4;
                case DbType.Double: return 8;
                case DbType.Xml: return 1 << 20;
                case DbType.Binary: return Int32.MaxValue;
                default: return 0;
            }
        }
    }
}
