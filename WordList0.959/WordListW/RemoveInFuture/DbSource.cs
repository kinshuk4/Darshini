using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using WordListW.Utils;

namespace WordListW.RemoveInFuture
{
    public class DbSource
    {
        public DbSource()
        {
            OleDbCommand cmdMyQuery = new OleDbCommand("SELECT * FROM DictionaryDB");

            DataTable dTable = DbUtils.SQLSelect(cmdMyQuery);
            m_orders = dTable;
        }

        public DataTable Orders
        {
            get
            {
                return m_orders;
            }
        }
        private DataTable m_orders;
    }
}
