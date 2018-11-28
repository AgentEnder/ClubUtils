using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClubUtils
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        private List<string> clubs = DBHelper.getClubNames();

        public Login()
        {
            ConsoleHelper.Create();
            Console.WriteLine("CLUB UTILS SIGN IN WINDOW OPENED");
            DBHelper.connect();
            InitializeComponent();
            ClubPicker.ItemsSource = clubs;
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (ClubPicker.SelectedIndex != -1 && email.Text.Length > 3 && password.Password.Length > 0)
            {
                if (new_account.IsChecked == true)
                { //CREATE NEW ACCOUNT CODE
                    if (email.Text.Substring(email.Text.Length-3) != "edu")
                    {
                        Console.WriteLine("Email must end in .edu!");
                        return;
                    }
                    if (password_c.Password == password.Password && name.Text.Length > 0)
                    {
                        string account_exists_sql = "select count() from `Users` where " +
                                        "`ClubName` = '" + ClubPicker.SelectedValue.ToString() +
                                        "' and `Email` = '" + email.Text + "'";
                        SQLiteCommand exists_sql_query = new SQLiteCommand(account_exists_sql, DBHelper.getConnection());
                        SQLiteDataReader exists_reader = exists_sql_query.ExecuteReader();
                        if (exists_reader.Read()) //Duplicate Account
                        {
                            if (Int16.Parse(exists_reader[0].ToString()) > 0) //exists_reader[0] is an obj... hard to convert to bool or int.
                            {
                                email.BorderBrush = Brushes.DarkRed;
                                email.BorderThickness = new Thickness(2.0);
                                Console.WriteLine("An account with that email address already exists!");
                                return;
                            }
                        }
                        string values = "(null, '";
                        values += name.Text + "','";
                        values += email.Text + "','";
                        //Store salted and hashed pwd, salt with email
                        values += Security.sha256_hash(password.Password + email.Text) + "','";
                        values += ClubPicker.SelectedValue.ToString() + "','";
                        values += "User','";
                        values += DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "')";
                        string query = "INSERT INTO `Users`(`ID`,`FullName`,`Email`,`Password`,`ClubName`,`Rank`,`JoinDate`) VALUES " + values;
                        DBHelper.ExecuteNonQuery(query);
                        new_account.IsChecked = !new_account.IsChecked; 
                    }
                }
                else
                { //LOGIN CODE
                    string enc_pwd = Security.sha256_hash(password.Password + email.Text);
                    string sql = "select * from Users where `email` = ";
                    sql += "'" + email.Text + "'";
                    sql += "and `ClubName` = '" + ClubPicker.SelectedValue.ToString() + "'";
                    SQLiteCommand sql_query = new SQLiteCommand(sql, DBHelper.getConnection());
                    SQLiteDataReader reader = sql_query.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("POSSIBLE ACCOUNT: " + reader["FullName"]);
                        if ((string)reader["Password"] == enc_pwd)
                        {
                            Console.WriteLine(reader["FullName"] + " LOGGED IN");
                            Member member = new Member((string)reader["FullName"], (string)reader["Email"], (string)reader["ClubName"], (string)reader["Rank"]);
                            Globals.currentMember = member;
                            Console.WriteLine("Logged in at rank: " + member.rank.ToString());
                            MainWindow temp = new MainWindow(member);
                            temp.Show();
                            this.Close();
                            return; //LOGIN COMPLETE
                        }
                        else
                        {
                            Console.WriteLine("INVALID PASSWORD");
                            password.BorderBrush = Brushes.DarkRed;
                            password.BorderThickness = new Thickness(2.0);
                        }
                    }
                }

            }
            else
            {
                //Require all fields
            }
        }


        private void FileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.Source as MenuItem;

            switch (mi.Name)
            {
                case "Exit":
                    {
                        this.Close();
                    }
                    break;
            }
        }
    }
}
