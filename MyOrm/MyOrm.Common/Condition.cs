using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace MyOrm.Common
{
    /// <summary>
    /// ��ѯ����
    /// <seealso cref="MyOrm.Common.SimpleCondition"/>
    /// <seealso cref="MyOrm.Common.ConditionSet"/>    
    /// </summary>
    [Serializable]
    public abstract class Condition { }

    /// <summary>
    /// �򵥲�ѯ����
    /// </summary>
    [Serializable]
    public sealed class SimpleCondition : Condition
    {
        private string expression = string.Empty;
        private object value;
        private ConditionOperator op;
        private ExpressionType expressionType;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public SimpleCondition() { }

        /// <summary>
        /// ��Ĭ�ϲ�����ConditionOperator.Equals���ɼ򵥲�ѯ����
        /// </summary>
        /// <param name="expression">���ʽ������Ϊ���������������ʽ��</param>
        /// <param name="value">����ֵ</param>
        public SimpleCondition(string expression, object value)
        {
            this.expression = expression;
            this.op = ConditionOperator.Equals;
            this.value = value;
        }

        /// <summary>
        /// ���ɼ򵥲�ѯ����
        /// </summary>
        /// <param name="expression">���ʽ������Ϊ���������������ʽ��</param>
        /// <param name="op">�����ȽϷ�</param>
        /// <param name="value">����ֵ</param>
        public SimpleCondition(string expression, ConditionOperator op, object value)
        {
            this.expression = expression;
            this.op = op;
            this.value = value;
        }

        /// <summary>
        /// ���ɼ򵥲�ѯ����
        /// </summary>
        /// <param name="expression">���ʽ������Ϊ���������������ʽ��</param>
        /// <param name="op">�����ȽϷ�</param>
        /// <param name="value">����ֵ</param>
        /// <param name="expressionType">���ʽ����</param>
        public SimpleCondition(string expression, ConditionOperator op, object value, ExpressionType expressionType)
        {
            this.expression = expression;
            this.op = op;
            this.value = value;
            this.expressionType = expressionType;
        }

        /// <summary>
        /// ���ʽ
        /// </summary>
        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }

        /// <summary>
        /// ���ʽ����
        /// </summary>
        public ExpressionType ExpressionType
        {
            get { return expressionType; }
            set { expressionType = value; }
        }

        /// <summary>
        /// ����ֵ
        /// </summary>
        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// �����ȽϷ�
        /// </summary>
        public ConditionOperator Operator
        {
            get { return op; }
            set { op = value; }
        }

        public override string ToString()
        {
            return String.Format(ExpressionType == ExpressionType.Property ? "[{0}] {1} {2}" : "{0} {1} {2}", Expression, Operator, Value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(SimpleCondition)) return false;
            SimpleCondition condition = (SimpleCondition)obj;
            return condition.expression == this.expression && condition.op == this.op && Equals(condition.value, this.value) && condition.expressionType == this.expressionType;
        }

        public override int GetHashCode()
        {
            int hash = (int)op + (int)expressionType;
            if (expression != null) hash += expression.GetHashCode();
            if (value != null) hash += value.GetHashCode();
            return hash;
        }
    }

    /// <summary>
    /// ���ʽ����
    /// </summary>
    public enum ExpressionType
    {
        /// <summary>
        /// ���ʽ����Ϊ������
        /// </summary>
        Property,
        /// <summary>
        /// ���ʽ����Ϊ����
        /// </summary>
        Function,
        /// <summary>
        /// ���ʽ����δ֪
        /// </summary>
        Unknown
    }

    /// <summary>
    /// ��ѯ��������
    /// </summary>
    [Serializable]
    public sealed class ConditionSet : Condition
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
        /// �������ͣ�Ĭ��ΪConditionJoinType.And
        /// </summary>
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
            string join = " " + joinType.ToString() + " ";
            foreach (Condition condition in subConditions)
            {
                if (sb.Length != 0) sb.Append(join);
                sb.Append("(");
                if (condition != null) sb.Append(condition.ToString());
                sb.Append(")");
            }
            return sb.ToString();
        }
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
        Equals = 0,
        /// <summary>
        /// ����
        /// </summary>
        LargerThan = 1,
        /// <summary>
        /// С��
        /// </summary>
        SmallerThan = 2,
        /// <summary>
        /// ��ָ���ַ���Ϊ��ʼ����Ϊ�ַ����Ƚϣ�
        /// </summary>
        StartsWith = 3,
        /// <summary>
        /// ��ָ���ַ���Ϊ��β����Ϊ�ַ����Ƚϣ�
        /// </summary>
        EndsWith = 4,
        /// <summary>
        /// �����ƶ��ַ�������Ϊ�ַ����Ƚϣ�
        /// </summary>
        Contains = 5,
        /// <summary>
        /// �߼��ǣ����жϲ�������
        /// </summary>
        Positive = 0x7FFF,
        /// <summary>
        /// �߼��񣨷��жϲ�������
        /// </summary>
        Not = ~Positive,
        /// <summary>
        /// �����
        /// </summary>
        NotEquals = Not | Equals,
        /// <summary>
        /// С�ڻ����
        /// </summary>
        NotLargerThan = Not | LargerThan,
        /// <summary>
        /// ���ڻ����
        /// </summary>
        NotSmallerThan = Not | SmallerThan,
        /// <summary>
        /// ��ָ���ַ���Ϊ��ʼ����Ϊ�ַ����Ƚϣ�
        /// </summary>
        NotStartsWith = Not | StartsWith,
        /// <summary>
        /// ��ָ���ַ���Ϊ��β����Ϊ�ַ����Ƚϣ�
        /// </summary>
        NotEndsWith = Not | EndsWith,
        /// <summary>
        /// �������ƶ��ַ�������Ϊ�ַ����Ƚϣ�
        /// </summary>
        NotContains = Not | Contains
    }
}
