using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.ComponentModel;
using MyOrm.Common;

namespace MyOrm
{
    #region IObjectViewDAO<T>
    /// <summary>
    /// 实体类的查询操作的泛型接口
    /// </summary>
    /// <typeparam name="T">实体类类型</typeparam>
    public interface IObjectViewDAO<T> : IObjectViewDAO
    {
        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <param name="keys">主键，多个主键按照名称顺序排列</param>
        /// <returns></returns>
        new T GetObject(params object[] keys);

        /// <summary>
        /// 根据条件获取单个对象
        /// </summary>
        /// <param name="condition">查询条件，若为null则表示没有条件</param>
        /// <returns>第一个符合条件的对象，若不存在则返回null</returns>
        new T SearchOne(Condition condition);

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="condition">查询条件，若为null则表示没有条件</param>
        /// <returns>符合条件的对象列表</returns>
        new List<T> Search(Condition condition);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="sectionSize">最大记录数</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="direction">排列顺序</param>
        /// <returns>符合条件的分页对象列表</returns>
        new List<T> SearchSection(Condition condition, int startIndex, int sectionSize, string orderby, ListSortDirection direction);
    }
    #endregion

    #region IObjectViewDAO
    /// <summary>
    /// 实体类的查询操作的非范型接口
    /// </summary>
    public interface IObjectViewDAO
    {
        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <param name="keys">主键，多个主键按照名称顺序排列</param>
        /// <returns></returns>
        Object GetObject(params object[] keys);

        /// <summary>
        /// 根据主键检查对象是否存在
        /// </summary>
        /// <param name="keys">主键，多个主键按照名称顺序排列</param>
        /// <returns>是否存在</returns>
        bool Exists(params object[] keys);

        /// <summary>
        /// 根据条件检查对象是否存在
        /// </summary>
        /// <param name="condition">查询条件，若为null则表示没有条件</param>
        /// <returns>是否存在</returns>
        bool Exists(Condition condition);

        /// <summary>
        /// 得到满足条件的对象个数
        /// </summary>
        /// <param name="condition">查询条件，若为null则表示没有条件</param>
        /// <returns>满足条件的对象个数</returns>
        int Count(Condition condition);

        /// <summary>
        /// 根据条件获取单个对象
        /// </summary>
        /// <param name="condition">查询条件，若为null则表示没有条件</param>
        /// <returns>第一个符合条件的对象，若不存在则返回null</returns>
        Object SearchOne(Condition condition);

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="condition">查询条件，若为null则表示没有条件</param>
        /// <returns>符合条件的对象列表</returns>
        IList Search(Condition condition);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="sectionSize">最大记录数</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="direction">排列顺序</param>
        /// <returns>符合条件的分页对象列表</returns>
        IList SearchSection(Condition condition, int startIndex, int sectionSize, string orderby, ListSortDirection direction);
    }
    #endregion
}
