using DesktopContactsApp.Classes;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;
        public MainWindow()
        {
            InitializeComponent();

            contacts = new List<Contact>();

            ReadDatabase();
        }

        private void ReadDatabase()
        {
            using (var conn = new SQLiteConnection(App.databaseFullPath))
            {
                conn.CreateTable<Contact>();
                contacts = conn.Table<Contact>().ToList();
            }

            if (contacts.Any())
                contactsListView.ItemsSource = contacts;
        }

        private void newContactButton_Click(object sender, RoutedEventArgs e)
        {
            var newContactWindow = new NewContactWindow();

            newContactWindow.ShowDialog();

            ReadDatabase();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            contactsListView.ItemsSource = contacts.Where(x => x.Name.Contains(textBox.Text, System.StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        private void contactsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedContact = contactsListView.SelectedItem as Contact;
            var contactDetailsWindow = new ContactDetailsWindow(selectedContact);

            contactDetailsWindow.ShowDialog();

            ReadDatabase();
        }
    }
}