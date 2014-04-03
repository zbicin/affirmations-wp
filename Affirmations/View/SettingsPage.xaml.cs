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
        
        public SettingsPage()
        {
            InitializeComponent();

            DataContext = App.ViewModel;            
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
            int a = 5;
            a = 5 + 2;
        }
    }
}