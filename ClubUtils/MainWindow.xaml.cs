using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Security;

namespace ClubUtils
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> clubs = DBHelper.getClubNames();
        public MainWindow()
        {
            ConsoleHelper.Create();
            Console.WriteLine("CLUB UTILS SIGN IN WINDOW OPENED");
            DBHelper.connect();
            InitializeComponent();
            ClubPicker.ItemsSource = clubs;
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (ClubPicker.SelectedIndex != -1 && email.Text != "" && password.SecurePassword.Length > 0)
            {
                if (new_account.IsChecked == true)
                {
                    string values = "(null, '";
                    values += email.Text + "',";
                    values += "null,'" + password.SecurePassword.GetHashCode();
                    values += "','" + ClubPicker.SelectedValue.ToString();
                    values+="','user')";
                    string query = "INSERT INTO `Users`(`ID`,`FullName`,`Email`,`Password`,`ClubName`,`Rank`) VALUES " + values;
                    Console.WriteLine(query);
                    DBHelper.ExecuteNonQuery(query);
                }
                else
                {
                    //TRY LOGIN
                }
                Console.WriteLine("USER: " + email.Text + " TRIED TO LOGIN");
            }
            else
            {
                //Require all fields
            }
        }
    }
}
