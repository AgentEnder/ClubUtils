using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ClubUtils
{
    static class dbHelper
    {
        private static bool isConnected = false;
        private static SQLiteConnection conn;
        
        public bool Connect()
        {
            conn = new SQLiteConnection("Data Source = db.db; Version = 3;")
        }

    }
}
