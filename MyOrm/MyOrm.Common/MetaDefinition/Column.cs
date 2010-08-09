using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FastMethodInvoker;
using System.Reflection;

namespace MyOrm.Common
{
    public class ColumnInfo : SqlStatement
    {
        public ColumnInfo(Column column)
        {
            Name = column.Name;
            this.column = column;
        }

        public ColumnInfo(TableInfo table, Column column)
        {
            Name = column.Name;
            this.column = column;
            this.table = table;
        }

        private TableInfo table;
        public TableInfo Table
        {
            get { return table; }
            internal set { table = value; }
        }

        private Column column;
        public Column Column
        {
            get { return column; }
        }

        public override string FormattedExpression
        {
            get
            {
                return Table == null ? Column.FormattedExpression :
                    String.Format("{0}.{1}", Table.FormattedName, Column.FormattedName);
            }
        }
    }

    public class ForeignColumn : Column
    {
        internal ForeignColumn(PropertyInfo property) : base(property) { }

        public string Foreign { get; internal set; }
        public ColumnInfo TargetColumn { get; internal set; }

        public override string FormattedExpression
        {
            get
            {
                return TargetColumn.FormattedExpression;
            }
        }
    }

    public abstract class Column : SqlStatement
    {
        private PropertyInfo property;
        private FastInvokeHandler setValueHandle;
        private FastInvokeHandler getValueHandle;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="property">列对应的实体属性</param>
        internal Column(PropertyInfo property)
        {
            this.property = property;
            PropertyName = Name = property.Name;
            FormattedPropertyName = String.Format(SqlNameFormat, property.Name);
            if (property.CanWrite) this.setValueHandle = FastInvoke.GetMethodInvoker(property.GetSetMethod());
            if (property.CanRead) this.getValueHandle = FastInvoke.GetMethodInvoker(property.GetGetMethod());
        }

        public Table Table { get; internal set; }

        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get ; private set; }

        /// <summary>
        /// 格式化的属性名
        /// </summary>
        public string FormattedPropertyName { get; private set; }

        /// <summary>
        /// 列所对应的属性类型
        /// </summary>
        public Type PropertyType
        {
            get { return property.PropertyType; }
        }       

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="target">要赋值的对象</param>
        /// <param name="value">值</param>
        public virtual void SetValue(object target, object value)
        {
            //property.SetValue(target, value, null);
            setValueHandle(target, new object[] { value });
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="target">对象</param>
        /// <returns>值</returns>
        public virtual object GetValue(object target)
        {
            //return property.GetValue(target, null);
            return getValueHandle(target, null);
        }

        public virtual string SelectExpression
        {
            get
            {
                return String.Format("{0} as {1}",FormattedExpression, FormattedPropertyName );
            }
        }

        public override string FormattedExpression
        {
            get
            {
                return String.Format("{0}.{1}", Table.FormattedName, FormattedName);
            }
        }
    }

    /// <summary>
    /// 数据库列信息
    /// </summary>
    public class ColumnDefinition : Column
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="property">列对应的实体属性</param>
        internal ColumnDefinition(PropertyInfo property)
            : base(property)
        {

        }

        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsPrimaryKey { get; internal set; }

        /// <summary>
        /// 是否是自增长标识
        /// </summary>
        public bool IsIdentity { get; internal set; }

        /// <summary>
        /// 是否是索引
        /// </summary>
        public bool IsIndex { get; internal set; }

        /// <summary>
        /// 是否唯一
        /// </summary>
        public bool IsUnique { get; internal set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; internal set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DbType DbType { get; internal set; }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool AllowNull { get; internal set; }

        /// <summary>
        /// 列操作模式
        /// </summary>
        public ColumnMode Mode { get; internal set; }

        /// <summary>
        /// 关联的外部对象类型
        /// </summary>
        public Type ForeignType { get; internal set; }
    }

    /// <summary>
    /// 列操作模式
    /// </summary>
    [Flags]
    public enum ColumnMode
    {
        /// <summary>
        /// 所有操作
        /// </summary>
        Full = Read | Update | Insert,
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 从数据库中读
        /// </summary>
        Read = 1,
        /// <summary>
        /// 往数据库更新
        /// </summary>
        Update = 2,
        /// <summary>
        /// 往数据库添加
        /// </summary>
        Insert = 4,
        /// <summary>
        /// 只写
        /// </summary>
        Write = Insert | Update,
        /// <summary>
        /// 不可更改
        /// </summary>
        Final = Insert | Read
    }
}
