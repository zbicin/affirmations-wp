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

namespace Affirmations.View
{
    public partial class RepeatPage : PhoneApplicationPage
    {
        private RepeatViewModel viewModel;
        public RepeatPage()
        {
            InitializeComponent();

            viewModel = new RepeatViewModel();
            DataContext = viewModel;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                NavigationService.Navigate(new Uri("/View/ListPage.xaml", UriKind.Relative));
            }
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pivot pivot = (Pivot)sender;

            if(pivot.SelectedIndex == pivot.Items.Count-1) {
                viewModel.BarMode = ApplicationBarMode.Default;
            }
            else
            {
                viewModel.BarMode = ApplicationBarMode.Minimized;
            }
        }
    }
}