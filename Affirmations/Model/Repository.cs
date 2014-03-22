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
        private static string AFFIRMATIONS_KEY = "affirmations";
        private static bool IsInitiated = false;

        public static List<Affirmation> Affirmations = new List<Affirmation>
        {
            new Affirmation {
                Text = "Jesteś zwycięzcą!",
                CreatedAt = new DateTime(2014,3,19)
            },
            new Affirmation {
                Text = "Lorem lipsum bardzo długa nazwa yeeeeaa jeszcze jeszcze dłuższa",
                CreatedAt = new DateTime(2014,2,10)
            }
        };

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
                Affirmations = new List<Affirmation>();

                if (settings.Contains(AFFIRMATIONS_KEY))
                {
                    string serializedAffirmations = "";
                    settings.TryGetValue<string>(AFFIRMATIONS_KEY, out serializedAffirmations);

                    StringReader stringReader = new StringReader(serializedAffirmations);
                    XmlSerializer xmlSerializer = new XmlSerializer(Affirmations.GetType());

                    Affirmations = (List<Affirmation>)xmlSerializer.Deserialize(stringReader);
                }

                IsInitiated = true;
            }
        }

    }
}
