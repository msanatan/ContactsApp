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
    public partial class ContactDetailsPage : ContentPage
    {
        private ContactsDataService db = new ContactsDataService();
     
        public Contact Model
        {
            get { return (Contact)BindingContext; }
            set
            {
                BindingContext = value;
                OnPropertyChanged();
            }
        }

        public ContactDetailsPage(Contact model)
        {

            Model = model;

            if (model.Name != null)
            {
                Title = Model.Name;
                
            }
            else
            {
                Title = "New Contact";
            }

            InitializeComponent();
        }

        void OnSave(object sender, EventArgs e)
        {
            
            if (!IsFormValid())
            {
                return;
            }

            Model.PhotoUrl = "ContactsApp.Images.anonymous.png";
            db.SaveContact(Model);

			Navigation.PopAsync ();
        }

        public async void OnDelete(object sender, EventArgs e)
        {

            var answer = await DisplayAlert("Are You Sure?", Model.Name + " will be deleted.", "Delete", "No");

            if (answer)
            {
                db.DeleteContact(Model.Id);
				await Navigation.PopAsync ();
            }
        }

        public async void OnCancel(object sender, EventArgs e)
        {
			await Navigation.PopAsync ();
        }

        public bool IsFormValid()
        {
            if (string.IsNullOrEmpty(Model.Name))
            {
                DisplayAlert("Validation Error", "'Name' is empty.", "Ok");
                return false;
            }

            return true;
        }

    }
}
