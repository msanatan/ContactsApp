using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ContactsApp.Services;
using SQLite;
using System.IO;
using Xamarin.Forms;
using ContactsApp.Droid.Implementation;

[assembly: Dependency(typeof(DatabaseImplementation_Android))]
namespace ContactsApp.Droid.Implementation
{
    public class DatabaseImplementation_Android : ISQLite
    {
        public DatabaseImplementation_Android()
        {
        }

    
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "Contacts.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(path);
            return conn;

        }
    }
}