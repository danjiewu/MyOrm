using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MyOrm.Common
{
    #region IObjectDAO<T>
    /// <summary>
    /// 实体类的增删改等基本操作的泛型接口
    /// </summary>
    /// <typeparam name="T">实体类类型</typeparam>
    public interface IObjectDAO<T> 
    {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="o">待添加的对象</param>
        /// <returns>是否成功添加</returns>
        bool Insert(T o);

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="o">待更新的对象</param>
        /// <returns>是否成功更新</returns>
        bool Update(T o);

        /// <summary>
        /// 更新或添加对象，若存在则更新，若不存在则添加
        /// </summary>
        /// <param name="o">待更新或添加的对象</param>
        /// <returns>是否成功更新或添加</returns>
        bool UpdateOrInsert(T o);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="o">待删除的对象</param>
        /// <returns>是否成功删除</returns>
        bool Delete(T o);

        /// <summary>
        /// 根据主键删除对象
        /// </summary>
        /// <param name="keys">主键，多个主键按照主键名顺序排列</param>
        /// <returns>是否成功删除</returns>
        bool DeleteByKeys(params object[] keys);
    }
    #endregion

    #region IObjectDAO
    /// <summary>
    /// 实体类的增删改等基本操作的非泛型接口
    /// </summary>
    public interface IObjectDAO 
    {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="o">待添加的对象</param>
        /// <returns>是否成功添加</returns>
        bool Insert(Object o);

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="o">待更新的对象</param>
        /// <returns>是否成功更新</returns>
        bool Update(Object o);

        /// <summary>
        /// 更新或添加对象，若存在则更新，若不存在则添加
        /// </summary>
        /// <param name="o">待更新或添加的对象</param>
        /// <returns>是否成功更新或添加</returns>
        bool UpdateOrInsert(Object o);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="o">待删除的对象</param>
        /// <returns>是否成功删除</returns>
        bool Delete(Object o);

        /// <summary>
        /// 根据主键删除对象
        /// </summary>
        /// <param name="keys">主键，多个主键按照主键名顺序排列</param>
        /// <returns>是否成功删除</returns>
        bool DeleteByKeys(params object[] keys);
    }
    #endregion
}
