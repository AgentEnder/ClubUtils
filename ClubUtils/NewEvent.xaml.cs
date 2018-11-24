using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClubUtils
{
    /// <summary>
    /// Interaction logic for NewEvent.xaml
    /// </summary>
    public partial class NewEvent : Window
    {
        public NewEvent()
        {
            InitializeComponent();
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            Stack<Control> invalidControls = new Stack<Control>();
            if (EventName.Text.Length <= 0) { invalidControls.Push(EventName); }
            if (StartDatePicker.SelectedDate == null) { invalidControls.Push(StartDatePicker); }
            if ((bool)RecurringCheck.IsChecked && EndDatePicker.SelectedDate == null) { invalidControls.Push(EndDatePicker); }
            if (invalidControls.Count > 0)
            {
                Control temp;
                while (invalidControls.Count > 0 && (temp = invalidControls.Pop()) != null)
                {
                    temp.BorderBrush = Brushes.DarkRed;
                    temp.BorderThickness = new Thickness(2.0);
                }
                return;
            }
            
            string values = "(null, '";
            values += EventName.Text + "','";
            values += StartDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd") + "','";
            values = values + ((bool)RecurringCheck.IsChecked ? EndDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd") : StartDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd")) + "','";
            values += Globals.currentMember.clubName + "','";
            values += ((bool)RecurringCheck.IsChecked ? "1" : "0") + "')";
            string query = "INSERT INTO `Events`(`ID`,`EventName`,`EventTime`,`StopTime`,`ClubName`,`Recurring`) VALUES " + values;
            try
            {
                DBHelper.ExecuteNonQuery(query);
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
            MainWindow.instance.Close();
            (new MainWindow(Globals.currentMember)).Show();
            this.Close();
        }
    }
}
