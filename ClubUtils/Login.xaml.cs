using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Web;

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
            if (ClubPicker.SelectedIndex != -1 && email.Text != "" && password.Password.Length > 0)
            {
                if (new_account.IsChecked == true && password_c.Password == password.Password && name.Text.Length > 0)
                {
                    string values = "(null, '";
                    values += name.Text + "','";
                    values += email.Text + "','";
                    //Store salted and hashed pwd, salt with email
                    values += Security.sha256_hash(password.Password + email.Text) + "','";
                    values += ClubPicker.SelectedValue.ToString() +"','";
                    values += "user')";
                    string query = "INSERT INTO `Users`(`ID`,`FullName`,`Email`,`Password`,`ClubName`,`Rank`) VALUES " + values;
                    Console.WriteLine(query);
                    DBHelper.ExecuteNonQuery(query);
                    Console.WriteLine(query + "RAN");
                }
                else
                {
                    Console.WriteLine("USER: " + email.Text + " TRIED TO LOGIN");
                    //TRY LOGIN
                }
                
            }
            else
            {
                //Require all fields
            }
        }
        
    }
}
