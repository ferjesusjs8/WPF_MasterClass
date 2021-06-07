using SQLite;
using System;

namespace DesktopContactsApp.Classes
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; private set; }

        public void Update(Contact contact)
        {
            contact.UpdatedDate = DateTime.Now;
        }
    }
}