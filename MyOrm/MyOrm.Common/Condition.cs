using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.ComponentModel;

namespace MyOrm.Common
{
    /// <summary>
    /// 查询条件
    /// <seealso cref="MyOrm.Common.SimpleCondition"/>
    /// <seealso cref="MyOrm.Common.ConditionSet"/>  
    /// <seealso cref="MyOrm.Common.ForeignCondition"/>    
    /// </summary>
    [Serializable]
    public abstract class Condition
    {
        /// <summary>
        /// 逻辑求反
        /// </summary>
        [DefaultValue(false)]
        public bool Opposite { get; set; }
    }

    /// <summary>
    /// 简单查询条件
    /// </summary>
    [Serializable]
    public sealed class SimpleCondition : Condition
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SimpleCondition() { }

        /// <summary>
        /// 以默认操作符ConditionOperator.Equals生成简单查询条件
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">条件值</param>
        public SimpleCondition(string propertyName, object value)
        {
            Property = propertyName;
            Operator = ConditionOperator.Equals;
            Value = value;
        }

        /// <summary>
        /// 生成简单查询条件
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="op">条件比较符</param>
        /// <param name="value">条件值</param>
        public SimpleCondition(string propertyName, ConditionOperator op, object value)
        {
            Property = propertyName;
            Operator = op;
            Value = value;
        }

        /// <summary>
        /// 生成简单查询条件
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="op">条件比较符</param>
        /// <param name="value">条件值</param>
        /// <param name="opposite">是否为非</param>
        public SimpleCondition(string propertyName, ConditionOperator op, object value, bool opposite)
        {
            Property = propertyName;
            Operator = op;
            Value = value;
            Opposite = opposite;
        }

        /// <summary>
        /// 生成简单查询条件
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="expressionFormat">格式化的表达式</param>
        /// <param name="op">条件比较符</param>
        /// <param name="value">条件值</param>
        /// <param name="expressionType">表达式类型</param>
        /// <param name="opposite">是否为非</param>
        public SimpleCondition(string propertyName, string expressionFormat, ConditionOperator op, object value, bool opposite)
        {
            Property = propertyName;
            ExpressionFormat = expressionFormat;
            Operator = op;
            Value = value;
            Opposite = opposite;
        }

        /// <summary>
        /// 属性名
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 格式化的表达式
        /// </summary>
        public string ExpressionFormat { get; set; }

        /// <summary>
        /// 条件值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 条件比较符
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
    /// 查询条件集合
    /// </summary>
    [Serializable]
    public sealed class ConditionSet : Condition, IEnumerable<Condition>
    {
        private ConditionJoinType joinType = ConditionJoinType.And;
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

        /// <summary>
        /// 以子查询条件集合初始化
        /// </summary>
        /// <param name="subConditions">子查询条件集合</param>
        public ConditionSet(IEnumerable<Condition> subConditions)
        {
            this.subConditions.AddRange(subConditions);
        }


        /// <summary>
        /// 以子查询条件集合初始化
        /// </summary>
        /// <param name="subConditions">子查询条件集合</param>
        public ConditionSet(params Condition[] subConditions)
        {
            this.subConditions.AddRange(subConditions);
        }

        /// <summary>
        /// 指定连接类型，以子查询条件集合初始化
        /// </summary>
        /// <param name="joinType">连接类型</param>
        /// <param name="subConditions">子查询条件集合</param>
        public ConditionSet(ConditionJoinType joinType, IEnumerable<Condition> subConditions)
        {
            this.joinType = joinType;
            this.subConditions.AddRange(subConditions);
        }

        /// <summary>
        /// 指定连接类型，以子查询条件集合初始化
        /// </summary>
        /// <param name="joinType">连接类型</param>
        /// <param name="subConditions">子查询条件集合</param>
        public ConditionSet(ConditionJoinType joinType, params Condition[] subConditions)
        {
            this.joinType = joinType;
            this.subConditions.AddRange(subConditions);
        }

        /// <summary>
        /// 连接类型，默认为ConditionJoinType.And
        /// </summary>
        [DefaultValue(ConditionJoinType.And)]
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

        #region IEnumerable<Condition> 成员

        public IEnumerator<Condition> GetEnumerator()
        {
            return (IEnumerator<Condition>)SubConditions;
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (System.Collections.IEnumerator)SubConditions;
        }

        #endregion
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
        Equals,
        /// <summary>
        /// 大于
        /// </summary>
        LargerThan,
        /// <summary>
        /// 小于
        /// </summary>
        SmallerThan,
        /// <summary>
        /// 以指定字符串为开始（作为字符串比较）
        /// </summary>
        StartsWith,
        /// <summary>
        /// 以指定字符串为结尾（作为字符串比较）
        /// </summary>
        EndsWith,
        /// <summary>
        /// 包含制定字符串（作为字符串比较）
        /// </summary>
        Contains,
        /// <summary>
        /// 包含
        /// </summary>
        In,
        /// <summary>
        /// 固定表达式，忽略value值
        /// </summary>
        Constant
    }

    /// <summary>
    /// 关联外部对象的条件  
    /// </summary>
    [Serializable]
    public class ForeignCondition : Condition
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ForeignCondition() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="foreignKey">外键</param>
        /// <param name="condition">外部对象的条件</param>
        public ForeignCondition(string foreignKey, Condition condition)
        {
            JoinedProperty = foreignKey;
            Condition = condition;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="foreignType">外部对象的类型</param>
        /// <param name="condition">外部对象的条件</param>
        public ForeignCondition(Type foreignType, Condition condition)
        {
            ForeignType = foreignType;
            Condition = condition;
        }

        /// <summary>
        /// 关联对象类型
        /// </summary>
        public Type ForeignType { get; set; }

        /// <summary>
        /// 关联外部对象的属性
        /// </summary>
        public string ForeignProperty { get; set; }

        /// <summary>
        /// 关联属性，与ForeignProperties对应
        /// </summary>
        public string JoinedProperty { get; set; }
        /// <summary>
        /// 外部对象的条件
        /// </summary>
        public Condition Condition { get; set; }
    }

}
