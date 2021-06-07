using DesktopContactsApp.Classes;
using SQLite;
using System;
using System.IO;
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
            // Save the Contact

            var contact = new Contact()
            {
                Email = emailTextBox.Text,
                Name = nameTextBox.Text,
                PhoneNumber = phoneTextBox.Text,
                LastName = lastNameTextBox.Text
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