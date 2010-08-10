using System;
using System.Collections.Generic;
using System.Text;
using MyOrm.Common;
using System.ComponentModel;
using System.Collections;

namespace MyOrm.Common
{
    public static class ConditionConvert
    {
        /// <summary>
        /// 判定对象是否符合给定的条件
        /// </summary>
        /// <param name="condition">用来判定的条件</param>
        /// <param name="target">判定对象</param>
        /// <returns>判定结果</returns>
        public static EnsureResult Ensure(Condition condition, object target)
        {
            if (condition == null) return EnsureResult.True;
            else if (condition is SimpleCondition)
                return Ensure(condition as SimpleCondition, target);
            else if (condition is ConditionSet)
                return Ensure(condition as ConditionSet, target);
            else
                return EnsureResult.Undetermined;
        }

        private static EnsureResult Ensure(SimpleCondition condition, object target)
        {
            if (!String.IsNullOrEmpty(condition.ExpressionFormat) || condition.Operator == ConditionOperator.Constant) return EnsureResult.Undetermined;
            object value = target is IIndexedProperty ? ((IIndexedProperty)target)[condition.Property] : target.GetType().GetProperty(condition.Property).GetValue(target, null);
            bool result = false;
            switch (condition.Operator)
            {
                case ConditionOperator.Equals: result = String.Compare(Convert.ToString(value), Convert.ToString(condition.Value), true) == 0; break;
                case ConditionOperator.Contains: result = Convert.ToString(value ?? String.Empty).Contains(Convert.ToString(condition.Value ?? String.Empty)); break;
                case ConditionOperator.EndsWith: result = Convert.ToString(value ?? String.Empty).EndsWith(Convert.ToString(condition.Value ?? String.Empty), StringComparison.OrdinalIgnoreCase); break;
                case ConditionOperator.StartsWith: result = Convert.ToString(value ?? String.Empty).StartsWith(Convert.ToString(condition.Value ?? String.Empty), StringComparison.OrdinalIgnoreCase); break;
                case ConditionOperator.SmallerThan: result = Comparer.Default.Compare(value, condition.Value) < 0; break;
                case ConditionOperator.LargerThan: result = Comparer.Default.Compare(value, condition.Value) > 0; break;
                case ConditionOperator.In:
                    foreach (object o in condition.Value as IEnumerable)
                    {
                        if (Equals(value, o))
                        {
                            result = true;
                            break;
                        }
                    } break;
                default: return EnsureResult.Undetermined;
            }
            if (condition.Opposite) result = !result;
            return result ? EnsureResult.True : EnsureResult.False;
        }

        private static EnsureResult Ensure(ConditionSet condition, object target)
        {
            bool undetermined = false;
            bool opposite = condition.Opposite;
            ConditionJoinType joinType = condition.JoinType;
            foreach (Condition subCondition in condition.SubConditions)
            {
                EnsureResult subResult = Ensure(subCondition, target);
                if (subResult == EnsureResult.False && joinType == ConditionJoinType.And) return opposite ? EnsureResult.True : EnsureResult.False;
                else if (subResult == EnsureResult.True && joinType == ConditionJoinType.Or) return opposite ? EnsureResult.True : EnsureResult.False;
                else if (subResult == EnsureResult.Undetermined) undetermined = true;
            }
            if (undetermined)
                return EnsureResult.Undetermined;
            else if (joinType == ConditionJoinType.Or && condition.SubConditions.Count > 0)
                return opposite ? EnsureResult.True : EnsureResult.False;
            else
                return opposite ? EnsureResult.False : EnsureResult.True;
        }

        /// <summary>
        /// 将属性和字符串转换为简单查询条件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="text">表示查询语句的字符串,可以为"=","<",">","!","%","<=",">="
        /// </list></param>
        /// <returns>简单查询条件</returns>
        public static SimpleCondition ParseCondition(PropertyDescriptor property, string text)
        {
            if (text.Length > 1)
            {
                switch (text.Substring(0, 2))
                {
                    case "<=":
                        return new SimpleCondition(property.Name, ConditionOperator.LargerThan, ParseValue(property, text.Substring(2)), true);
                    case ">=":
                        return new SimpleCondition(property.Name, ConditionOperator.SmallerThan, ParseValue(property, text.Substring(2)), true);
                }
            }
            bool opposite = false;
            if (text.Length > 0 && text[0] == '!')
            {
                opposite = true;
                text = text.Substring(1);
            }

            if (text.Length > 0) 
            {
                switch (text[0])
                {
                    case '=':
                        return new SimpleCondition(property.Name, ConditionOperator.Equals, ParseValue(property, text.Substring(1)), opposite);
                    case '>':
                        return new SimpleCondition(property.Name, ConditionOperator.LargerThan, ParseValue(property, text.Substring(1)), opposite);
                    case '<':
                        return new SimpleCondition(property.Name, ConditionOperator.SmallerThan, ParseValue(property, text.Substring(1)), opposite);
                    case '%':
                        return new SimpleCondition(property.Name, ConditionOperator.Contains, text.Substring(1), opposite);
                }
            }
            if (text.IndexOf(',') >= 0)
            {
                List<object> values = new List<object>();
                foreach (string value in text.Split(','))
                {
                    values.Add(ParseValue(property, value));
                }
                return new SimpleCondition(property.Name, ConditionOperator.In, values.ToArray(), opposite);
            }
            return new SimpleCondition(property.Name, ConditionOperator.Equals, ParseValue(property, text), opposite);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ParseValue(PropertyDescriptor property, string value)
        {
            if (String.IsNullOrEmpty(value)) return null;
            return property.Converter.ConvertFromString(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="op"></param>
        /// <param name="opposite"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToText(ConditionOperator op, bool opposite, object value)
        {
            switch (op)
            {
                case ConditionOperator.In:
                    List<string> values = new List<string>();
                    foreach (object o in value as IEnumerable)
                    {
                        values.Add(Convert.ToString(o));
                    }
                    return (opposite ? "!" : "") + String.Join(",", values.ToArray());
                case ConditionOperator.LargerThan: return (opposite ? "<=" : ">") + Convert.ToString(value);
                case ConditionOperator.SmallerThan: return (opposite ? ">=" : "<") + Convert.ToString(value);
                case ConditionOperator.Contains: return (opposite ? "!%" : "%") + Convert.ToString(value);
                case ConditionOperator.Equals: return (opposite ? "!=" : "=") + Convert.ToString(value);
                default: return Convert.ToString(value);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EnsureResult
    {
        False,
        True,
        Undetermined
    }
}
