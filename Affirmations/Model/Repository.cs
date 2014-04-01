using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Affirmations.Model
{
    public static class Repository
    {
        public static string IS_REMINDER_ENABLED_KEY = "isReminderEnabled";
        public static string LAST_REPETITION_DATE_KEY = "lastRepetitionDate";
        public static string REMINDER_DATETIME_KEY = "reminderDateTime";
        private static string AFFIRMATIONS_KEY = "affirmations";
        private static bool IsInitiated = false;

        public static ObservableCollection<Affirmation> Affirmations { get; set; }

        public static void SaveChanges()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            
            XmlSerializer xmlSerializer = new XmlSerializer(Affirmations.GetType());
            StringWriter stringWriter = new StringWriter();

            xmlSerializer.Serialize(stringWriter, Affirmations);

            if (settings.Contains(AFFIRMATIONS_KEY))
            {
                settings[AFFIRMATIONS_KEY] = stringWriter.ToString();
            }
            else
            {
                settings.Add(AFFIRMATIONS_KEY, stringWriter.ToString());
            }

            settings.Save();
        }

        public static void Init()
        {
            if (!IsInitiated)
            {
                var settings = IsolatedStorageSettings.ApplicationSettings;
                Affirmations = new ObservableCollection<Affirmation>();

                if (settings.Contains(AFFIRMATIONS_KEY))
                {
                    string serializedAffirmations = "";
                    settings.TryGetValue<string>(AFFIRMATIONS_KEY, out serializedAffirmations);

                    StringReader stringReader = new StringReader(serializedAffirmations);
                    XmlSerializer xmlSerializer = new XmlSerializer(Affirmations.GetType());

                    Affirmations = (ObservableCollection<Affirmation>)xmlSerializer.Deserialize(stringReader);
                }

                IsInitiated = true;
            }
        }

    }
}
