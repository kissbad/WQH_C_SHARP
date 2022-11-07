using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oracle
{
    class test
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DataTable datatable = ExecuteDataTable("select * from sys_ddl");
            string js = JsonConvert.SerializeObject(datatable);
            var i = datatable.Rows[2][2];//.Columns[1];
        }

        static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=gdqfyy.top)(PORT=8563)) (CONNECT_DATA=(SID=orcl)));User ID=qfmn;Password=qfmn1234;";
        public static OracleDataReader ExecuteReader(string strSQL)
        {
            OracleConnection connection = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand(strSQL, connection);
            try
            {
                connection.Open();
                OracleDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (OracleException e)
            {
                throw new Exception(e.Message);
            }

        }
        public static int ExecuteNonQuery(string sql, params OracleParameter[] parameters)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ExecuteDataTable(string sql, params OracleParameter[] parameters)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    return datatable;
                }
            }
        }
    }
}
