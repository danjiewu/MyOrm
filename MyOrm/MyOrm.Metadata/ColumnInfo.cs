using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FastMethodInvoker;
using System.Reflection;

namespace MyOrm.Metadata
{
    /// <summary>
    /// ���ݿ�����Ϣ
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
        /// ���캯��
        /// </summary>
        /// <param name="property">�ж�Ӧ��ʵ������</param>
        public ColumnInfo(PropertyInfo property)
        {
            this.property = property;
            this.setValueHandle = FastInvoke.GetMethodInvoker(property.GetSetMethod());
            this.getValueHandle = FastInvoke.GetMethodInvoker(property.GetGetMethod());
        }

        /// <summary>
        /// �ڲ�ѯʱ���ڵı����������Ǳ���
        /// </summary>
        public string ForeignTable
        {
            get { return foreignTable; }
            set { foreignTable = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }

        /// <summary>
        /// ������Ӧ����������
        /// </summary>
        public string PropertyName
        {
            get { return property.Name; }
        }

        /// <summary>
        /// ������Ӧ����������
        /// </summary>
        public Type PropertyType
        {
            get { return property.PropertyType; }
        }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
            set { isPrimaryKey = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public DbType DbType
        {
            get { return dbType; }
            set { dbType = value; }
        }

        /// <summary>
        /// �Ƿ�����Ϊ��
        /// </summary>
        public bool AllowNull
        {
            get { return allowNull; }
            set { allowNull = value; }
        }

        /// <summary>
        /// �в���ģʽ
        /// </summary>
        public ColumnMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        /// <summary>
        /// ��ֵ
        /// </summary>
        /// <param name="target">Ҫ��ֵ�Ķ���</param>
        /// <param name="value">ֵ</param>
        public virtual void SetValue(object target, object value)//TODO
        {
            //property.SetValue(target, value, null);
            setValueHandle(target, new object[] { value });
        }

        /// <summary>
        /// ȡֵ
        /// </summary>
        /// <param name="target">����</param>
        /// <returns>ֵ</returns>
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
        /// ����
        /// </summary>
        Ignore = 0,
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
        Write = Insert | Update
    }
}
