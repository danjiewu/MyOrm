using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.ComponentModel;

namespace MyOrm.Common
{
    /// <summary>
    /// ��ѯ����
    /// <seealso cref="MyOrm.Common.SimpleCondition"/>
    /// <seealso cref="MyOrm.Common.ConditionSet"/>  
    /// <seealso cref="MyOrm.Common.ForeignCondition"/>    
    /// </summary>
    [Serializable]
    public abstract class Condition
    {
        /// <summary>
        /// �߼���
        /// </summary>
        [DefaultValue(false)]
        public bool Opposite { get; set; }
    }

    /// <summary>
    /// �򵥲�ѯ����
    /// </summary>
    [Serializable]
    public sealed class SimpleCondition : Condition
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public SimpleCondition() { }

        /// <summary>
        /// ��Ĭ�ϲ�����ConditionOperator.Equals���ɼ򵥲�ѯ����
        /// </summary>
        /// <param name="propertyName">������</param>
        /// <param name="value">����ֵ</param>
        public SimpleCondition(string propertyName, object value)
        {
            Property = propertyName;
            Operator = ConditionOperator.Equals;
            Value = value;
        }

        /// <summary>
        /// ���ɼ򵥲�ѯ����
        /// </summary>
        /// <param name="propertyName">������</param>
        /// <param name="op">�����ȽϷ�</param>
        /// <param name="value">����ֵ</param>
        public SimpleCondition(string propertyName, ConditionOperator op, object value)
        {
            Property = propertyName;
            Operator = op;
            Value = value;
        }

        /// <summary>
        /// ���ɼ򵥲�ѯ����
        /// </summary>
        /// <param name="propertyName">������</param>
        /// <param name="op">�����ȽϷ�</param>
        /// <param name="value">����ֵ</param>
        /// <param name="opposite">�Ƿ�Ϊ��</param>
        public SimpleCondition(string propertyName, ConditionOperator op, object value, bool opposite)
        {
            Property = propertyName;
            Operator = op;
            Value = value;
            Opposite = opposite;
        }

        /// <summary>
        /// ���ɼ򵥲�ѯ����
        /// </summary>
        /// <param name="propertyName">������</param>
        /// <param name="expressionFormat">��ʽ���ı��ʽ</param>
        /// <param name="op">�����ȽϷ�</param>
        /// <param name="value">����ֵ</param>
        /// <param name="expressionType">���ʽ����</param>
        /// <param name="opposite">�Ƿ�Ϊ��</param>
        public SimpleCondition(string propertyName, string expressionFormat, ConditionOperator op, object value, bool opposite)
        {
            Property = propertyName;
            ExpressionFormat = expressionFormat;
            Operator = op;
            Value = value;
            Opposite = opposite;
        }

        /// <summary>
        /// ������
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// ��ʽ���ı��ʽ
        /// </summary>
        public string ExpressionFormat { get; set; }

        /// <summary>
        /// ����ֵ
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// �����ȽϷ�
        /// </summary>
        [DefaultValue(ConditionOperator.Equals)]
        public ConditionOperator Operator { get; set; }

