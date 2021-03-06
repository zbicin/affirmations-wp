﻿using System;
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
        
        public SettingsPage()
        {
            InitializeComponent();

            DataContext = App.ViewModel;   
         
#if DEBUG
            spDebug.Visibility = System.Windows.Visibility.Visible;
#endif
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.ViewModel.SaveSettings();

            if (e.Content == null
                || e.Content.GetType() != typeof(TimePickerPage))
            {
                if (App.ViewModel.IsReminderEnabled
                    && App.ViewModel.Affirmations.Count > 0)
                {
                    App.ViewModel.ReminderHelper.TryScheduleReminder(App.ViewModel.LastRepetitionDate);
                }
                else
                {
                    App.ViewModel.ReminderHelper.TryUnscheduleReminder();
                }
            }


            base.OnNavigatedFrom(e);
        }

        private void buttonDebugTime_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Wiesz co robisz?", "Chwilkę Panie Kierowniku", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                App.ViewModel.ResetAll();
                App.ViewModel = new MainViewModel();
                MessageBox.Show("Zrobione. Uruchom ponownie aplikację.");
                new Application().Terminate();
            }
        }

        private void buttonShowSettings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(App.ViewModel.ShowSettings());
        }
    }
}