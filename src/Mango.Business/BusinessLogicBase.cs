using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

using System.Data.Objects.DataClasses;
using System.Data;
using Mango.Model;
using Mango.Data;

namespace Mango.Business
{
    public abstract class BusinessLogicBase<T, E> where T : class where E : EntityObject
    {
        protected IRepository repository = new Repository();
        protected TranslatorBase<T, E> translator;
        protected const string ArgumentNullException = "Null object argument. Please contact your system administartor";
        protected const string UpdateException = "Operation failed due to update exception!";
        protected const string NoItemModified = "No item modified";
        protected const string NoItemRemoved = "No item removed";
        protected const string ErrowDuringProccesing = "Errow Occured During Processing.";
        protected const string NoContextSupplied = "Context wasn't supplied!";
        protected const string AlreadyInUse = "The DELETE statement conflicted with the REFERENCE constraint";
        protected const string DuplicateKeyDetected = "Cannot insert duplicate key";
        protected const string DuplicateKeyDetectedMessage = "Duplicate ID detected! Record already exist with the same ID! kindly correct and try again.";

        public T GetModelBy(Func<E, bool> predicate)
        {
            try
            {
                E entity = repository.Single(predicate);
                return translator.Translate(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<T> GetModelsBy(Func<E, bool> predicate)
        {
            try
            {
                List<E> entity = repository.Find(predicate).ToList();
                return translator.Translate(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual List<T> GetAll()
        {
            try
            {
                List<E> entities = repository.Fetch<E>().ToList();
                List<T> models = translator.Translate(entities);
                return models;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual T Add(T model)
        {
            try
            {
                E entity = translator.Translate(model);
                repository.Add(entity);
                repository.SaveChanges();
                return translator.Translate(entity);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(ArgumentNullException);
            }
            catch (UpdateException uex)
            {
                if (uex.InnerException.Message.Contains(DuplicateKeyDetected))
                {
                    throw new Exception(DuplicateKeyDetectedMessage);
                }

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public virtual T Add(T model)
        //{
        //    try
        //    {
        //        E entity = AddHelper(model);
        //        return translator.Translate(entity);
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        throw new ArgumentNullException(ArgumentNullException);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private E AddHelper(T model)
        //{
        //    try
        //    {
        //        E entity = translator.Translate(model);
        //        repository.Add(entity);
        //        return entity;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public virtual T AddAndSave(T model)
        //{
        //    try
        //    {
        //        E entity = AddHelper(model);
        //        ObjectContextManager.SummitChanges();
        //        return translator.Translate(entity);
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        throw new ArgumentNullException(ArgumentNullException);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public virtual int Add(List<T> models)
        {
            try
            {
                List<E> entities = translator.Translate(models);
                foreach (E entity in entities)
                {
                    repository.Add(entity);
                }

                return repository.SaveChanges();
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(ArgumentNullException);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual bool Remove(Func<E, bool> selector)
        {
            try
            {
                int rowsAffected = 0;
                IEnumerable<E> entities = repository.Find(selector);
                if (entities == null || entities.Count() == 0)
                {
                    return true;
                }

                //repository.Delete(selector);
                //return true;

                foreach (E entity in entities)
                {
                    repository.Delete(entity);
                    rowsAffected++;
                }

                if (rowsAffected > 0)
                {
                    return true;
                }
                else if (rowsAffected == 0)
                {
                    throw new Exception(NoItemModified);
                }
                else
                {
                    throw new Exception(ErrowDuringProccesing);
                }
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException(ArgumentNullException);
            }
            catch (UpdateException uex)
            {
                if (uex.InnerException.Message.Contains(AlreadyInUse))
                {
                    throw new Exception("This item is currently associated with other items in the system and cannot be removed at this time. Kindly remove all associated items prior to its removal.");
                }

                throw new UpdateException(UpdateException);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(ArgumentNullException);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public E GetEntityBy(Func<E, bool> predicate)
        {
            try
            {
                return repository.Single(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

       











        //public T GetModelBy(Func<E, bool> predicate)
        //{
        //    try
        //    {
        //        E entity = contextManager.Repository.Single(predicate);
        //        return translator.Translate(entity);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public List<T> GetModelsBy(Func<E, bool> predicate)
        //{
        //    try
        //    {
        //        List<E> entity = contextManager.Repository.Find(predicate).ToList();
        //        return translator.Translate(entity);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public virtual List<T> GetAll()
        //{
        //    try
        //    {
        //        List<E> entities = contextManager.Repository.Fetch<E>().ToList();
        //        List<T> models = translator.Translate(entities);
        //        return models;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public virtual T Add(T model)
        //{
        //    try
        //    {
        //        E entity = AddHelper(model);
        //        return translator.Translate(entity);
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        throw new ArgumentNullException(ArgumentNullException);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private E AddHelper(T model)
        //{
        //    try
        //    {
        //        E entity = translator.Translate(model);
        //        contextManager.Repository.Add(entity);
        //        return entity;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public virtual T AddAndSave(T model)
        //{
        //    try
        //    {
        //        E entity = AddHelper(model);
        //        contextManager.SummitChanges();
        //        return translator.Translate(entity);
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        throw new ArgumentNullException(ArgumentNullException);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public virtual int Add(List<T> models)
        //{
        //    try
        //    {
        //        List<E> entities = translator.Translate(models);
        //        foreach (E entity in entities)
        //        {
        //            contextManager.Repository.Add(entity);
        //        }

        //        return entities.Count;

        //        //return unitOfWork.Repository.SaveChanges();
                
        //        //return unitOfWork.Commit();
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        throw new ArgumentNullException(ArgumentNullException);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public virtual bool Remove(Func<E, bool> selector)
        //{
        //    try
        //    {
        //        int rowsAffected = 0;
        //        IEnumerable<E> entities = contextManager.Repository.Find(selector);
        //        if (entities == null || entities.Count() == 0)
        //        {
        //            return true;
        //        }

        //        foreach (E entity in entities)
        //        {
        //            contextManager.Repository.Delete(entity);
        //            rowsAffected++;
        //        }

        //        if (rowsAffected > 0)
        //        {
        //            return true;
        //        }
        //        else if (rowsAffected == 0)
        //        {
        //            throw new Exception(NoItemModified);
        //        }
        //        else
        //        {
        //            throw new Exception(ErrowDuringProccesing);
        //        }
        //    }
        //    catch (NullReferenceException)
        //    {
        //        throw new NullReferenceException(ArgumentNullException);
        //    }
        //    catch (UpdateException)
        //    {
        //        throw new UpdateException(UpdateException);
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        throw new ArgumentNullException(ArgumentNullException);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public E GetEntityBy(Func<E, bool> predicate)
        //{
        //    try
        //    {
        //        return contextManager.Repository.Single(predicate);
        //    }
        //    catch (Exception )
        //    {
        //        throw ;
        //    }
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //private void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (contextManager != null)
        //        {
        //            contextManager.Dispose();
        //        }
        //    }
        //}












        //private ObjectContextManager contextManager;

        //public BusinessLogicBase()
        //{
        //    if (contextManager == null)
        //    {
        //        contextManager = new ObjectContextManager();
        //    }
        //}

        //protected TranslatorBase<T, E> translator;
        //protected const string ArgumentNullException = "Null object argument. Please contact your system administartor";
        //protected const string UpdateException = "Operation failed due to update exception!";
        //protected const string NoItemModified = "No item modified";
        //protected const string NoItemRemoved = "No item removed";
        //protected const string ErrowDuringProccesing = "Errow Occured During Processing.";
        //protected const string NoContextSupplied = "Context wasn't supplied!";

        //public ObjectContextManager ContextManager
        //{
        //    get { return contextManager; }
        //}

        //public T GetModelBy(Func<E, bool> predicate)
        //{
        //    try
        //    {
        //        E entity = contextManager.Repository.Single(predicate);
        //        return translator.Translate(entity);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public List<T> GetModelsBy(Func<E, bool> predicate)
        //{
        //    try
        //    {
        //        List<E> entity = contextManager.Repository.Find(predicate).ToList();
        //        return translator.Translate(entity);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public virtual List<T> GetAll()
        //{
        //    try
        //    {
        //        List<E> entities = contextManager.Repository.Fetch<E>().ToList();
        //        List<T> models = translator.Translate(entities);
        //        return models;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public virtual T Add(T model)
        //{
        //    try
        //    {
        //        E entity = AddHelper(model);
        //        return translator.Translate(entity);
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        throw new ArgumentNullException(ArgumentNullException);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private E AddHelper(T model)
        //{
        //    try
        //    {
        //        E entity = translator.Translate(model);
        //        contextManager.Repository.Add(entity);
        //        return entity;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public virtual T AddAndSave(T model)
        //{
        //    try
        //    {
        //        E entity = AddHelper(model);
        //        contextManager.SummitChanges();
        //        return translator.Translate(entity);
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        throw new ArgumentNullException(ArgumentNullException);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public virtual int Add(List<T> models)
        //{
        //    try
        //    {
        //        List<E> entities = translator.Translate(models);
        //        foreach (E entity in entities)
        //        {
        //            contextManager.Repository.Add(entity);
        //        }

        //        return entities.Count;
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        throw new ArgumentNullException(ArgumentNullException);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public virtual bool Remove(Func<E, bool> selector)
        //{
        //    try
        //    {
        //        int rowsAffected = 0;
        //        IEnumerable<E> entities = contextManager.Repository.Find(selector);
        //        if (entities == null || entities.Count() == 0)
        //        {
        //            return true;
        //        }

        //        foreach (E entity in entities)
        //        {
        //            contextManager.Repository.Delete(entity);
        //            rowsAffected++;
        //        }

        //        if (rowsAffected > 0)
        //        {
        //            return true;
        //        }
        //        else if (rowsAffected == 0)
        //        {
        //            throw new Exception(NoItemModified);
        //        }
        //        else
        //        {
        //            throw new Exception(ErrowDuringProccesing);
        //        }
        //    }
        //    catch (NullReferenceException)
        //    {
        //        throw new NullReferenceException(ArgumentNullException);
        //    }
        //    catch (UpdateException)
        //    {
        //        throw new UpdateException(UpdateException);
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        throw new ArgumentNullException(ArgumentNullException);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public E GetEntityBy(Func<E, bool> predicate)
        //{
        //    try
        //    {
        //        return contextManager.Repository.Single(predicate);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

       



    }



}
