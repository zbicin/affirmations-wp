using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affirmations.Model
{
    public class StorageHelper
    {
        private const string AFFIRMATIONS_FILENAME = "affirmations.json";
        private const string SETTINGS_FILENAME = "settings.json";
        private const string STORAGE_FOLDERNAME = "Affirmations";

        private Dictionary<string, object> Settings;
        private JsonSerializerSettings SerializerSettings;
        public List<Affirmation> Affirmations;

        public StorageHelper()
        {
            LoadSettings();
            LoadAffirmations();

            SerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            };
        }

        public void SetSetting(string key, object value)
        {
            Settings[key] = value;
        }

        public T TryGetSetting<T>(string key, T defaultValue)
        {
            return Settings.ContainsKey(key) ? (T)Settings[key] : defaultValue;
        }

        private void WriteToFile(string filename, string contents)
        {
            // Get the local folder.
            IsolatedStorageFile local =
                IsolatedStorageFile.GetUserStoreForApplication();

            // Create a new folder named DataFolder.
            if (!local.DirectoryExists(STORAGE_FOLDERNAME))
            {
                local.CreateDirectory(STORAGE_FOLDERNAME);
            }

            // Create a new file named DataFile.txt.
            using (var isoFileStream =
                    new IsolatedStorageFileStream(
                        STORAGE_FOLDERNAME + "\\" + filename,
                        System.IO.FileMode.Create,
                            local))
            {
                // Write the data from the textbox.
                using (var isoFileWriter = new System.IO.StreamWriter(isoFileStream))
                {
                    isoFileWriter.Write(contents);
                }
            }
        }

        private string ReadFromFile(string filename)
        {
            IsolatedStorageFile local = IsolatedStorageFile.GetUserStoreForApplication();

            if (!local.FileExists(STORAGE_FOLDERNAME + "\\" + filename))
            {
                return null;
            }

            // Specify the file path and options.
            using (var isoFileStream =
                    new IsolatedStorageFileStream(STORAGE_FOLDERNAME+"\\"+filename, FileMode.Open, local))
            {
                // Read the data.
                using (var isoFileReader = new System.IO.StreamReader(isoFileStream))
                {
                    return isoFileReader.ReadToEnd();
                }
            }
        }

        public void SaveSettings()
        {
            WriteToFile(SETTINGS_FILENAME, JsonConvert.SerializeObject(Settings, SerializerSettings));
        }

        public void SaveAffirmations()
        {
            WriteToFile(AFFIRMATIONS_FILENAME, JsonConvert.SerializeObject(Affirmations, SerializerSettings));
        }

        private void LoadSettings()
        {
            string serializedSettings = ReadFromFile(SETTINGS_FILENAME);

            if (serializedSettings != null)
            {
                Settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(serializedSettings, SerializerSettings);
            }
            else
            {
                Settings = new Dictionary<string, object>();
            }
        }

        private void LoadAffirmations()
        {
            string serializedAffirmations = ReadFromFile(AFFIRMATIONS_FILENAME);

            if(serializedAffirmations != null) {
                Affirmations = JsonConvert.DeserializeObject<List<Affirmation>>(serializedAffirmations);
            }
            else {
                Affirmations = new List<Affirmation>();
            }
        }
    }
}
