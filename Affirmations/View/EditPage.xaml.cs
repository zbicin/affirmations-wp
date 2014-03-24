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
        private EditViewModel viewModel;

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
                viewModel = new EditViewModel(affirmationIndex);
                DataContext = viewModel;
            }
			
			tbText.Focus();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Repository.Affirmations[Convert.ToInt16(affirmationIndex)].Text = tbText.Text;
            Repository.SaveChanges();

            try
            {
                NavigationService.GoBack();
            }
            catch (InvalidOperationException exception)
            {
                NavigationService.Navigate(new Uri("/View/ListPage.xaml", UriKind.Relative));
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

    }
}