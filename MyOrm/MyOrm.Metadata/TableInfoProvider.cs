using System;
using System.Reflection;

namespace MyOrm.Metadata
{
    /// <summary>
    /// 表信息提供类
    /// </summary>
    public abstract class TableInfoProvider
    {
        /// <summary>
        /// 获取对象类型所对应的表信息
        /// </summary>
        /// <param name="objectType">对象类型</param>
        /// <returns>对应的表信息</returns>
        public abstract TableInfo GetTableInfo(Type objectType);

        /// <summary>
        /// 获取相关联的表信息
        /// </summary>
        /// <param name="objectType">对象类型</param>
        /// <returns>相关联的表信息集合</returns>
        public abstract TableJoinInfo[] GetTableJoins(Type objectType);

        /// <summary>
        /// 获取属性所对应的列信息
        /// </summary>
        /// <param name="property">属性</param>
        /// <returns>对应的列信息</returns>
        public abstract ColumnInfo GetColumnInfo(PropertyInfo property);
    }
}
