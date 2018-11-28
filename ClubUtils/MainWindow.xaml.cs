using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClubUtils
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow instance;
        private Dictionary<DateTime, List<Event>> eventLookup = new Dictionary<DateTime, List<Event>>();
        public MainWindow(Member member)
        {
            instance = this;
            InitializeComponent();
            if (Globals.currentMember.rank > Member.ranks.USER)
            {
                AdminToolBar.Visibility = Visibility.Visible;
                NewBulletinBtn.Visibility = Visibility.Visible;
            }
            if (Globals.currentMember.rank >= Member.ranks.VICE_PRESIDENT)
            {
                NewEventBtn.Visibility = Visibility.Visible;
            }
            if (Globals.currentMember.rank < Member.ranks.TREASURER)
            {
                Financials.Visibility = Visibility.Collapsed;
            }
            /////Calendar Updates
            #region Calendar Updates
            List<Event> dates = DBHelper.getEventsFromClub(Globals.currentMember.clubName);
            foreach (Event item in dates) //LOOP THROUGH EVENTS
            {
                Console.WriteLine("ADDING EVENT: " + item.name + " AT TIME: " + item.time.ToShortDateString());
                if (item.recurs) //EVENT RECURS 
                {
                    DateTime EndAt = DateTime.Now.AddYears(1); //Force end for displayed recurrances
                    DateTime curr = item.time;
                    while (curr < EndAt && curr <= item.endTime)
                    {
                        myCalendar.BlackoutDates.Remove(new CalendarDateRange(curr));
                        if (!eventLookup.ContainsKey(curr.Date))

                        {
                            eventLookup[curr.Date] = new List<Event>();
                        }
                        eventLookup[curr.Date].Add(item);
                        curr = curr.AddDays(7);
                    }
                }
                else
                {
                    myCalendar.BlackoutDates.Remove(new CalendarDateRange(item.time));
                    if (!eventLookup.ContainsKey(item.time))
                    {
                        eventLookup[item.time.Date] = new List<Event>();
                    }
                    eventLookup[item.time.Date].Add(item);
                }
            }
            DateTime currMin = DateTime.MinValue;
            List<DateTime> nonBlackouts = eventLookup.Keys.ToList();
            nonBlackouts.Sort();
            foreach (DateTime item in nonBlackouts)
            {
                myCalendar.BlackoutDates.Add(new CalendarDateRange(currMin, item.AddDays(-1)));
                currMin = item.AddDays(1);
            }
            myCalendar.BlackoutDates.Add(new CalendarDateRange(currMin, DateTime.MaxValue));
            #endregion
            #region Bulletin Updates
            List<Bulletin> bulletins = DBHelper.getBulletinsFromClub(Globals.currentMember.clubName);
            bulletins.Sort((y, x) => x.time.CompareTo(y.time)); //Descending Sort
            foreach (var bulletin in bulletins)
            {
                Label heading = new Label();
                heading.Content = bulletin.heading;
                Label timestamp = new Label();
                timestamp.Content = bulletin.time.ToShortDateString();
                RichTextBox body = new RichTextBox();
                try
                {
                    MemoryStream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(bulletin.body));
                    body.Selection.Load(stream, DataFormats.Rtf);
                }
                catch
                {
                    body.AppendText("Bulletin failed to load!");
                }
                body.IsReadOnly = true;
                body.BorderThickness = new Thickness(0);
                body.Background = Brushes.GhostWhite;
                DockPanel temp = new DockPanel();
                DockPanel.SetDock(timestamp, Dock.Left);
                temp.Children.Add(timestamp);
                StackPanel contentPanel = new StackPanel();
                contentPanel.Children.Add(heading);
                contentPanel.Children.Add(body);
                DockPanel.SetDock(contentPanel, Dock.Right);
                temp.Children.Add(contentPanel);
                BulletinPanel.Children.Add(temp);
            }
            #endregion 
        }

        private void AdminMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.Source as MenuItem;

            switch (mi.Name)
            {
                case "Attendance":
                    {
                        AttendanceTracker temp = new AttendanceTracker();
                        temp.Show();
                        break;
                    }

                case "Membership":
                    {
                        MembershipTracker temp = new MembershipTracker();
                        temp.Show();
                        break;
                    }

                case "Financials":
                    {
                        FinancialTracker temp = new FinancialTracker();
                        temp.Show();
                        break;
                    }
                case "Emails":
                    {
                        MassEmail temp = new MassEmail();
                        temp.Show();
                        break;
                    }
            }
        }

        private void myCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myCalendar.SelectedDate == null)
            {
                return;
            }
            EventContainer.Children.Clear();
            foreach (Event item in eventLookup[(DateTime)myCalendar.SelectedDate])
            {
                Label temp = new Label();
                HeadingLabel.Content = "Events on " + ((DateTime)myCalendar.SelectedDate).ToShortDateString();
                temp.Content = item.name;
                EventContainer.Children.Add(temp);
            }
        }

        private void FileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.Source as MenuItem;

            switch (mi.Name)
            {
                case "LogOut":
                    {
                        Globals.currentMember = null;
                        Login temp = new Login();
                        temp.Show();
                        this.Close();
                    }
                    break;
                case "Exit":
                    {
                        this.Close();
                    }
                    break;
            }
        }

        private void NewEventBtn_Click(object sender, RoutedEventArgs e)
        {
            NewEvent tempWindow = new NewEvent();
            tempWindow.Show();
        }

        private void NewBulletinBtn_Click(object sender, RoutedEventArgs e)
        {
            NewBulletin tempWindow = new NewBulletin();
            tempWindow.Show();
        }
    }
}
