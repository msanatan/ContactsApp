using ContactsApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactsApp.Services
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }

    public class ContactsDataService
    {
        static object locker = new object();

        SQLiteConnection database;

        public ContactsDataService()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            database.CreateTable<Contact>();
        }

        public IEnumerable<Contact> GetContacts()
        {
            lock (locker)
            {
                return (from i in database.Table<Contact>() select i).ToList();
            }
        }

        public Contact GetContactById(int Id)
        {
            lock (locker)
            {
                return database.Table<Contact>().FirstOrDefault(n => n.Id == Id);
            }
        }

        public int SaveContact(Contact Contact)
        {
            lock (locker)
            {
                if (Contact.Id != 0)
                {
                    database.Update(Contact);
                    return Contact.Id;
                }
                else {
                    return database.Insert(Contact);
                }
            }
        }

        public int DeleteContact(int Id)
        {
            lock (locker)
            {
                
                return database.Delete<Contact>(Id);
            }
        }
    }

}