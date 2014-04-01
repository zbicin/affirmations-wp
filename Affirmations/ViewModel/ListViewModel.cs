using Affirmations.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace Affirmations.ViewModel
{
    public class ListViewModel : BindableBase
    {
        public ObservableCollection<Affirmation> Affirmations { get; set; }

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

        public DateTime ReminderDateTime { get; set; }

        private IsolatedStorageSettings Settings { get; set; }

        public bool IsRepeatEnabled
        {
            get
            {
                return Affirmations.Count() > 0;
            }
            set
            {
                ;
            }
        }
        

        public ListViewModel()
        {
            Affirmations = Repository.Affirmations;
            Settings = IsolatedStorageSettings.ApplicationSettings;
            System.Diagnostics.Debug.WriteLine("ListViewMolde()");

            if (Settings.Contains(Repository.LAST_REPETITION_DATE_KEY))
            {
                LastRepetitionDate = (DateTime)Settings[Repository.LAST_REPETITION_DATE_KEY];
            }
            else
            {
                LastRepetitionDate = new DateTime(0);
            }

            if (Settings.Contains(Repository.REMINDER_DATETIME_KEY))
            {
                ReminderDateTime = (DateTime)Settings[Repository.REMINDER_DATETIME_KEY];
            }
            else
            {
                ReminderDateTime = new DateTime(0, 0, 0, 18, 0, 0);
            }
        }
    }
}
