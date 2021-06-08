using DesktopContactsApp.Classes;
using SQLite;
using System;
using System.Windows;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        public NewContactWindow()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var contact = new Contact()
            {
                Email = emailTextBox.Text,
                Name = nameTextBox.Text,
                PhoneNumber = phoneTextBox.Text,
                CreatedDate = DateTime.Now
            };

            using (var connection = new SQLiteConnection(App.databaseFullPath))
            {
                connection.CreateTable<Contact>();
                connection.Insert(contact);
            }

            Close();
        }
    }
}