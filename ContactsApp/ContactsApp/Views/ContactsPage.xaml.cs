using ContactsApp.Models;
using ContactsApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ContactsApp.Views
{
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        private ContactsDataService db = new ContactsDataService();

        private IEnumerable<Contact> _contacts;

        public IEnumerable<Contact> Contacts
        {
            get { return _contacts; }
            set
            {
                if (_contacts == value)
                {
                    return;
                }
                _contacts = value;

                OnPropertyChanged();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Contacts = db.GetContacts();
        }


        public void OnAdd(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.ContactDetailsPage(new Contact()));

        }

        public async void OnSelect(object sender, SelectedItemChangedEventArgs e)
        {
            Contact model = (Contact)e.SelectedItem;
            await Navigation.PushAsync(new Views.ContactDetailsPage(model));
        }


    }
}
