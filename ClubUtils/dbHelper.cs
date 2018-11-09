using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ClubUtils
{
    static class DBHelper
    {
        private static bool isConnected = false;
        private static SQLiteConnection conn;
        
        public static bool connect()
        {
            if (isConnected)
            {
                return true;
            }
            conn = new SQLiteConnection("Data Source = db.db; Version = 3;");
            try
            {
                conn.Open();
                //Console.WriteLine(conn.FileName);
                isConnected = true;
                return true;
            }
            catch (SQLiteException e)
            {
                ConsoleHelper.Create();
                Console.WriteLine(e);
                return false;
            }
        }

        public static List<string> getClubNames()
        {
            if (!isConnected)
                connect();
            SQLiteCommand sql_query = new SQLiteCommand("select ClubName from Clubs", conn);
            SQLiteDataReader reader = sql_query.ExecuteReader();
            List<string> return_data = new List<string>();
            while (reader.Read())
            {
                return_data.Add(reader["ClubName"].ToString());
            }
            return return_data;
        }

        public static List<Member> getMembersFromClub(String club)
        {
            if (!isConnected)
                connect();
            SQLiteCommand sql_query = new SQLiteCommand("select * from Users where `ClubName` = '" +  club +"'", conn);
            SQLiteDataReader reader = sql_query.ExecuteReader();
            List<Member> return_data = new List<Member>();
            while (reader.Read())
            {
                Member temp = new Member(reader["FullName"].ToString(), reader["Email"].ToString(), reader["ClubName"].ToString(), reader["Rank"].ToString());
                return_data.Add(temp);
            }
            return return_data;
        }

        

        public static bool ExecuteNonQuery(string sql)
        {
            try
            {
                SQLiteCommand non_query = new SQLiteCommand(sql, conn);
                non_query.ExecuteNonQuery();
                return true;
            }
            catch(SQLiteException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static SQLiteConnection getConnection()
        {
            return conn;
        }

    }
}
