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
        private string AFFIRMATIONS_KEY = "affirmations";

        private IsolatedStorageSettings Settings;
        //private ScheduledRemindersHelper ReminderHelper;

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
                if (value)
                {
                   // RemindersHelper.ScheduleReminder(LastRepetitionDate);
                }
                else
                {
                    //ReminderHelper.UnscheduleReminder();
                }
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
            }
        }

        public MainViewModel()
        {
            Settings = IsolatedStorageSettings.ApplicationSettings;
            Affirmations = new ObservableCollection<Affirmation>();
            LoadSettings();
        }

        public void SaveSettings()
        {
            SaveSingleSetting<DateTime>(LAST_REPETITION_DATE_KEY, LastRepetitionDate);
            SaveSingleSetting<DateTime>(REMINDER_DATETIME_KEY, ReminderDateTime);
            SaveSingleSetting<bool>(IS_REMINDER_ENABLED_KEY, IsReminderEnabled);

            //affirmations
            XmlSerializer xmlSerializer = new XmlSerializer(Affirmations.GetType());
            StringWriter stringWriter = new StringWriter();

            xmlSerializer.Serialize(stringWriter, Affirmations);
            SaveSingleSetting<string>(AFFIRMATIONS_KEY, stringWriter.ToString());
        }

        private void SaveSingleSetting<T>(string key, T value)
        {
            if (Settings.Contains(key))
            {
                Settings[key] = value;
            }
            else
            {
                Settings.Add(key, value);
            }
        }

        public void LoadSettings()
        {
            IsReminderEnabled = GetSingleSettingOrDefault<bool>(IS_REMINDER_ENABLED_KEY, false);
            ReminderDateTime = GetSingleSettingOrDefault<DateTime>(REMINDER_DATETIME_KEY, new DateTime(1900, 1, 1, 18, 0, 0));
            LastRepetitionDate = GetSingleSettingOrDefault<DateTime>(LAST_REPETITION_DATE_KEY, new DateTime(0));

            //affirmations
            string serializedAffirmations = GetSingleSettingOrDefault<string>(AFFIRMATIONS_KEY, String.Empty);
            StringReader stringReader = new StringReader(serializedAffirmations);
            XmlSerializer xmlSerializer = new XmlSerializer(Affirmations.GetType());

            Affirmations = (ObservableCollection<Affirmation>)xmlSerializer.Deserialize(stringReader);
        }

        private T GetSingleSettingOrDefault<T>(string key, T defaultValue)
        {
            if (Settings.Contains(key))
            {
                return (T)Settings[key];
            }
            return defaultValue;
        }


    }
}
