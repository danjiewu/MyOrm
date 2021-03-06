﻿using System;
using System.Collections.Generic;
using System.Text;
using MyOrm.Common;
using System.ComponentModel;
using System.Collections;

namespace MyOrm.Common
{
    /// <summary>
    /// 从字符串生成条件，条件转换为字符串，以及判定对象是否符合条件等操作的静态类
    /// </summary>
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

        /// <summary>
        /// 将属性和字符串转换为简单查询条件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="text">表示查询语句的字符串,可以使用"=","&lt;","&gt;","!","%","*","&lt;=","&gt;="为起始字符表示条件符号 </param>
        /// <returns>简单查询条件</returns>
        public static SimpleCondition ParseCondition(PropertyDescriptor property, string text)
        {
            if (text == null) return new SimpleCondition(property.Name, null);
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
                        return new SimpleCondition(property.Name, ConditionOperator.Contains, text.Substring(1).Trim(), opposite);
                    case '*':
                        return new SimpleCondition(property.Name, ConditionOperator.Like, text.Substring(1).Trim(), opposite);
                }
            }
            else
            {
                return new SimpleCondition(property.Name, ConditionOperator.Equals, null, opposite);
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
        /// 字符串转化为对应属性类型的值
        /// </summary>
        /// <param name="property">属性定义</param>
        /// <param name="value">输入字符串</param>
        /// <returns>可被属性接受的值</returns>
        private static object ParseValue(PropertyDescriptor property, string value)
        {
            if (value == null) return null;
            if (property.PropertyType == typeof(string)) return value;
            value = value.Trim();
            if (value.Length == 0) return null;
            Type type = property.PropertyType;
            if (Nullable.GetUnderlyingType(type) != null) type = Nullable.GetUnderlyingType(type);
            if (type.IsEnum)
            {
                int i;
                if (Int32.TryParse(value, out i)) return Enum.ToObject(type, i);
                else return Enum.Parse(type, value);
            }
            else if (type == typeof(bool))
            {
                char ch = char.ToUpper(value[0]);
                if (ch == 'Y' || ch == 'T' || ch == '1') return true;
                else if (ch == 'N' || ch == 'F' || ch == '0') return false;
            }
            return Convert.ChangeType(value, type);
        }

        /// <summary>
        /// 根据条件生成用于解析的字符串
        /// </summary>
        /// <param name="op">条件类型</param>
        /// <param name="opposite">是否为非</param>
        /// <param name="value">用于比较的值</param>
        /// <returns></returns>
        public static string ToText(ConditionOperator op, bool opposite, object value)
        {
            switch (op)
            {
                case ConditionOperator.In:
                    List<string> values = new List<string>();
                    foreach (object o in value as IEnumerable)
                    {
                        values.Add(ToText(o));
                    }
                    string str = String.Join(",", values.ToArray());
                    return opposite ? "!" + ToText(str, "<>=*%".ToCharArray()) : ToText(str, "!<>=*%".ToCharArray());
                case ConditionOperator.LargerThan: return opposite ? "<=" + ToText(value) : ">" + ToText(value, '=');
                case ConditionOperator.SmallerThan: return opposite ? ">=" + ToText(value) : "<" + ToText(value, '=');
                case ConditionOperator.Like: return (opposite ? "!*" : "*") + ToText(value);
                case ConditionOperator.Contains: return (opposite ? "!%" : "%") + ToText(value);
                case ConditionOperator.Equals:
                    str = ToText(value);
                    if (value != null && (str == String.Empty || "<>=*%".IndexOf(str[0]) >= 0 || (str[0] == '!' && !opposite) || str.IndexOf(',') >= 0))
                        str = '=' + str;
                    return (opposite ? "!" : "") + str;
                default: return (opposite ? "!" : "") + ToText(value, "!<>=*%".ToCharArray());
            }
        }

        private static string ToText(object value, params char[] escapeChars)
        {
            if (value is Enum) return ((int)value).ToString();
            else if (value is bool) return (bool)value ? "1" : "0";
            string text = Convert.ToString(value);
            if (String.IsNullOrEmpty(text)) return text;
            if (Array.IndexOf(escapeChars, text[0]) >= 0) return ' ' + text;
            else return text;
        }
    }

    /// <summary>
    /// 条件判定结果
    /// </summary>
    public enum EnsureResult
    {
        /// <summary>
        /// 不满足条件
        /// </summary>
        False,
        /// <summary>
        /// 满足条件
        /// </summary>
        True,
        /// <summary>
        /// 无法确定
        /// </summary>
        Undetermined
    }
}
