using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using WQH.Data;

namespace WQH.system
{
    public class Trans : IDisposable
    {
        // Fields
        private DbConnection conn;
        private DbTransaction dbTrans;

        // Methods
        public Trans()
        {
            this.conn = DbHelper.CreateConnection();
            this.conn.Open();
            this.dbTrans = this.conn.BeginTransaction();
        }

        public Trans(string connectionString)
        {
            this.conn = DbHelper.CreateConnection(connectionString);
            this.conn.Open();
            this.dbTrans = this.conn.BeginTransaction();
        }

        public void Colse()
        {
            if (this.conn.State == ConnectionState.Open)
            {
                this.conn.Close();
            }
        }

        public void Commit()
        {
            this.dbTrans.Commit();
            this.Colse();
        }

        public void Dispose()
        {
            this.Colse();
        }

        public void RollBack()
        {
            this.dbTrans.Rollback();
            this.Colse();
        }

        // Properties
        public DbConnection DbConnection
        {
            get
            {
                return this.conn;
            }
        }

        public DbTransaction DbTrans
        {
            get
            {
                return this.dbTrans;
            }
        }
    }
}
