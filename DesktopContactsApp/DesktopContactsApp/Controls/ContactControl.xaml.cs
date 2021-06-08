using DesktopContactsApp.Classes;
using System.Windows;
using System.Windows.Controls;

namespace DesktopContactsApp.Controls
{
    /// <summary>
    /// Interaction logic for ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {


        public Contact Contact
        {
            get { return (Contact)GetValue(ContactProperty); }
            set { SetValue(ContactProperty, value); }
        }

        public static readonly DependencyProperty ContactProperty =
            DependencyProperty.Register("Contact", typeof(Contact), typeof(ContactControl), new PropertyMetadata(new Contact() { }, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ContactControl;
            var contact = e.NewValue as Contact;

            if (contact != null)
            {
                control.nameTextBlock.Text = contact.Name;
                control.phoneNumberTextBlock.Text = contact.PhoneNumber;
                control.emailTextBlock.Text = contact.Email;
                control.createdDateTextBlock.Text = contact.CreatedDate.ToString();
                control.updatedDateTextBlock.Text = contact.UpdatedDate.ToString();
            }
        }

        public ContactControl()
        {
            InitializeComponent();
        }
    }
}