        public override string ToString()
        {
            string str;
            if (Operator == ConditionOperator.In)
            {
                List<string> values = new List<string>();
                foreach (object o in Value as IEnumerable)
                {
                    values.Add(Convert.ToString(o));
                }
                str = String.Join(",", values.ToArray());
            }
            else
                str = Convert.ToString(Value);
            return String.Format("{0} {1}{2} {3}", ExpressionFormat == null ? Property : String.Format(ExpressionFormat, Property), Opposite ? "Not " : null, Operator, str);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(SimpleCondition)) return false;
            SimpleCondition condition = (SimpleCondition)obj;
            return condition.Property == Property && condition.Operator == Operator && Equals(condition.Value, Value) && condition.ExpressionFormat == ExpressionFormat;
        }

        public override int GetHashCode()
        {
            int hash = (int)Operator;
            if (Property != null) hash += Property.GetHashCode();
            if (ExpressionFormat != null) hash += ExpressionFormat.GetHashCode();
            if (Value != null) hash += Value.GetHashCode();
            return hash;
        }
    }

    /// <summary>
    /// ��ѯ��������
    /// </summary>
    [Serializable]
    public sealed class ConditionSet : Condition, IEnumerable<Condition>
    {
        private ConditionJoinType joinType = ConditionJoinType.And;
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public ConditionSet() { }

        /// <summary>
        /// ָ���������ʹ������ѯ��������
        /// </summary>
        /// <param name="joinType">��������</param>
        public ConditionSet(ConditionJoinType joinType)
        {
            this.joinType = joinType;
        }

        /// <summary>
        /// ���Ӳ�ѯ�������ϳ�ʼ��
        /// </summary>
        /// <param name="subConditions">�Ӳ�ѯ��������</param>
        public ConditionSet(IEnumerable<Condition> subConditions)
        {
            this.subConditions.AddRange(subConditions);
        }


        /// <summary>
        /// ���Ӳ�ѯ�������ϳ�ʼ��
        /// </summary>
        /// <param name="subConditions">�Ӳ�ѯ��������</param>
        public ConditionSet(params Condition[] subConditions)
        {
            this.subConditions.AddRange(subConditions);
        }

        /// <summary>
        /// ָ���������ͣ����Ӳ�ѯ�������ϳ�ʼ��
        /// </summary>
        /// <param name="joinType">��������</param>
        /// <param name="subConditions">�Ӳ�ѯ��������</param>
        public ConditionSet(ConditionJoinType joinType, IEnumerable<Condition> subConditions)
        {
            this.joinType = joinType;
            this.subConditions.AddRange(subConditions);
        }

        /// <summary>
        /// ָ���������ͣ����Ӳ�ѯ�������ϳ�ʼ��
        /// </summary>
        /// <param name="joinType">��������</param>
        /// <param name="subConditions">�Ӳ�ѯ��������</param>
        public ConditionSet(ConditionJoinType joinType, params Condition[] subConditions)
        {
            this.joinType = joinType;
            this.subConditions.AddRange(subConditions);
        }

        /// <summary>
        /// �������ͣ�Ĭ��ΪConditionJoinType.And
        /// </summary>
        [DefaultValue(ConditionJoinType.And)]
        public ConditionJoinType JoinType
        {
            get { return joinType; }
            set { joinType = value; }
        }

        /// <summary>
        /// ��ӵ�����ѯ����
        /// </summary>
        /// <param name="condition">�Ӳ�ѯ����</param>
        public void Add(Condition condition)
        {
            subConditions.Add(condition);
        }

        private List<Condition> subConditions = new List<Condition>();
        /// <summary>
        /// �Ӳ�ѯ��������
        /// </summary>
        public List<Condition> SubConditions
        {
            get { return subConditions; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Condition condition in subConditions)
            {
                if (sb.Length != 0) sb.Append(" " + joinType.ToString() + " ");
                sb.Append("(");
                if (condition != null) sb.Append(condition.ToString());
                sb.Append(")");
            }
            return sb.ToString();
        }

        #region IEnumerable<Condition> ��Ա

        public IEnumerator<Condition> GetEnumerator()
        {
            return (IEnumerator<Condition>)SubConditions;
        }

        #endregion

        #region IEnumerable ��Ա

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (System.Collections.IEnumerator)SubConditions;
        }

        #endregion
    }

    /// <summary>
    /// ��ѯ���������ӹ�ϵö��
    /// </summary>
    public enum ConditionJoinType
    {
        /// <summary>
        /// ͨ���߼��루And������
        /// </summary>
        And,
        /// <summary>
        /// ͨ���߼��루Or������
        /// </summary>
        Or
    }

    /// <summary>
    /// �����жϲ�����
    /// </summary>
    public enum ConditionOperator
    {
        /// <summary>
        /// ���
        /// </summary>
        Equals,
        /// <summary>
        /// ����
        /// </summary>
        LargerThan,
        /// <summary>
        /// С��
        /// </summary>
        SmallerThan,
        /// <summary>
        /// ��ָ���ַ���Ϊ��ʼ����Ϊ�ַ����Ƚϣ�
        /// </summary>
        StartsWith,
        /// <summary>
        /// ��ָ���ַ���Ϊ��β����Ϊ�ַ����Ƚϣ�
        /// </summary>
        EndsWith,
        /// <summary>
        /// �����ƶ��ַ�������Ϊ�ַ����Ƚϣ�
        /// </summary>
        Contains,
        /// <summary>
        /// ����
        /// </summary>
        In,
        /// <summary>
        /// �̶����ʽ������valueֵ
        /// </summary>
        Constant
    }

    /// <summary>
    /// �����ⲿ���������  
    /// </summary>
    [Serializable]
    public class ForeignCondition : Condition
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public ForeignCondition() { }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="foreignKey">���</param>
        /// <param name="condition">�ⲿ���������</param>
        public ForeignCondition(string foreignKey, Condition condition)
        {
            JoinedProperty = foreignKey;
            Condition = condition;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="foreignType">�ⲿ���������</param>
        /// <param name="condition">�ⲿ���������</param>
        public ForeignCondition(Type foreignType, Condition condition)
        {
            ForeignType = foreignType;
            Condition = condition;
        }

        /// <summary>
        /// ������������
        /// </summary>
        public Type ForeignType { get; set; }

        /// <summary>
        /// �����ⲿ���������
        /// </summary>
        public string ForeignProperty { get; set; }

        /// <summary>
        /// �������ԣ���ForeignProperties��Ӧ
        /// </summary>
        public string JoinedProperty { get; set; }
        /// <summary>
        /// �ⲿ���������
        /// </summary>
        public Condition Condition { get; set; }
    }

}
