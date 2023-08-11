//using Android.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Maui.Models
{
    public class ContactRepository
    {
        public static List<Contact> _contacts = new List<Contact>() {
            new Contact{ContactId=1,Name="John Doe", Email="john@example.com"},
            new Contact{ContactId=2, Name="Jane Doe", Email="jane@example.com"},
            new Contact{ContactId=3, Name="Mariah Carey", Email="mariah@example.com"},
            new Contact{ContactId=4, Name="Regan Kay", Email="regan@example.com"},
            new Contact{ContactId=5, Name="Terry Smith", Email="terry@example.com"}
        };

        public static List<Contact> GetContacts() => _contacts;
        public static Contact GetContactbyId(int contactId)
        {
            var contact= _contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (contact != null)
            {
                return new Contact
                {
                    ContactId = contactId,
                    Name = contact.Name,
                    Phone= contact.Phone,
                    Email= contact.Email,
                    Address= contact.Address,

                };
            }
            return null;

        }

        public static void UpdateContact(int contactId, Contact contact)
        {
            if (contactId != contact.ContactId) return;

            var contactToUpdate = _contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (contactToUpdate != null)
            {
                // To learn AutoMapper for doing this
                contactToUpdate.Address = contact.Address;
                contactToUpdate.Phone = contact.Phone;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Name = contact.Name;
            }

        }
        public static void AddContact(Contact contact)
        {
            var maxId=_contacts.Max(x => x.ContactId);
            contact.ContactId = maxId + 1;
            _contacts.Add(contact);
        }

        public static void DeleteContact(int contactId)
        {
            var contact= _contacts.FirstOrDefault(x=>x.ContactId==contactId);
            if(contact != null)
            {
                _contacts.Remove(contact);
            }
        }
        public static List<Contact> SearchContact(string filterText)
        {
           var contacts= _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Email) && x.Email.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            else
                return contacts;

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Phone) && x.Phone.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            else
                return contacts;

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Address) && x.Address.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            else
                return contacts;

            return contacts;



        }

    }
}
