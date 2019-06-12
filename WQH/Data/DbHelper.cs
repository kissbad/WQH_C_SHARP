using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using WQH.system;

namespace WQH.Data
{
    public class DbHelper
    {
        // Fields
        private DbConnection connection;
        private static string dbConnectionString = ConfigurationManager.ConnectionStrings["Center"].ConnectionString;
        private static string dbProviderName = "System.Data.SqlClient";

        // Methods
        public DbHelper()
        {
            this.connection = CreateConnection(dbConnectionString);
        }

        public DbHelper(string connectionString)
        {
            this.connection = CreateConnection(connectionString);
        }

        public void AddInParameter(DbCommand cmd, string parameterName, object value)
        {
            DbParameter parameter = cmd.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(parameter);
        }

        public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
        {
            DbParameter parameter = cmd.CreateParameter();
            parameter.DbType = dbType;
            parameter.ParameterName = parameterName;
            parameter.Size = size;
            parameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parameter);
        }

        public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
        {
            foreach (DbParameter parameter in dbParameterCollection)
            {
                cmd.Parameters.Add(parameter);
            }
        }

        public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
        {
            DbParameter parameter = cmd.CreateParameter();
            parameter.DbType = dbType;
            parameter.ParameterName = parameterName;
            parameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(parameter);
        }

        public static DbConnection CreateConnection()
        {
            DbConnection connection = DbProviderFactories.GetFactory(dbProviderName).CreateConnection();
            connection.ConnectionString = dbConnectionString;
            return connection;
        }

        public static DbConnection CreateConnection(string connectionString)
        {
            DbConnection connection = DbProviderFactories.GetFactory(dbProviderName).CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }

        public DataSet ExecuteDataSet(DbCommand cmd)
        {
            DbDataAdapter adapter = DbProviderFactories.GetFactory(dbProviderName).CreateDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet;
        }

        public DataSet ExecuteDataSet(DbCommand cmd, Trans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbDataAdapter adapter = DbProviderFactories.GetFactory(dbProviderName).CreateDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet;
        }

        public DataTable ExecuteDataTable(DbCommand cmd)
        {
            DbDataAdapter adapter = DbProviderFactories.GetFactory(dbProviderName).CreateDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable ExecuteDataTable(DbCommand cmd, Trans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbDataAdapter adapter = DbProviderFactories.GetFactory(dbProviderName).CreateDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }

        public int ExecuteNonQuery(DbCommand cmd)
        {
            cmd.Connection.Open();
            int num = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return num;
        }

        public int ExecuteNonQuery(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            return cmd.ExecuteNonQuery();
        }

        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            cmd.Connection.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public DbDataReader ExecuteReader(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            return reader;
        }

        public object ExecuteScalar(DbCommand cmd)
        {
            cmd.Connection.Open();
            object obj2 = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return obj2;
        }

        public object ExecuteScalar(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            return cmd.ExecuteScalar();
        }

        public DbParameter GetParameter(DbCommand cmd, string parameterName)
        {
            return cmd.Parameters[parameterName];
        }

        public DbCommand GetSqlStringCommond(string sqlQuery)
        {
            DbCommand command = this.connection.CreateCommand();
            command.CommandText = sqlQuery;
            command.CommandType = CommandType.Text;
            return command;
        }

        public DbCommand GetStoredProcCommond(string storedProcedure)
        {
            DbCommand command = this.connection.CreateCommand();
            command.CommandText = storedProcedure;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }
    }
}
