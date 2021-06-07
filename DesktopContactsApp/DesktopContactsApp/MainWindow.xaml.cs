using DesktopContactsApp.Classes;
using SQLite;
using System.Windows;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ReadDatabase();
        }

        private void ReadDatabase()
        {
            using (var conn = new SQLiteConnection(App.databaseFullPath))
            {
                conn.CreateTable<Contact>();
                var contacts = conn.Table<Contact>().ToList();
            }
        }

        private void newContactButton_Click(object sender, RoutedEventArgs e)
        {
            var newContactWindow = new NewContactWindow();

            newContactWindow.ShowDialog();

            ReadDatabase();
        }
    }
}