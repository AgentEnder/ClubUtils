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
/*
public class DataItem
{
    public string ClubName { get; set; }
    public string Date { get; set; }
    public string Name { get; set; }
    public bool Present { get; set; }
}
*/
namespace ClubUtils
{
    /// <summary>
    /// Interaction logic for AttendanceTracker.xaml
    /// </summary>
    
    public partial class AttendanceTracker : Window
    {
        MessageBoxResult result;
        public AttendanceTracker()
        {
            InitializeComponent();
            DataGridAttendanceTracker.CanUserAddRows = false;
            ButtonAttendanceTracker.Visibility = Visibility.Hidden;
            /*     DataGridTextColumn col1 = new DataGridTextColumn();
                 DataGridTextColumn col2 = new DataGridTextColumn();
                 DataGridTextColumn col3 = new DataGridTextColumn();
                 DataGridTextColumn col4 = new DataGridTextColumn();
                 DataGridAttendanceTracker.Columns.Add(col1);
                 DataGridAttendanceTracker.Columns.Add(col2);
                 DataGridAttendanceTracker.Columns.Add(col3);
                 DataGridAttendanceTracker.Columns.Add(col4);
                 col1.Binding = new Binding("ClubName");
                 col2.Binding = new Binding("Date");
                 col3.Binding = new Binding("Name");
                 col4.Binding = new Binding("Present");           
                 col1.Header = "ClubName";
                 col2.Header = "Date";
                 col3.Header = "Name";
                 col4.Header = "Present";
                 */
        }

        private void ButtonAttendanceTracker_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBox.Show("Are you sure you want to save? These changes will be permanent.", "Save?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                DataTable data = new DataTable();
                data = ((DataView)DataGridAttendanceTracker.ItemsSource).Table;
                DBHelper.appendAttendanceTable(data);
                this.Close();
            }
        }

        private void DataGridAttendanceTracker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DatePickerAttendanceTracker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string date = DatePickerAttendanceTracker.SelectedDate.Value.ToString("yyyy-MM-dd");
            DataGridAttendanceTracker.IsReadOnly = false;
            //bool holderBool;
            //string holderString;
            //int holderInt;
            DataTable dt = new DataTable();
            //DataTable attendanceData = new DataTable();
            //DataItem dataItem;
            //List<DataItem> dataItems = new List<DataItem>();
            dt = DBHelper.getAttendanceTable(date);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataGridAttendanceTracker.ItemsSource = dt.DefaultView;
                    ButtonAttendanceTracker.Visibility = Visibility.Hidden;
                    DataGridAttendanceTracker.IsReadOnly = true;
                    /*
                    holderString = row["Present"].ToString();
                    holderInt = Int32.Parse(holderString);
                    if (holderInt == 0)
                    {
                        holderBool = false;
                    } else
                    {
                        holderBool = true;
                    }
                    // DataGridAttendanceTracker.Items.Add(new DataItem { ClubName = row["ClubName"].ToString(), Date = row["Date"].ToString(), Name = row["FullName"].ToString(), Present = holderBool });
                    dataItem = new DataItem { ClubName = row["ClubName"].ToString(), Date = row["Date"].ToString(), Name = row["FullName"].ToString(), Present = holderBool };
                    dataItems.Add(dataItem);
                    DataGridAttendanceTracker.ItemsSource = dataItems;
                    */
                }

            }
            else
            {
                ButtonAttendanceTracker.Visibility = Visibility.Visible;
                DataTable data = new DataTable();
                data = DBHelper.getAttendanceList();
                data.Columns.Add("Date", typeof(String));
                data.Columns.Add("Present", typeof(int));
                DataGridAttendanceTracker.ItemsSource = data.DefaultView;
                foreach (DataRow row in data.Rows)
                {
                    row["Date"] = date;
                    row["Present"] = 0;
                }

            }

        }
    }
}
