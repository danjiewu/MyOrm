using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

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
        [XmlAttribute]
        public bool Opposite { get; set; }

        public abstract EnsureResult Ensure(object target);
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
        /// ������
        /// </summary>
        [XmlAttribute]
        public string Property { get; set; }

        /// <summary>
        /// ����ֵ
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// �����ȽϷ�
        /// </summary>
        [DefaultValue(ConditionOperator.Equals)]
        [XmlAttribute]
        public ConditionOperator Operator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public override EnsureResult Ensure(object target)
        {
            object targetValue = target is IIndexedProperty ? ((IIndexedProperty)target)[Property] : target.GetType().GetProperty(Property).GetValue(target, null);
            bool result = false;
            switch (Operator)
            {
                case ConditionOperator.Equals: result = String.Compare(Convert.ToString(targetValue), Convert.ToString(Value), true) == 0; break;
                case ConditionOperator.Contains: result = Convert.ToString(targetValue ?? String.Empty).Contains(Convert.ToString(Value ?? String.Empty)); break;
                case ConditionOperator.EndsWith: result = Convert.ToString(targetValue ?? String.Empty).EndsWith(Convert.ToString(Value ?? String.Empty), StringComparison.OrdinalIgnoreCase); break;
                case ConditionOperator.StartsWith: result = Convert.ToString(targetValue ?? String.Empty).StartsWith(Convert.ToString(Value ?? String.Empty), StringComparison.OrdinalIgnoreCase); break;
                case ConditionOperator.SmallerThan: result = Comparer.Default.Compare(targetValue, Value) < 0; break;
                case ConditionOperator.LargerThan: result = Comparer.Default.Compare(targetValue, Value) > 0; break;
                case ConditionOperator.In:
                    foreach (object o in Value as IEnumerable)
                    {
                        if (Equals(targetValue, o))
                        {
                            result = true;
                            break;
                        }
                    } break;
                case ConditionOperator.Like: result = new Regex(Convert.ToString(Value ?? String.Empty).ToString().Replace(".", "\\.").Replace("_", ".").Replace("%", ".*"), RegexOptions.IgnoreCase).IsMatch(Convert.ToString(targetValue ?? String.Empty));///TODO
                    break;
                default: return EnsureResult.Undetermined;
            }
            if (Opposite) result = !result;
            return result ? EnsureResult.True : EnsureResult.False;
        }

        /// <summary>
        /// ��дToString����
        /// </summary>
        /// <returns></returns>
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
            return String.Format("{0} {1}{2} {3}", Property, Opposite ? "Not " : null, Operator, str);
        }

        /// <summary>
        /// ��дEquals����
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(SimpleCondition)) return false;
            SimpleCondition condition = (SimpleCondition)obj;
            return condition.Property == Property && condition.Operator == Operator && Equals(condition.Value, Value);
        }

        /// <summary>
        /// ��дGetHashCode����
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hash = (int)Operator;
            if (Property != null) hash += Property.GetHashCode();
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
        [XmlAttribute]
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
        [XmlArray]
        public List<Condition> SubConditions
        {
            get { return subConditions; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public override EnsureResult Ensure(object target)
        {
            bool undetermined = false;
            bool opposite = Opposite;
            ConditionJoinType joinType = JoinType;
            foreach (Condition subCondition in SubConditions)
            {
                EnsureResult subResult = subCondition == null ? EnsureResult.True : subCondition.Ensure(target);
                if (subResult == EnsureResult.False && joinType == ConditionJoinType.And) return opposite ? EnsureResult.True : EnsureResult.False;
                else if (subResult == EnsureResult.True && joinType == ConditionJoinType.Or) return opposite ? EnsureResult.True : EnsureResult.False;
                else if (subResult == EnsureResult.Undetermined) undetermined = true;
            }
            if (undetermined)
                return EnsureResult.Undetermined;
            else if (joinType == ConditionJoinType.Or && SubConditions.Count > 0)
                return opposite ? EnsureResult.True : EnsureResult.False;
            else
                return opposite ? EnsureResult.False : EnsureResult.True;
        }


        /// <summary>
        /// ��дToString����
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Condition condition in subConditions)
            {
                if (sb.Length != 0) sb.Append(" " + joinType.ToString() + " ");
                sb.Append("{");
                if (condition != null) sb.Append(condition.ToString());
                sb.Append("}");
            }
            return sb.ToString();
        }

        #region IEnumerable<Condition> ��Ա

        IEnumerator<Condition> IEnumerable<Condition>.GetEnumerator()
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
        /// ƥ���ַ�����ʽ����Ϊ�ַ����Ƚϣ�
        /// </summary>
        Like,
        /// <summary>
        /// ����
        /// </summary>
        In
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
        [XmlAttribute(typeof(string))]
        public Type ForeignType { get; set; }

        /// <summary>
        /// �����ⲿ���������
        /// </summary>
        [XmlAttribute]
        public string ForeignProperty { get; set; }

        /// <summary>
        /// �������ԣ���ForeignProperties��Ӧ
        /// </summary>
        [XmlAttribute]
        public string JoinedProperty { get; set; }
        /// <summary>
        /// �ⲿ���������
        /// </summary>
        public Condition Condition { get; set; }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{{{4}}}", Opposite ? "Not " : null, JoinedProperty == null ? null : JoinedProperty + "=", ForeignType, ForeignProperty == null ? null : "." + ForeignProperty, Condition);
        }

        public override EnsureResult Ensure(object target)
        {
            return EnsureResult.Undetermined;
        }
    }
}
