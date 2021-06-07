using System;
using System.IO;
using System.Windows;

namespace DesktopContactsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static string database = "Contacts.db";
        static string databasePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string databaseFullPath = Path.Combine(databasePath, database);
    }
}