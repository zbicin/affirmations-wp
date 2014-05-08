using Affirmations.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affirmations.Model
{
    public class Affirmation
    {
        [Newtonsoft.Json.JsonIgnore]
        private static List<string> PredefinedTexts = new List<string>
        {
            AppResources.ExampleAffirmation1,
            AppResources.ExampleAffirmation2,
            AppResources.ExampleAffirmation3,
            AppResources.ExampleAffirmation4,
            AppResources.ExampleAffirmation5,
            AppResources.ExampleAffirmation6,
            AppResources.ExampleAffirmation7
        };

        [Newtonsoft.Json.JsonIgnore]
        private static Random random = new Random();

        public String Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public Affirmation Clone()
        {
            return new Affirmation
            {
                Text = this.Text,
                CreatedAt = this.CreatedAt
            };
        }

        public static string GetRandomText()
        {
            return PredefinedTexts[random.Next(0, PredefinedTexts.Count)];
        }
    }
}
