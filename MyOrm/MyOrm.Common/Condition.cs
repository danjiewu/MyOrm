using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace MyOrm.Common
{
    /// <summary>
    /// 查询条件
    /// <seealso cref="MyOrm.Common.SimpleCondition"/>
    /// <seealso cref="MyOrm.Common.ConditionSet"/>    
    /// </summary>
    [Serializable]
    public abstract class Condition { }

    /// <summary>
    /// 简单查询条件
    /// </summary>
    [Serializable]
    public sealed class SimpleCondition : Condition
    {
        private string expression = string.Empty;
        private object value;
        private ConditionOperator op;
        private ExpressionType expressionType;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SimpleCondition() { }

        /// <summary>
        /// 以默认操作符ConditionOperator.Equals生成简单查询条件
        /// </summary>
        /// <param name="expression">表达式，可以为属性名、函数表达式等</param>
        /// <param name="value">条件值</param>
        public SimpleCondition(string expression, object value)
        {
            this.expression = expression;
            this.op = ConditionOperator.Equals;
            this.value = value;
        }

        /// <summary>
        /// 生成简单查询条件
        /// </summary>
        /// <param name="expression">表达式，可以为属性名、函数表达式等</param>
        /// <param name="op">条件比较符</param>
        /// <param name="value">条件值</param>
        public SimpleCondition(string expression, ConditionOperator op, object value)
        {
            this.expression = expression;
            this.op = op;
            this.value = value;
        }

        /// <summary>
        /// 生成简单查询条件
        /// </summary>
        /// <param name="expression">表达式，可以为属性名、函数表达式等</param>
        /// <param name="op">条件比较符</param>
        /// <param name="value">条件值</param>
        /// <param name="expressionType">表达式类型</param>
        public SimpleCondition(string expression, ConditionOperator op, object value, ExpressionType expressionType)
        {
            this.expression = expression;
            this.op = op;
            this.value = value;
            this.expressionType = expressionType;
        }

        /// <summary>
        /// 表达式
        /// </summary>
        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }

        /// <summary>
        /// 表达式类型
        /// </summary>
        public ExpressionType ExpressionType
        {
            get { return expressionType; }
            set { expressionType = value; }
        }

        /// <summary>
        /// 条件值
        /// </summary>
        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// 条件比较符
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
    /// 表达式类型
    /// </summary>
    public enum ExpressionType
    {
        /// <summary>
        /// 表达式内容为属性名
        /// </summary>
        Property,
        /// <summary>
        /// 表达式内容为函数
        /// </summary>
        Function,
        /// <summary>
        /// 表达式内容未知
        /// </summary>
        Unknown
    }

    /// <summary>
    /// 查询条件集合
    /// </summary>
    [Serializable]
    public sealed class ConditionSet : Condition
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ConditionSet() { }

        /// <summary>
        /// 指定连接类型创建查查询条件集合
        /// </summary>
        /// <param name="joinType">连接类型</param>
        public ConditionSet(ConditionJoinType joinType)
        {
            this.joinType = joinType;
        }

        private ConditionJoinType joinType = ConditionJoinType.And;
        /// <summary>
        /// 连接类型，默认为ConditionJoinType.And
        /// </summary>
        public ConditionJoinType JoinType
        {
            get { return joinType; }
            set { joinType = value; }
        }

        /// <summary>
        /// 添加单个查询条件
        /// </summary>
        /// <param name="condition">子查询条件</param>
        public void Add(Condition condition)
        {
            subConditions.Add(condition);
        }

        private List<Condition> subConditions = new List<Condition>();
        /// <summary>
        /// 子查询条件集合
        /// </summary>
        public List<Condition> SubConditions
        {
            get { return subConditions; }
        }
    }

    /// <summary>
    /// 查询条件的连接关系枚举
    /// </summary>
    public enum ConditionJoinType
    {
        /// <summary>
        /// 通过逻辑与（And）连接
        /// </summary>
        And,
        /// <summary>
        /// 通过逻辑与（Or）连接
        /// </summary>
        Or
    }

    /// <summary>
    /// 条件判断操作符
    /// </summary>
    public enum ConditionOperator
    {
        /// <summary>
        /// 相等
        /// </summary>
        Equals = 0,
        /// <summary>
        /// 大于
        /// </summary>
        LargerThan = 1,
        /// <summary>
        /// 小于
        /// </summary>
        SmallerThan = 2,
        /// <summary>
        /// 以指定字符串为开始（作为字符串比较）
        /// </summary>
        StartsWith = 3,
        /// <summary>
        /// 以指定字符串为结尾（作为字符串比较）
        /// </summary>
        EndsWith = 4,
        /// <summary>
        /// 包含制定字符串（作为字符串比较）
        /// </summary>
        Contains = 5,
        /// <summary>
        /// 逻辑否（非判断操作符）
        /// </summary>
        Not = 0xF000,
        /// <summary>
        /// 不相等
        /// </summary>
        NotEquals = Not | Equals,
        /// <summary>
        /// 小于或等于
        /// </summary>
        NotLargerThan = Not | LargerThan,
        /// <summary>
        /// 大于或等于
        /// </summary>
        NotSmallerThan = Not | SmallerThan,
        /// <summary>
        /// 以指定字符串为开始（作为字符串比较）
        /// </summary>
        NotStartsWith = Not | StartsWith,
        /// <summary>
        /// 以指定字符串为结尾（作为字符串比较）
        /// </summary>
        NotEndsWith = Not | EndsWith,
        /// <summary>
        /// 不包含制定字符串（作为字符串比较）
        /// </summary>
        NotContains = Not | Contains
    }
}
