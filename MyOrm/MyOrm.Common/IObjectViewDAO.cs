using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.ComponentModel;

namespace MyOrm.Common
{
    #region IObjectViewDAO<T>
    /// <summary>
    /// ʵ����Ĳ�ѯ�����ķ��ͽӿ�
    /// </summary>
    /// <typeparam name="T">ʵ��������</typeparam>
    public interface IObjectViewDAO<T>
    {
        /// <summary>
        /// ����������ȡ����
        /// </summary>
        /// <param name="keys">���������������������˳������</param>
        /// <returns></returns>
        T GetObject(params object[] keys);

        /// <summary>
        /// ���������������Ƿ����
        /// </summary>
        /// <param name="keys">���������������������˳������</param>
        /// <returns>�Ƿ����</returns>
        bool Exists(params object[] keys);

        /// <summary>
        /// ���������������Ƿ����
        /// </summary>
        /// <param name="condition">��ѯ��������Ϊnull���ʾû������</param>
        /// <returns>�Ƿ����</returns>
        bool Exists(Condition condition);

        /// <summary>
        /// �õ����������Ķ������
        /// </summary>
        /// <param name="condtion">��ѯ��������Ϊnull���ʾû������</param>
        /// <returns>���������Ķ������</returns>
        int Count(Condition condtion);

        /// <summary>
        /// ����������ȡ��������
        /// </summary>
        /// <param name="condition">��ѯ��������Ϊnull���ʾû������</param>
        /// <returns>��һ�����������Ķ������������򷵻�null</returns>
        T SearchOne(Condition condition);

        /// <summary>
        /// ����������ѯ
        /// </summary>
        /// <param name="condition">��ѯ��������Ϊnull���ʾû������</param>
        /// <returns>���������Ķ����б�</returns>
        List<T> Search(Condition condition);

        /// <summary>
        /// ��ҳ��ѯ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="startIndex">��ʼλ��</param>
        /// <param name="sectionSize">����¼��</param>
        /// <param name="orderby">�����ֶ�</param>
        /// <param name="direction">����˳��</param>
        /// <returns>���������ķ�ҳ�����б�</returns>
        List<T> SearchSection(Condition condition, int startIndex, int sectionSize, string orderby, ListSortDirection direction);
    }
    #endregion

    #region IObjectViewDAO
    /// <summary>
    /// ʵ����Ĳ�ѯ�����ķǷ��ͽӿ�
    /// </summary>
    public interface IObjectViewDAO
    {
        /// <summary>
        /// ����������ȡ����
        /// </summary>
        /// <param name="keys">���������������������˳������</param>
        /// <returns></returns>
        Object GetObject(params object[] keys);

        /// <summary>
        /// ���������������Ƿ����
        /// </summary>
        /// <param name="keys">���������������������˳������</param>
        /// <returns>�Ƿ����</returns>
        bool Exists(params object[] keys);

        /// <summary>
        /// ���������������Ƿ����
        /// </summary>
        /// <param name="condition">��ѯ��������Ϊnull���ʾû������</param>
        /// <returns>�Ƿ����</returns>
        bool Exists(Condition condition);

        /// <summary>
        /// �õ����������Ķ������
        /// </summary>
        /// <param name="condtion">��ѯ��������Ϊnull���ʾû������</param>
        /// <returns>���������Ķ������</returns>
        int Count(Condition condtion);

        /// <summary>
        /// ����������ȡ��������
        /// </summary>
        /// <param name="condition">��ѯ��������Ϊnull���ʾû������</param>
        /// <returns>��һ�����������Ķ������������򷵻�null</returns>
        Object SearchOne(Condition condition);

        /// <summary>
        /// ����������ѯ
        /// </summary>
        /// <param name="condition">��ѯ��������Ϊnull���ʾû������</param>
        /// <returns>���������Ķ����б�</returns>
        IList Search(Condition condition);

        /// <summary>
        /// ��ҳ��ѯ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="startIndex">��ʼλ��</param>
        /// <param name="sectionSize">����¼��</param>
        /// <param name="orderby">�����ֶ�</param>
        /// <param name="direction">����˳��</param>
        /// <returns>���������ķ�ҳ�����б�</returns>
        IList SearchSection(Condition condition, int startIndex, int sectionSize, string orderby, ListSortDirection direction);
    }
    #endregion
}
