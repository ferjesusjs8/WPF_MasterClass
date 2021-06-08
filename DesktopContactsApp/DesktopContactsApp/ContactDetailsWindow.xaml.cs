using DesktopContactsApp.Classes;
using SQLite;
using System;
using System.Windows;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for ContactDetailsWindow.xaml
    /// </summary>
    public partial class ContactDetailsWindow : Window
    {
        Contact _contact;

        public ContactDetailsWindow(Contact contact)
        {
            InitializeComponent();

            _contact = contact;

            nameTextBox.Text = _contact.Name;
            phoneNumberTextBox.Text = _contact.PhoneNumber;
            emailTextBox.Text = _contact.Email;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            _contact.Name = nameTextBox.Text;
            _contact.PhoneNumber = phoneNumberTextBox.Text;
            _contact.Email = emailTextBox.Text;
            _contact.UpdatedDate = DateTime.Now;

            using (var conn = new SQLiteConnection(App.databaseFullPath))
            {
                conn.CreateTable<Contact>();
                conn.Update(_contact);
            }

            Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new SQLiteConnection(App.databaseFullPath))
            {
                conn.CreateTable<Contact>();
                conn.Delete(_contact);
            }

            Close();
        }
    }
}