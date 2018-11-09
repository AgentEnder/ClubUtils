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
using System.Windows.Shapes;

namespace ClubUtils
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(Member member)
        {
            InitializeComponent();
            welcomeLabel.Content += Globals.currentMember.fullName;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.Source as MenuItem;

            switch (mi.Name)
            {
                case "Attendance":
                    { 
                        break;
                    }

                case "Membership":
                    {
                        MembershipTracker test = new MembershipTracker();
                        test.Show();
                        this.Close();
                        break;
                    }

                case "Financials":
                    {
                        break;
                    }
                case "Emails":
                    {
                        break;
                    }
            }
        }
    }
}
