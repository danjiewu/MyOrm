using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FastMethodInvoker;
using System.Reflection;

namespace MyOrm.Common
{
    /// <summary>
    /// �е�����
    /// </summary>
    public class ColumnRef : SqlStatement
    {
        /// <summary>
        /// �����е�����
        /// </summary>
        /// <param name="column">����Ϣ</param>
        public ColumnRef(Column column)
        {
            Name = column.Name;
            this.column = column;
        }

        /// <summary>
        /// ����ָ�����������
        /// </summary>
        /// <param name="table">��</param>
        /// <param name="column">������</param>
        public ColumnRef(TableRef table, Column column)
        {
            Name = column.Name;
            this.column = column;
            this.table = table;
        }

        private TableRef table;
        /// <summary>
        /// �����ڵı�
        /// </summary>
        public TableRef Table
        {
            get { return table; }
            internal set { table = value; }
        }

        private Column column;
        /// <summary>
        /// ����Ϣ
        /// </summary>
        public Column Column
        {
            get { return column; }
        }

        /// <summary>
        /// ��ʽ���ı��ʽ
        /// </summary>
        public override string FormattedExpression(ISqlBuilder sqlBuilder)
        {
            return Table == null ? Column.FormattedExpression(sqlBuilder) :
                String.Format("{0}.{1}", Table.FormattedName(sqlBuilder), Column.FormattedName(sqlBuilder));
        }
    }

    /// <summary>
    /// ������������Ϣ
    /// </summary>
    public class ForeignColumn : Column
    {
        internal ForeignColumn(PropertyInfo property) : base(property) { }

        /// <summary>
        /// �������ı���
        /// </summary>
        public string Foreign { get; internal set; }

        /// <summary>
        /// ָ�����
        /// </summary>
        public ColumnRef TargetColumn { get; internal set; }

        /// <summary>
        /// ��ʽ���ı��ʽ
        /// </summary>
        public override string FormattedExpression(ISqlBuilder sqlBuilder)
        {
            return TargetColumn.FormattedExpression(sqlBuilder);
        }

        /// <summary>
        /// ����
        /// </summary>
        public override string Name
        {
            get
            {
                return TargetColumn == null ? null : TargetColumn.Name;
            }
            internal set
            {
            }
        }
    }

    /// <summary>
    /// ��������Ϣ
    /// </summary>
    public abstract class Column : SqlStatement
    {
        private PropertyInfo property;
        private FastInvokeHandler setValueHandle;
        private FastInvokeHandler getValueHandle;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="property">�ж�Ӧ��ʵ������</param>
        internal Column(PropertyInfo property)
        {
            this.property = property;
            PropertyName = Name = property.Name;
            if (property.CanWrite) this.setValueHandle = FastInvoke.GetMethodInvoker(property.GetSetMethod());
            if (property.CanRead) this.getValueHandle = FastInvoke.GetMethodInvoker(property.GetGetMethod());
        }

        /// <summary>
        /// �����ı���Ϣ
        /// </summary>
        public Table Table { get; internal set; }

        /// <summary>
        /// ������
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// ������Ӧ����������
        /// </summary>
        public Type PropertyType
        {
            get { return property.PropertyType; }
        }

        /// <summary>
        /// �ж�Ӧ������
        /// </summary>
        public PropertyInfo Property { get { return property; } }

        /// <summary>
        /// �������ⲿ��������
        /// </summary>
        public Type ForeignType { get; internal set; }

        /// <summary>
        /// ��ֵ
        /// </summary>
        /// <param name="target">Ҫ��ֵ�Ķ���</param>
        /// <param name="value">ֵ</param>
        public virtual void SetValue(object target, object value)
        {
            //property.SetValue(target, value, null);
            setValueHandle(target, new object[] { value });
        }

        /// <summary>
        /// ȡֵ
        /// </summary>
        /// <param name="target">����</param>
        /// <returns>ֵ</returns>
        public virtual object GetValue(object target)
        {
            //return property.GetValue(target, null);
            return getValueHandle(target, null);
        }

        /// <summary>
        /// ��ʽ���ı��ʽ
        /// </summary>
        public override string FormattedExpression(ISqlBuilder sqlBuilder)
        {
            return String.Format("{0}.{1}", Table.FormattedName(sqlBuilder), FormattedName(sqlBuilder));
        }
    }

    /// <summary>
    /// ���ݿ�����Ϣ
    /// </summary>
    public class ColumnDefinition : Column
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="property">�ж�Ӧ��ʵ������</param>
        internal ColumnDefinition(PropertyInfo property)
            : base(property)
        {

        }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool IsPrimaryKey { get; internal set; }

        /// <summary>
        /// �Ƿ�����������ʶ
        /// </summary>
        public bool IsIdentity { get; internal set; }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool IsIndex { get; internal set; }

        /// <summary>
        /// �Ƿ�Ψһ
        /// </summary>
        public bool IsUnique { get; internal set; }

        /// <summary>
        /// ����
        /// </summary>
        public int Length { get; internal set; }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public DbType DbType { get; internal set; }

        /// <summary>
        /// �Ƿ�����Ϊ��
        /// </summary>
        public bool AllowNull { get; internal set; }

        /// <summary>
        /// �в���ģʽ
        /// </summary>
        public ColumnMode Mode { get; internal set; }
    }

    /// <summary>
    /// �в���ģʽ
    /// </summary>
    [Flags]
    public enum ColumnMode
    {
        /// <summary>
        /// ���в���
        /// </summary>
        Full = Read | Update | Insert,
        /// <summary>
        /// ��
        /// </summary>
        None = 0,
        /// <summary>
        /// �����ݿ��ж�
        /// </summary>
        Read = 1,
        /// <summary>
        /// �����ݿ����
        /// </summary>
        Update = 2,
        /// <summary>
        /// �����ݿ����
        /// </summary>
        Insert = 4,
        /// <summary>
        /// ֻд
        /// </summary>
        Write = Insert | Update,
        /// <summary>
        /// ���ɸ���
        /// </summary>
        Final = Insert | Read
    }
}
