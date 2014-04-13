using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Affirmations.Model;
using Affirmations.ViewModel;

namespace Affirmations
{
    public partial class AddPage : PhoneApplicationPage
    {

        public AddPage()
        {
            InitializeComponent();
            App.ViewModel.NewAffirmation = new Affirmation
            {
                Text = Affirmation.GetRandomText(),
                CreatedAt = DateTime.Now
            };
            DataContext = App.ViewModel;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            tbText.GetBindingExpression(PhoneTextBox.TextProperty).UpdateSource();

            if (App.ViewModel.NewAffirmation.Text.Length > 0)
            {
                App.ViewModel.Affirmations.Add(App.ViewModel.NewAffirmation);
                App.ViewModel.SaveAffirmations();

                if (App.ViewModel.IsReminderEnabled)
                {
                    // users wants reminders; make sure there will be one
                    App.ViewModel.ReminderHelper.TryScheduleReminder(App.ViewModel.LastRepetitionDate);
                }

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

        private void PhoneApplicationPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
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