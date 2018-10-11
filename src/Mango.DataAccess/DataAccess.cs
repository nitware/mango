using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Mango.DataAccess
{
    public class DataAccess
    {
        private SqlConnection conn;
        private static string excel03ConnString = ConfigurationManager.ConnectionStrings["excelO3ConString"].ToString();
        private static string excel07ConnString = ConfigurationManager.ConnectionStrings["excelO7ConString"].ToString();
        private static string connString = ConfigurationManager.ConnectionStrings["sapEntities"].ToString();

        //private static string connString = "server=.;database=SPAS;Trusted_Connection=Yes;";

        public DataAccess()
        {
            conn = new SqlConnection(connString);
        }

        public static string ConnString
        {
            get { return connString; }
        }
        public static string Excel03ConnString
        {
            get { return excel03ConnString; }
        }
        public static string Excel07ConnString
        {
            get { return excel07ConnString; }
        }

        public DataSet ExecuteDataset(string cmdText, ArrayList commandParameters, string tableName)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();

                if (conn.State == ConnectionState.Closed)
                {
                    conn = new SqlConnection(connString);
                    conn.Open();
                }

                PrepareCommand(cmd, null, CommandType.StoredProcedure, cmdText, commandParameters);

                da = new SqlDataAdapter(cmd);
                da.Fill(ds, tableName);
                //conn.Close();

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //public DataSet ExecuteDatasetWithTransaction(string cmdText, ArrayList commandParameters, string tableName, Transaction transaction)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        SqlDataAdapter da = new SqlDataAdapter();
        //        SqlCommand cmd = new SqlCommand();
                
        //        conn = transaction.connection;
        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn = new SqlConnection(connString);
        //            conn.Open();
        //        }

        //        PrepareCommand(cmd, transaction.sqlTransactionObj, CommandType.StoredProcedure, cmdText, commandParameters);

        //        da = new SqlDataAdapter(cmd);
        //        da.Fill(ds, tableName);
        //        //conn.Close();

        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}


        public int ExecuteProc(string cmdText, ArrayList commandParameters, Transaction tr)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();

                conn = tr.connection;
                if (conn.State == ConnectionState.Closed)
                {
                    conn = new SqlConnection(connString);
                    conn.Open();
                }

                PrepareCommand(cmd, tr.sqlTransactionObj, CommandType.StoredProcedure, cmdText, commandParameters);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public object ExecuteProcWithOutputParam(string cmdText, ArrayList commandParameters, Transaction tr)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand();

                conn = tr.connection;
                if (conn.State == ConnectionState.Closed)
                {
                    conn = new SqlConnection(connString);
                    conn.Open();
                }

                PrepareCommand(cmd, tr.sqlTransactionObj, CommandType.StoredProcedure, cmdText, commandParameters);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    return -1;
                }
                else
                {
                    return cmd.Parameters[0].Value;
                    //return cmd.Parameters[5].Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PrepareCommand(SqlCommand command, SqlTransaction transaction, CommandType commandType, string commandText, ArrayList commandParameters)
        {
            try
            {
                //'if the provided connection is not open, we will open it 
                //' conn.Open()
                //'associate the connection with the command 
                command.Connection = conn;

                //'set the command text (stored procedure name or SQL statement) 
                command.CommandText = commandText; //'if we were provided a transaction, assign it. 
                if (transaction != null)
                {
                    command.Transaction = transaction;
                }

                //'set the command type 
                command.CommandType = commandType; //'attach the command parameters if they are provided 
                if (commandParameters != null)
                {
                    AttachParameters(command, commandParameters);
                }

                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AttachParameters(SqlCommand command, ArrayList commandParameters)
        {
            try
            {
                foreach (SqlParameter p in commandParameters)
                {
                    //'check for derived output value with no value assigned 
                    if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                    {
                        p.Value = null;
                    }

                    command.Parameters.Add(p);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            try
            {
                DateTime defaultDate = Convert.ToDateTime(null);
                SqlParameter param;

                if (Size > 0)
                {
                    param = new SqlParameter(ParamName, DbType, Size);
                }
                else
                {
                    param = new SqlParameter(ParamName, DbType);
                }

                param.Direction = ParameterDirection.Input;
                if (Value is DateTime)
                {
                    if (defaultDate == Convert.ToDateTime(Value))
                    {
                        param.Value = null;
                    }
                    else
                    {
                        param.Value = Value;
                    }
                }
                else if (Value is Int32)
                {
                    {
                        param.Value = Value;
                    }
                }
                else
                {
                    if (Value != null && Value.ToString().Length > 0)
                    {
                        param.Value = Value;
                    }
                    else
                    {
                        param.Value = null;
                    }
                }

                return param;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlParameter MakeOutputParam(string ParamName, SqlDbType DbType, int Size)
        {
            try
            {
                SqlParameter param;
                if (Size > 0)
                {
                    param = new SqlParameter(ParamName, DbType, Size);
                }
                else
                {
                    param = new SqlParameter(ParamName, DbType);
                }

                param.Direction = ParameterDirection.Output;

                return param;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}