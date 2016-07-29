﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyOrm.Common
{
    /// <summary>
    /// 关联的外部实体类型定义
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ForeignTypeAttribute : System.Attribute
    {
        private Type objectType;
        /// <summary>
        /// 关联的外部实体类型
        /// </summary>
        /// <param name="objectType">外部实体的类型</param>
        public ForeignTypeAttribute(Type objectType)
        {
            this.objectType = objectType;
        }

        /// <summary>
        /// 外部实体类型
        /// </summary>
        public Type ObjectType
        {
            get { return objectType; }
        }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 附加筛选属性
        /// </summary>
        public string FilterProperty { get; set; }

        /// <summary>
        /// 筛选值
        /// </summary>
        public object FilterValue { get; set; }
    }
}
