﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using MyOrm.Common;
using System.ComponentModel;
using Northwind.DAO;

namespace Northwind.Business
{
    public class EntityServiceBase<T, TView> : IEntityService<T>, IEntityViewService<TView> where TView : T, new()
    {
        protected internal static DAOFactory DAOFactory = new DAOFactory();

        protected internal IObjectDAO<T> ObjectDAO
        {
            get { return DAOFactory.GetObjectDAO<T>(); }
        }

        protected internal IObjectViewDAO<TView> ObjectViewDAO
        {
            get { return DAOFactory.GetObjectViewDAO<TView>(); }
        }
        #region IEntityService<T> 成员

        public Type EntityType
        {
            get { return typeof(T); }
        }

        public bool Insert(T entity)
        {
            return ObjectDAO.Insert(entity);
        }

        public bool Update(T entity)
        {
            return ObjectDAO.Update(entity);
        }

        public UpdateOrInsertResult UpdateOrInsert(T entity)
        {
            try
            {
                return ObjectDAO.UpdateOrInsert(entity);
            }
            finally
            {
                entity = default(T);
            }
        }

        public bool DeleteID(params object[] id)
        {
            return ObjectDAO.DeleteByKeys(id);
        }

        public bool Delete(T entity)
        {
            return ObjectDAO.Delete(entity);
        }

        public int Delete(Condition condition)
        {
            return ObjectDAO.Delete(condition);
        }

        public void BatchInsert(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                Insert(entity);
            }
        }

        public void BatchUpdate(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                Update(entity);
            }
        }

        public void BatchUpdateOrInsert(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                UpdateOrInsert(entity);
            }
        }

        public void BatchDelete(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                Delete(entity);
            }
        }

        public void BatchDeleteID(IEnumerable ids)
        {
            foreach (object id in ids)
            {
                if (id is object[])
                    DeleteID((object[])id);
                else
                    DeleteID(id);
            }
        }
        #endregion

        #region IEntityViewService<T> 成员

        public Type ViewType
        {
            get { return typeof(TView); }
        }

        public virtual TView GetObject(object id)
        {
            return ObjectViewDAO.GetObject(id);
        }

        public virtual bool ExistsID(object id)
        {
            return ObjectViewDAO.Exists(id);
        }

        public virtual bool Exists(Condition condition)
        {
            return ObjectViewDAO.Exists(condition);
        }

        public virtual int Count(Condition condition)
        {
            return ObjectViewDAO.Count(condition);
        }

        public virtual TView SearchOne(Condition condition)
        {
            return ObjectViewDAO.SearchOne(condition);
        }

        public virtual List<TView> Search(Condition condition)
        {
            return ObjectViewDAO.Search(condition);
        }

        public virtual List<TView> SearchSection(Condition condition, int startIndex, int sectionSize, string orderby, ListSortDirection direction)
        {
            SectionSet section = new SectionSet() { StartIndex = startIndex, SectionSize = sectionSize };
            if (!String.IsNullOrEmpty(orderby)) section.Orders = new Sorting[] { new Sorting() { PropertyName = orderby, Direction = direction } };
            return SearchSection(condition, section);
        }

        public virtual List<TView> SearchSection(Condition condition, SectionSet section)
        {
            return ObjectViewDAO.SearchSection(condition, section);
        }

        #endregion

        #region IEntityService 成员

        bool IEntityService.Insert(object entity)
        {
            return Insert((T)entity);
        }

        bool IEntityService.Update(object entity)
        {
            return Update((T)entity);
        }

        UpdateOrInsertResult IEntityService.UpdateOrInsert(object entity)
        {
            return UpdateOrInsert((T)entity);
        }

        void IEntityService.BatchInsert(IEnumerable entities)
        {
            if (entities is IEnumerable<T>)
                BatchInsert(entities as IEnumerable<T>);
            else
            {
                List<T> list = new List<T>();
                foreach (T entity in entities)
                {
                    list.Add(entity);
                }
                BatchInsert(list);
            }
        }

        void IEntityService.BatchUpdate(IEnumerable entities)
        {
            if (entities is IEnumerable<T>)
                BatchUpdate(entities as IEnumerable<T>);
            else
            {
                List<T> list = new List<T>();
                foreach (T entity in entities)
                {
                    list.Add(entity);
                }
                BatchUpdate(list);
            }
        }

        void IEntityService.BatchUpdateOrInsert(IEnumerable entities)
        {
            if (entities is IEnumerable<T>)
                BatchUpdateOrInsert(entities as IEnumerable<T>);
            else
            {
                List<T> list = new List<T>();
                foreach (T entity in entities)
                {
                    list.Add(entity);
                }
                BatchUpdateOrInsert(list);
            }
        }

        void IEntityService.BatchDelete(IEnumerable entities)
        {
            if (entities is IEnumerable<T>)
                BatchDelete(entities as IEnumerable<T>);
            else
            {
                List<T> list = new List<T>();
                foreach (T entity in entities)
                {
                    list.Add(entity);
                }
                BatchDelete(list);
            }
        }

        #endregion

        #region IEntityViewService 成员

        object IEntityViewService.GetObject(object id)
        {
            return GetObject(id);
        }

        object IEntityViewService.SearchOne(Condition condition)
        {
            return SearchOne(condition);
        }

        IList IEntityViewService.Search(Condition condition)
        {
            return Search(condition);
        }

        IList IEntityViewService.SearchSection(Condition condition, int startIndex, int sectionSize, string orderby, ListSortDirection direction)
        {
            return SearchSection(condition, startIndex, sectionSize, orderby, direction);
        }

        #endregion
    }
}
