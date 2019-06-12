using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using WQH.Data;


namespace WindowsService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        Thread th = new Thread(ThreadAction);

        protected override void OnStart(string[] args)
        {
            WQH.system.IO.File.WriteLog("服务开始 ", 1);

            //开始线程
            th.IsBackground = true;
            th.Start();
        }

 

        public static void ThreadAction()
        {
            while (true)
            {
                if (DateTime.Now.Minute.ToString() == "59")
                {
                    WQH.system.IO.File.WriteLog("开始同步商品数据", 1);

                    string sql = @"with temp as(select DISTINCT GoodsId from collectLog 
where CrateDateTime > Dateadd(d, -100, getdate()) and isnull(GoodsId,'') <> '')
select a.GoodsId,a.GoodsLink from goods a join temp b on a.GoodsId = b.GoodsId";

                    DataTable dt = SqlHelper.ExecuteReader( CommandType.Text, sql, null);

                    using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                    {
                        connection.Open();
                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                string sql2 = "exec recordGoodsSale @GoodsId,@SaleCount";

                                SqlHelper.ExecuteNonQuery(connection, null, CommandType.Text, sql2, new SqlParameter[]{
                                    new System.Data.SqlClient.SqlParameter("@GoodsId",row["GoodsId"].ToString()),
                                    new System.Data.SqlClient.SqlParameter("@SaleCount","100"),
                                });
                                Thread.Sleep(1 * 1000);
                            }
                            catch (Exception ex)
                            {
                                WQH.system.IO.File.WriteLog(row["GoodsId"].ToString() + "|" + ex.Message.ToString(), 1);
                                WQH.system.IO.File.WriteLog(ex.StackTrace, 0);
                            }
                        }
                        if (connection.State != ConnectionState.Closed)
                        {
                            connection.Close();
                        }
                    }
                    WQH.system.IO.File.WriteLog("同步商品数据结束", 1);
                }
                Thread.Sleep(60 * 1000);
            }
        }

        public static void ThreadAction2()
        {

        }

        protected override void OnStop()
        {
            th.Abort();
            WQH.system.IO.File.WriteLog("服务停止 ", 1);
        }
    }
}
