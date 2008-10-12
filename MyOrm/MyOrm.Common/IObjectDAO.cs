using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MyOrm.Common
{
    #region IObjectDAO<T>
    /// <summary>
    /// ʵ�������ɾ�ĵȻ��������ķ��ͽӿ�
    /// </summary>
    /// <typeparam name="T">ʵ��������</typeparam>
    public interface IObjectDAO<T> 
    {
        /// <summary>
        /// ��Ӷ���
        /// </summary>
        /// <param name="o">����ӵĶ���</param>
        /// <returns>�Ƿ�ɹ����</returns>
        bool Insert(T o);

        /// <summary>
        /// ���¶���
        /// </summary>
        /// <param name="o">�����µĶ���</param>
        /// <returns>�Ƿ�ɹ�����</returns>
        bool Update(T o);

        /// <summary>
        /// ���»���Ӷ�������������£��������������
        /// </summary>
        /// <param name="o">�����»���ӵĶ���</param>
        /// <returns>�Ƿ�ɹ����»����</returns>
        bool UpdateOrInsert(T o);

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="o">��ɾ���Ķ���</param>
        /// <returns>�Ƿ�ɹ�ɾ��</returns>
        bool Delete(T o);

        /// <summary>
        /// ��������ɾ������
        /// </summary>
        /// <param name="keys">�����������������������˳������</param>
        /// <returns>�Ƿ�ɹ�ɾ��</returns>
        bool DeleteByKeys(params object[] keys);
    }
    #endregion

    #region IObjectDAO
    /// <summary>
    /// ʵ�������ɾ�ĵȻ��������ķǷ��ͽӿ�
    /// </summary>
    public interface IObjectDAO 
    {
        /// <summary>
        /// ��Ӷ���
        /// </summary>
        /// <param name="o">����ӵĶ���</param>
        /// <returns>�Ƿ�ɹ����</returns>
        bool Insert(Object o);

        /// <summary>
        /// ���¶���
        /// </summary>
        /// <param name="o">�����µĶ���</param>
        /// <returns>�Ƿ�ɹ�����</returns>
        bool Update(Object o);

        /// <summary>
        /// ���»���Ӷ�������������£��������������
        /// </summary>
        /// <param name="o">�����»���ӵĶ���</param>
        /// <returns>�Ƿ�ɹ����»����</returns>
        bool UpdateOrInsert(Object o);

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
    }
    #endregion
}
