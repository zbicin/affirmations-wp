using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Affirmations.ViewModel;
using Affirmations.Model;

namespace Affirmations.View
{
    public partial class EditPage : PhoneApplicationPage
    {
        private string affirmationIndex;

        public EditPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            affirmationIndex = "-1";

            if (NavigationContext.QueryString.TryGetValue("affirmationIndex", out affirmationIndex))
            {
                App.ViewModel.EditedAffirmation = App.ViewModel.Affirmations.ElementAt<Affirmation>(Convert.ToInt16(affirmationIndex)).Clone(); 
                DataContext = App.ViewModel;
            }
			
			tbText.Focus();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            tbText.GetBindingExpression(PhoneTextBox.TextProperty).UpdateSource();

            if (App.ViewModel.EditedAffirmation.Text.Length > 0)
            {
                App.ViewModel.Affirmations[Convert.ToInt16(affirmationIndex)] = App.ViewModel.EditedAffirmation.Clone();

                try
                {
                    NavigationService.GoBack();
                }
                catch (InvalidOperationException exception)
                {
                    NavigationService.Navigate(new Uri("/View/ListPage.xaml", UriKind.Relative));
                }
            }
            else
            {
                MessageBox.Show("Treść afirmacji nie może być pusta.");
            }

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ta afirmacja zostanie usunięta z Twojej listy.", "Afirmacje", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                App.ViewModel.Affirmations.RemoveAt(Convert.ToInt16(affirmationIndex));

                try
                {
                    NavigationService.GoBack();
                }
                catch (InvalidOperationException exception)
                {
                    NavigationService.Navigate(new Uri("/View/ListPage.xaml", UriKind.Relative));
                }
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            tbText.SelectionStart = tbText.Text.Length;
			tbText.Focus();
        }

        private void tbText_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Focus(); // focus the page to hide the keypad
            }
        }

        private void tbText_GotFocus(object sender, RoutedEventArgs e)
        {
            tbText.SelectAll();
        }

    }
}