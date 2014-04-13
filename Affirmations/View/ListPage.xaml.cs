using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Affirmations.Resources;
using Affirmations.Model;
using Affirmations.ViewModel;
using System.Collections.ObjectModel;

namespace Affirmations.View
{
    public partial class ListPage : PhoneApplicationPage
    {

        // Constructor
        public ListPage()
        {
            InitializeComponent();

            DataContext = App.ViewModel;

            UpdateListVisibility();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel spSender = (StackPanel)sender;
            Affirmation affirmation = (Affirmation)spSender.DataContext;
            int affirmationIndex = App.ViewModel.Affirmations.IndexOf(affirmation);
         
            NavigationService.Navigate(new Uri("/View/EditPage.xaml?affirmationIndex=" + affirmationIndex, UriKind.Relative));
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/AddPage.xaml", UriKind.Relative));
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/SettingsPage.xaml", UriKind.Relative));
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bardzo ładny about.\n\nOstatnio majstrowano przy aplikacji: 05.04.2014.");
        }

        private void htRepeat_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (App.ViewModel.Affirmations.Count > 0)
            {
                NavigationService.Navigate(new Uri("/View/RepeatPage.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Nie dodałeś jeszcze rzadnej afirmacji.");
            }
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/SettingsPage.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateListVisibility();
        }

        private void UpdateListVisibility()
        {
            // hide list page if there are no affirmations
            if (App.ViewModel.Affirmations.Count < 1)
            {
                pvPages.SelectedIndex = 0;
                pvPages.IsLocked = true;
            }
            else
            {
                pvPages.IsLocked = false;
            }
        }


        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}