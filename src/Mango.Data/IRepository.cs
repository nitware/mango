using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace Mango.Data
{
    public interface IRepository : IDisposable
    {
        IQueryable<T> Fetch<T>() where T : EntityObject;
        IEnumerable<T> GetAll<T>() where T : EntityObject;
        IEnumerable<T> Find<T>(Func<T, bool> predicate) where T : EntityObject;
        T Single<T>(Func<T, bool> predicate) where T : EntityObject;
        T First<T>(Func<T, bool> predicate) where T : EntityObject;
        void Add<T>(T entity) where T : EntityObject;
        void Delete<T>(T entity) where T : EntityObject;
        void Delete<T>(Func<T, bool> predicate) where T : EntityObject;
        void Attach<T>(T entity) where T : EntityObject;
        //ObjectContext GetContext();
        int SaveChanges();
        //int SaveChanges(SaveOptions options);

    }



}
