using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Mango.Data;

namespace Mango.Data
{
    public class Repository : IRepository
    {
        /// <summary>
        /// The context object for the database
        /// </summary>
        private ObjectContext context;
        protected const string AlreadyInUse = "The DELETE statement conflicted with the REFERENCE constraint";

        /// <summary>
        /// The transaction scope object for handling transactions
        /// </summary>
        /// private TransactionScope transaction;

        /// <summary>
        /// The IObjectSet that represents the current entity.
        /// </summary>
        //private IObjectSet<T> objectSet;

        /// <summary>
        /// Initializes a new instance of the Repository class
        /// </summary>
        public Repository()
            : this(new MangoEntities())
        {

        }

        /// <summary>
        /// Initializes a new instance of the Repository class
        /// </summary>
        /// <param name="context">The Entity Framework ObjectContext</param>
        public Repository(ObjectContext _context)
        {
            context = _context;
            //objectSet = context.CreateObjectSet<T>();
        }
               
        /// <summary>
        /// Gets all records as an IQueryable
        /// </summary>
        /// <returns>An IQueryable object containing the results of the query</returns>
        public IQueryable<T> Fetch<T>() where T : EntityObject
        {
            return context.CreateObjectSet<T>();
        }

        /// <summary>
        /// Gets all records as an IEnumberable
        /// </summary>
        /// <returns>An IEnumberable object containing the results of the query</returns>
        public IEnumerable<T> GetAll<T>() where T : EntityObject
        {
            IObjectSet<T> objectSet = context.CreateObjectSet<T>();
            return objectSet.AsEnumerable();
        }

        /// <summary>
        /// Gets records count
        /// </summary>
        /// <returns>Total record count in the specified entity</returns>
        public int Count<T>() where T : EntityObject
        {
            IObjectSet<T> objectSet = context.CreateObjectSet<T>();
            return objectSet.Count();
        }

        /// <summary>
        /// Finds a record with the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A collection containing the results of the query</returns>
        public IEnumerable<T> Find<T>(Func<T, bool> predicate) where T : EntityObject
        {
            IObjectSet<T> objectSet = context.CreateObjectSet<T>();
            return objectSet.Where<T>(predicate);
        }

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public T Single<T>(Func<T, bool> predicate) where T : EntityObject
        {
            IObjectSet<T> objectSet = context.CreateObjectSet<T>();
            return objectSet.SingleOrDefault<T>(predicate);
        }

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public T First<T>(Func<T, bool> predicate) where T : EntityObject
        {
            IObjectSet<T> objectSet = context.CreateObjectSet<T>();
            return objectSet.FirstOrDefault<T>(predicate);
        }

        /// <summary>
        /// Deletes the specified entitiy
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <exception cref="ArgumentNullException"> if <paramref name="entity"/> is null</exception>
        public void Delete<T>(T entity) where T : EntityObject
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            IObjectSet<T> objectSet = context.CreateObjectSet<T>();           
            objectSet.DeleteObject(entity);
        }

        /// <summary>
        /// Deletes records matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        public void Delete<T>(Func<T, bool> predicate) where T : EntityObject
        {
            IObjectSet<T> objectSet = context.CreateObjectSet<T>();
            IEnumerable<T> records = from x in objectSet.Where<T>(predicate) select x;

            foreach (T record in records)
            {
                objectSet.DeleteObject(record);
            }
        }
        
        /// <summary>
        /// Adds the specified entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <exception cref="ArgumentNullException"> if <paramref name="entity"/> is null</exception>
        public void Add<T>(T entity) where T : EntityObject
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
                        
            IObjectSet<T> objectSet = context.CreateObjectSet<T>();
            objectSet.AddObject(entity);           
        }
       
        /// <summary>
        /// Attaches the specified entity
        /// </summary>
        /// <param name="entity">Entity to attach</param>
        public void Attach<T>(T entity) where T : EntityObject
        {
            IObjectSet<T> objectSet = context.CreateObjectSet<T>();
            objectSet.Attach(entity);
        }

        /// <summary>
        /// Saves all context changes
        /// </summary>
        public int SaveChanges()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains(AlreadyInUse))
                {
                    throw new Exception("This item is currently asspciated with other items in the system and cannot be removed at this time. Kindly remove all associated items prior to its removal.");
                }

                throw;
            }

            //return -1;
        }
       

        //public ObjectContext GetContext()
        //{
        //    return context;
        //}

        ///// <summary>
        ///// Saves all context changes with the specified SaveOptions
        ///// </summary>
        ///// <param name="options">Options for saving the context</param>
        //public int SaveChanges(SaveOptions options)
        //{
        //    return context.SaveChanges(options);
        //}

        //public void Refresh<T>(RefreshMode mode, T entity)
        //{
        //    context.Refresh(mode, entity);
        //}
        //public void Refresh<T>(RefreshMode mode, List<T> entities)
        //{
        //    context.Refresh(mode, entities);
        //}
       
        /// <summary>
        /// Releases all resources used by the WarrantManagement.DataExtract.Dal.ReportDataBase
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the WarrantManagement.DataExtract.Dal.ReportDataBase
        /// </summary>
        /// <param name="disposing">A boolean value indicating whether or not to dispose managed resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }


    }


}
