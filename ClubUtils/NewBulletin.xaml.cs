using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for NewBulletin.xaml
    /// </summary>
    public partial class NewBulletin : Window
    {
        public NewBulletin()
        {
            InitializeComponent();
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            Stack<Control> invalidControls = new Stack<Control>();
            if (Heading.Text.Length <= 0) { invalidControls.Push(Heading); }
            if (BulletinText.Document.ContentEnd == BulletinText.Document.ContentStart) { invalidControls.Push(BulletinText); }
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
            MemoryStream SerializedBulletin = new MemoryStream();
            TextRange textRange = new TextRange(BulletinText.Document.ContentStart, BulletinText.Document.ContentEnd);
            textRange.Save(SerializedBulletin, DataFormats.Rtf);
            SerializedBulletin.Seek(0, SeekOrigin.Begin);
            StreamReader stream = new StreamReader(SerializedBulletin);
            string SerializedBulletinText = stream.ReadToEnd();
            Console.WriteLine();
            string values = "(null, '";
            values += Heading.Text + "','";
            values += DateTime.Now.ToString("yyyy-MM-dd") + "','";
            values += SerializedBulletinText + "','";
            values += Globals.currentMember.clubName + "')";
            string query = "INSERT INTO `Bulletins`(`ID`,`Heading`,`TimeStamp`,`Text`,`ClubName`) VALUES " + values;
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
