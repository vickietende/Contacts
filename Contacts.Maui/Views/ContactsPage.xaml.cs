using Contacts.Maui.Models;
using System.Collections.ObjectModel;
using Contact = Contacts.Maui.Models.Contact;
namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
	public ContactsPage()
	{
		InitializeComponent();
      
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Searchbar.Text = string.Empty;  
        LoadContacts();
     

    }



    private async void listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		
		if(listContacts.SelectedItem!=null) {

            //Logic
            await Shell.Current.GoToAsync($"{nameof(EditContactPage)}?Id={((Contact)listContacts.SelectedItem).ContactId}");
            
        }

    }

    private void listContacts_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listContacts.SelectedItem = null;

    }

    private void btnAdd_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddContactsPage));

    }

    private void MenuItemDelete_Clicked(object sender, EventArgs e)
    {
        var menuItem= sender as MenuItem;
        var contact=menuItem.CommandParameter as Contact;
        ContactRepository.DeleteContact(contact.ContactId);
        LoadContacts();

    }
    private void LoadContacts()
    {
        var contacts = new ObservableCollection<Contact>(ContactRepository.GetContacts());

        listContacts.ItemsSource = contacts;

    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
       var contacts= new ObservableCollection<Contact>(ContactRepository.SearchContact(((SearchBar)sender).Text));
       listContacts.ItemsSource = contacts;
    }

}