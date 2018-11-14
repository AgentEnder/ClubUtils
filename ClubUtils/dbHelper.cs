using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;

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

        public static DataTable getUserTable()
        {
            string cmdString = "Select FullName, Email, ClubName, Rank, JoinDate from Users where `ClubName` = '" + Globals.currentMember.clubName + "'";
            SQLiteCommand cmd = new SQLiteCommand(cmdString, conn);
            SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable("Users");
            sda.Fill(dt);
            return dt;
        }

        public static List<Member> getMembersFromClub(String club)
        {
            if (!isConnected)
                connect();
            SQLiteCommand sql_query = new SQLiteCommand("select * from Users where `ClubName` = '" + club + "'", conn);
            SQLiteDataReader reader = sql_query.ExecuteReader();
            List<Member> return_data = new List<Member>();
            while (reader.Read())
            {
                Member temp = new Member(reader["FullName"].ToString(), reader["Email"].ToString(), reader["ClubName"].ToString(), reader["Rank"].ToString());
                return_data.Add(temp);
            }
            return return_data;
        }

        public static List<Event> getEventsFromClub(String club)
        {
            if (!isConnected)
                connect();
            SQLiteCommand sql_query = new SQLiteCommand("select * from Events where `ClubName` = '" + club + "'", conn);
            SQLiteDataReader reader = sql_query.ExecuteReader();
            List<Event> return_data = new List<Event>();
            while (reader.Read())
            {
                bool recurs = false;
                short temp_r;
                if (Int16.TryParse(reader["Recurring"].ToString(), out temp_r))
                {
                    if (temp_r == 1)
                    {
                        recurs = true;
                    }
                }
                DateTime stopTime;
                try
                {
                    stopTime = DateTime.Parse(reader["StopTime"].ToString());
                }
                catch (Exception)
                {
                    stopTime = DateTime.MaxValue;
                }
                Event temp = new Event(reader["EventName"].ToString(), DateTime.Parse(reader["EventTime"].ToString()),stopTime, reader["ClubName"].ToString(), recurs);
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
            catch (SQLiteException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static SQLiteConnection getConnection()
        {
            return conn;
        }

        public static List<Bulletin> getBulletinsFromClub(String club)
        {
            if (!isConnected)
                connect();
            SQLiteCommand sql_query = new SQLiteCommand("select * from Bulletins where `ClubName` = '" + club + "'", conn);
            SQLiteDataReader reader = sql_query.ExecuteReader();
            List<Bulletin> return_data = new List<Bulletin>();
            while (reader.Read())
            {
                Bulletin temp = new Bulletin(reader["Heading"].ToString(), DateTime.Parse(reader["Timestamp"].ToString()), reader["Text"].ToString(), club);
                return_data.Add(temp);
            }
            return return_data;
        }

    }
}
