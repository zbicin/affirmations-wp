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
    public partial class SettingsPage : PhoneApplicationPage
    {
        private SettingsViewModel viewModel;
        
        public SettingsPage()
        {
            InitializeComponent();

            viewModel = new SettingsViewModel();
            DataContext = viewModel;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            viewModel.Settings.Save();

            base.OnNavigatedFrom(e);
        }

        private void buttonDebugTime_Click(object sender, RoutedEventArgs e)
        {
            int a = 5;
            a = 5 + 2;
        }
    }
}