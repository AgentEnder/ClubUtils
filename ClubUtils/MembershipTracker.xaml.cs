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
        private List<string> members = new List<string>();
        public MembershipTracker()
        {
            InitializeComponent();
            List<Member> member_instances = DBHelper.getMembersFromClub(Globals.currentMember.clubName);
            foreach (var member in member_instances)
            {
                members.Add(member.fullName);
            }
          //  LstBoxMembership.ItemsSource = members;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        //    LstBoxMembership.ItemsSource = members;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
