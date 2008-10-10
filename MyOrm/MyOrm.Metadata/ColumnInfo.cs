using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FastMethodInvoker;
using System.Reflection;

namespace MyOrm.Metadata
{
    /// <summary>
    /// 数据库列信息
    /// </summary>
    public class ColumnInfo
    {
        private string columnName;
        private string foreignTable;
        private DbType dbType;
        private int length;
        private bool isPrimaryKey = false;
        private bool allowNull = true;
        private PropertyInfo property;
        private ColumnMode mode;
        private FastInvokeHandler setValueHandle;
        private FastInvokeHandler getValueHandle;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="property">列对应的实体属性</param>
        public ColumnInfo(PropertyInfo property)
        {
            this.property = property;
            this.setValueHandle = FastInvoke.GetMethodInvoker(property.GetSetMethod());
            this.getValueHandle = FastInvoke.GetMethodInvoker(property.GetGetMethod());
        }

        /// <summary>
        /// 在查询时所在的表名，可以是别名
        /// </summary>
        public string ForeignTable
        {
            get { return foreignTable; }
            set { foreignTable = value; }
        }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }

        /// <summary>
        /// 列所对应的属性名称
        /// </summary>
        public string PropertyName
        {
            get { return property.Name; }
        }

        /// <summary>
        /// 列所对应的属性类型
        /// </summary>
        public Type PropertyType
        {
            get { return property.PropertyType; }
        }

        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
            set { isPrimaryKey = value; }
        }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DbType DbType
        {
            get { return dbType; }
            set { dbType = value; }
        }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool AllowNull
        {
            get { return allowNull; }
            set { allowNull = value; }
        }

        /// <summary>
        /// 列操作模式
        /// </summary>
        public ColumnMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="target">要赋值的对象</param>
        /// <param name="value">值</param>
        public virtual void SetValue(object target, object value)//TODO
        {
            //property.SetValue(target, value, null);
            setValueHandle(target, new object[] { value });
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="target">对象</param>
        /// <returns>值</returns>
        public virtual object GetValue(object target)//TODO
        {
            return getValueHandle(target, null);
        }

        public override string ToString()
        {
            return ColumnName;
        }
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
        /// 忽略
        /// </summary>
        Ignore = 0,
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
        Write = Insert | Update
    }
}
