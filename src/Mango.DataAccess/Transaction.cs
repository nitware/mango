using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
//using System.ServiceModel;

namespace Mango.DataAccess
{
    public class Transaction : IDisposable
    {
        private bool happy = true;
        private bool done = false;
        public SqlTransaction sqlTransactionObj;
        public SqlConnection connection;
        private bool disposed = false;

        public Transaction() { }

        public Transaction(string connString)
        {
            try
            {
                connection = new SqlConnection(connString);

                try
                {
                    connection.Open();
                }
                catch (SqlException e)
                {
                    //' -- throw exception with incorrect connstring
                    throw new ApplicationException("Unable to open connection." + connString + "! " + e.Message);
                }

                //' -- starts transaction
                sqlTransactionObj = connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                //' -- try to close connection if it is open
                try
                {
                    connection.Close();
                    throw ex;
                }
                catch (Exception ex2)
                {
                    throw ex2;
                }
            }
        }

        public SqlTransaction SqlTransaction
        {
            get
            {
                //' -- check the object has not been disposed
                if (disposed == true)
                {
                    throw new ObjectDisposedException("Transaction");
                }

                //' -- il object's lifetime has expired, caller cannot 
                //' access the transaction
                if (this.done == true)
                {
                    throw new InvalidOperationException("Transaction has been closed and can no longer be used");
                }

                //' -- returns sqlTransaction internally referenced
                return sqlTransactionObj;
            }
        }

        public void Commit()
        {
            //' -- checks the object has not been disposed
            if (disposed == true)
            {
                throw new ObjectDisposedException("SqlTransaction");
            }

            //' -- checks a commit or rollback has not been executed
            if (done == true)
            {
                throw new InvalidOperationException("Transaction has yet been committed/rolled back");
            }

            //' -- checks we are happy and therefore can commit 
            //' the transaction
            if (happy == false)
            {
                throw new InvalidOperationException("Transaction has commit disabled and cannot be commited");
            }

            try
            {
                //' -- commits the transaction
                sqlTransactionObj.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //' -- updates status to show this object's lifetime 
                //' has expired
                done = true;

                //' -- Always close underlying database connection 
                if (sqlTransactionObj.Connection != null)
                {
                    if (sqlTransactionObj.Connection.State == ConnectionState.Open)
                    {
                        sqlTransactionObj.Connection.Close();
                    }
                    sqlTransactionObj.Connection.Dispose();
                }

                //' -- Disposed transaction is no longer useful
                sqlTransactionObj.Dispose();

            }
        }

        public void Abort()
        {
            // -- checks if the object has been disposed
            if (disposed == true)
            {
                throw new ObjectDisposedException("SqlTransaction");
            }

            // -- checks a commit or rollback has not been executed
            if (done == true)
            {
                throw new InvalidOperationException("Transaction has yet been committed/rolled back");
            }

            try
            {
                // -- Rolls back the transaction
                sqlTransactionObj.Rollback();

                // -- updates status to unhappy
                happy = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // -- update status because object's lifetime has expired
                done = true;

                // -- closes underlying connection to database
                if (sqlTransactionObj.Connection != null)
                {
                    if (sqlTransactionObj.Connection.State == ConnectionState.Open)
                    {
                        sqlTransactionObj.Connection.Close();
                    }

                    sqlTransactionObj.Connection.Dispose();
                }

                // -- Disposes transaction that now is no more useful
                sqlTransactionObj.Dispose();
            }
        }

        public void DisableCommit()
        {
            //' -- checks that the object has not been disposed
            if (disposed == true)
            {
                this.happy = false;
                throw new ObjectDisposedException("SqlTransaction");
            }
        }

        protected void Dispose(bool disposing)
        {
            if (disposed == true)
            {
                return;
            }

            if (disposing == true)
            {
                //' -- Avoid runtime error when disposing 
                //' (connction broken, etc.)
                try
                {
                    //' -- if transaction has not been committed or rolled 
                    //' back we need to close it
                    if (done == false)
                    {
                        //' -- Commits if the object is happy, 
                        //' otherwise rolls back
                        if (this.happy == true)
                        {
                            sqlTransactionObj.Commit();
                        }
                        else
                        {
                            sqlTransactionObj.Rollback();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                try
                {
                    //' -- Important: closes and releases reference 
                    //' to transaction
                    connection.Close();
                    connection.Dispose();
                    SqlTransaction.Dispose();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error closing transaction's resources:" + ex.Message);
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            //' -- makes resources cleanup
            Dispose(true);

            //' -- suppress call to finalize
            GC.SuppressFinalize(this);
        }




    }
}