using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ClubUtils
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<DateTime, List<Event>> eventLookup = new Dictionary<DateTime, List<Event>>();
        public MainWindow(Member member)
        {
            InitializeComponent();
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
                TextBlock body = new TextBlock();
                body.Text = bulletin.body;
                body.TextWrapping = TextWrapping.Wrap;
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
    }

    

}
