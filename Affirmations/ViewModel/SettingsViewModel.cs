using Affirmations.Model;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Affirmations.ViewModel
{
    public class SettingsViewModel : BindableBase
    {
        public IsolatedStorageSettings Settings {get; set;}

        private DateTime _reminderDateTime;
        public DateTime ReminderDateTime
        {
            get
            {
                return _reminderDateTime;
            }
            set
            {
                this.SetProperty<DateTime>(Repository.REMINDER_DATETIME_KEY, ref _reminderDateTime, value);
            }
        }

        private bool _isReminderEnabled;
        public bool IsReminderEnabled
        {
            get
            {
                return _isReminderEnabled;
            }
            set
            {
                this.SetProperty<bool>(Repository.IS_REMINDER_ENABLED_KEY, ref _isReminderEnabled, value);
            }
        }

        public SettingsViewModel()
        {
            Settings = IsolatedStorageSettings.ApplicationSettings;

            if (Settings.Contains(Repository.IS_REMINDER_ENABLED_KEY))
            {
                IsReminderEnabled = (bool)Settings[Repository.IS_REMINDER_ENABLED_KEY];
            }
            else
            {
                Settings[Repository.IS_REMINDER_ENABLED_KEY] = IsReminderEnabled = false;
            }

            if (Settings.Contains(Repository.REMINDER_DATETIME_KEY))
            {
                ReminderDateTime = (DateTime)Settings[Repository.REMINDER_DATETIME_KEY];
            }
            else
            {
                DateTime tommorow = DateTime.Now.AddDays(1);
                Settings[Repository.REMINDER_DATETIME_KEY] = ReminderDateTime = new DateTime(tommorow.Year, tommorow.Month, tommorow.Day, 18, 0, 0);
            }
        }

        protected bool SetProperty<T>(string settingsKey, ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            bool baseResult = base.SetProperty<T>(ref storage, value, propertyName);

            if (baseResult)
            {
                Settings[settingsKey] = value;
            }

            return baseResult;
        }
    }
}
