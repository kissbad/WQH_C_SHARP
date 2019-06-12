using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC_API.Models.Request
{
    public class getGoodsList
    {
        public int page_index;
        public int page_size;
        public List<object> get()
        {
            using (SqlConnection conn = new SqlConnection(BusinessClass.Global.ConnectionString))
            {
                return conn.Query<object>("getGoodsList", new { this.page_index, this.page_size },null,true,null,CommandType.StoredProcedure).ToList();
            }
        }
    }
}