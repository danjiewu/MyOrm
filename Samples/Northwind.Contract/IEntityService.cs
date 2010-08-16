using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using MyOrm.Common;

namespace Northwind.Business
{
    public interface IEntityService<T> : IEntityService
    {
        bool Insert(T entity);

        bool Update(T entity);

        bool Update(T newEntity, T oldEntity);

        UpdateOrInsertResult UpdateOrInsert(T entity);

        void BatchInsert(IEnumerable<T> entities);

        void BatchUpdate(IEnumerable<T> entities);

        void BatchUpdateOrInsert(IEnumerable<T> entities);

        void BatchDelete(IEnumerable<T> entities);
    }

    public interface IEntityService
    {
        Type EntityType { get; }

        bool Insert(Object entity);

        bool Update(Object entity);

        bool Update(Object newEntity, Object oldEntity);

        UpdateOrInsertResult UpdateOrInsert(Object entity);

        void BatchInsert(IEnumerable entities);

        void BatchUpdate(IEnumerable entities);

        void BatchUpdateOrInsert(IEnumerable entities);

        void BatchDelete(IEnumerable entities);
    }

    public interface IEntityViewService<TView> : IEntityViewService
    {
        new TView GetObject(object id);

        new TView SearchOne(Condition condition);

        new List<TView> Search(Condition condition);

        new List<TView> SearchSection(Condition condition, int startIndex, int sectionSize, string orderby, ListSortDirection direction);
   
    }

    public interface IEntityViewService
    {
        Type ViewType { get; }

        object GetObject(object id);

        bool ExistsID(object id);

        bool Exists(Condition condition);

        int Count(Condition condition);

        object SearchOne(Condition condition);

        IList Search(Condition condition);

        IList SearchSection(Condition condition, int startIndex, int sectionSize, string orderby, ListSortDirection direction);
    
    }
}
