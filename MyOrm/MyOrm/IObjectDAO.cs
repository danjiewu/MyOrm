using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MyOrm.Common;

namespace MyOrm
{
    #region IObjectDAO<T>
    /// <summary>
    /// ʵ�������ɾ�ĵȻ��������ķ��ͽӿ�
    /// </summary>
    /// <typeparam name="T">ʵ��������</typeparam>
    public interface IObjectDAO<T> : IObjectDAO
    {
        /// <summary>
        /// ���Ӷ���
        /// </summary>
        /// <param name="o">�����ӵĶ���</param>
        /// <returns>�Ƿ�ɹ�����</returns>
        bool Insert(T o);

        /// <summary>
        /// ���¶���
        /// </summary>
        /// <param name="o">�����µĶ���</param>
        /// <returns>�Ƿ�ɹ�����</returns>
        bool Update(T o);

        /// <summary>
        /// ��������µ����ݿ⣬������ݿ��ͻ
        /// </summary>
        /// <param name="current">�����µĶ���</param>
        /// <param name="original">ԭʼ�Ķ���</param>
        /// <returns>�Ƿ�ɹ�����</returns>
        bool Update(T current, T original);

        /// <summary>
        /// ���»����Ӷ�������������£���������������
        /// </summary>
        /// <param name="o">�����»����ӵĶ���</param>
        /// <returns>�Ƿ���»�����</returns>
        UpdateOrInsertResult UpdateOrInsert(T o);

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="o">��ɾ���Ķ���</param>
        /// <returns>�Ƿ�ɹ�ɾ��</returns>
        bool Delete(T o);
    }
    #endregion

    #region IObjectDAO
    /// <summary>
    /// ʵ�������ɾ�ĵȻ��������ķǷ��ͽӿ�
    /// </summary>
    public interface IObjectDAO
    {
        /// <summary>
        /// ���Ӷ���
        /// </summary>
        /// <param name="o">�����ӵĶ���</param>
        /// <returns>�Ƿ�ɹ�����</returns>
        bool Insert(Object o);

        /// <summary>
        /// ���¶���
        /// </summary>
        /// <param name="o">�����µĶ���</param>
        /// <returns>�Ƿ�ɹ�����</returns>
        bool Update(Object o);


        /// <summary>
        /// ��������µ����ݿ⣬������ݿ��ͻ
        /// </summary>
        /// <param name="current">�����µĶ���</param>
        /// <param name="original">ԭʼ�Ķ���</param>
        /// <returns>�Ƿ�ɹ�����</returns>
        bool Update(Object current, Object original);

        /// <summary>
        /// ���»����Ӷ�������������£���������������
        /// </summary>
        /// <param name="o">�����»����ӵĶ���</param>
        /// <returns>�Ƿ���»�����</returns>
        UpdateOrInsertResult UpdateOrInsert(Object o);

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="o">��ɾ���Ķ���</param>
        /// <returns>�Ƿ�ɹ�ɾ��</returns>
        bool Delete(Object o);

        /// <summary>
        /// ��������ɾ������
        /// </summary>
        /// <param name="keys">�����������������������˳������</param>
        /// <returns>�Ƿ�ɹ�ɾ��</returns>
        bool DeleteByKeys(params object[] keys);

        /// <summary>
        /// ��������ɾ������
        /// </summary>
        /// <param name="condition">����</param>
        /// <returns>ɾ����������</returns>
        int Delete(Condition condition);
    }
    #endregion
}