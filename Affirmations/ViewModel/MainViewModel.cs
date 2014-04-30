using Affirmations.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Affirmations.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private string IS_REMINDER_ENABLED_KEY = "isReminderEnabled";
        private string LAST_REPETITION_DATE_KEY = "lastRepetitionDate";
        private string REMINDER_DATETIME_KEY = "reminderDateTime";

        private StorageHelper Storage;
        public ScheduledRemindersHelper ReminderHelper;

        public ObservableCollection<Affirmation> Affirmations { get; set; }

        private DateTime _reminderDateTime;
        public DateTime ReminderDateTime
        {
            get
            {
                return _reminderDateTime;
            }
            set
            {
                this.SetProperty<DateTime>(ref _reminderDateTime, value);
            }
        }

        public bool IsFirstRepetitionDone
        {
            get
            {
                return LastRepetitionDate.CompareTo(new DateTime(0)) != 0;
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
                this.SetProperty<bool>(ref _isReminderEnabled, value);
            }
        }

        private Affirmation _newAffirmation;
        public Affirmation NewAffirmation
        {
            get
            {
                return _newAffirmation;
            }
            set
            {
                SetProperty<Affirmation>(ref _newAffirmation, value);
            }
        }

        private Affirmation _editedAffirmation;
        public Affirmation EditedAffirmation
        {
            get
            {
                return _editedAffirmation;
            }
            set
            {
                SetProperty<Affirmation>(ref _editedAffirmation, value);
            }
        }

        private DateTime _lastRepetitionDate;
        public DateTime LastRepetitionDate
        {
            get
            {
                return _lastRepetitionDate;
            }
            set
            {
                SetProperty<DateTime>(ref _lastRepetitionDate, value);
                OnPropertyChanged("IsFirstRepetitionDone");
            }
        }

        public MainViewModel()
        {
            Storage = new StorageHelper();
            ReminderHelper = new ScheduledRemindersHelper();

            Affirmations = new ObservableCollection<Affirmation>(Storage.Affirmations);
            LoadSettings();
        }

        public void SaveSettings()
        {
            Storage.SetSetting(LAST_REPETITION_DATE_KEY, LastRepetitionDate);
            Storage.SetSetting(REMINDER_DATETIME_KEY, ReminderDateTime);
            Storage.SetSetting(IS_REMINDER_ENABLED_KEY, IsReminderEnabled);

            Storage.SaveSettings();
        }

        /*private void SaveSingleSetting<T>(string key, T value)
        {
            if (Settings.ContainsSetting(key))
            {
                Settings.SetSetting(key, value);
            }
            else
            {
                Settings.Add(key, value);
            }
        }*/

        public void LoadSettings()
        {
            IsReminderEnabled = Storage.TryGetSetting<bool>(IS_REMINDER_ENABLED_KEY, false);
            ReminderDateTime = Storage.TryGetSetting<DateTime>(REMINDER_DATETIME_KEY, new DateTime(1900, 1, 1, 18, 0, 0));
            LastRepetitionDate = Storage.TryGetSetting<DateTime>(LAST_REPETITION_DATE_KEY, new DateTime(0));
        }

        /*private T GetSingleSettingOrDefault<T>(string key, T defaultValue)
        {
            if (Settings.ContainsSetting(key))
            {
                return Settings.Get<T>(key);
            }
            return defaultValue;
        }*/



        public void SaveAffirmations()
        {
            Storage.Affirmations = Affirmations.ToList();
            Storage.SaveAffirmations();
        }

        public void ResetAll()
        {
            Storage.ResetAll();
        }

        public string ShowSettings()
        {
            return Storage.ToString() + "\nPora następnego przypomnienia: " + ReminderHelper.GetBeginDate(LastRepetitionDate).ToString() + "\n\nLiczba afirmacji: " + Storage.Affirmations.Count;
        }
    }
}
