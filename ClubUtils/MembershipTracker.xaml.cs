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
    /// Interaction logic for MembershipTracker.xaml
    /// </summary>
    public partial class MembershipTracker : Window
    {
        private List<string> members = DBHelper.getMembersFromClub(Globals.currentMember.clubName);
        public MembershipTracker()
        {
            InitializeComponent();
            LstBoxMembership.ItemsSource = members;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LstBoxMembership.ItemsSource = members;
        }
    }
}
