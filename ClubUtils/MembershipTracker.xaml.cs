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
using System.Data;


namespace ClubUtils
{
    /// <summary>
    /// Interaction logic for MembershipTracker.xaml
    /// </summary>
    public partial class MembershipTracker : Window
    {
        private List<string> members = new List<string>();
        MessageBoxResult result;
        public MembershipTracker()
        {
            InitializeComponent();

            DataGridMembershipTracker.ItemsSource = DBHelper.getUserTable().DefaultView;
            DataGridMembershipTracker.CanUserAddRows = false;

            if (Globals.currentMember.rank > Member.ranks.VICE_PRESIDENT)
            {
                DataGridMembershipTracker.IsReadOnly = false;                
            }
            else
            {
                DataGridMembershipTracker.IsReadOnly = true;
                ButtonSaveMembershipTracker.Visibility = Visibility.Hidden;
                ButtonSaveMembershipTracker.Visibility = Visibility.Collapsed;
            }


            //    List<Member> member_instances = DBHelper.getMembersFromClub(Globals.currentMember.clubName);
            //    foreach (var member in member_instances)
            //    {
            //        members.Add(member.fullName);
            //    }
            //  LstBoxMembership.ItemsSource = members;
        }

        private void ButtonSaveMembershipTracker_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBox.Show("Are you sure you want to save? These changes will be permanent.","Save?", MessageBoxButton.YesNo, MessageBoxImage.Warning); 
            if (result == MessageBoxResult.Yes)
            {
                DataTable data = new DataTable();
                data = ((DataView)DataGridMembershipTracker.ItemsSource).Table;
                DBHelper.updateUserTable(data);
                this.Close();
            }
            
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void DataGridMemberShipTracker_ItemCreated(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            string headerName = e.Column.Header.ToString();
            if (headerName == "ID")
            {
                e.Cancel = true;
            }
        }
    }
}
