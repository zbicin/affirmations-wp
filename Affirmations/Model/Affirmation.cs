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
            "Mogę dokonać czegokolwiek zapragnę.",
            "Tylko ode mnie zależy to, jaki wpływ mają na moje zachowanie inni.",
            "To ja decyduję jak wygląda moje życie.",
            "Mój czas jest tu i teraz.",
            "Każdy dzień jest wspaniałą okazją do przeżycia czegoś niesamowitego.",
            "Swoje marzenia traktuję jak plany.",
            "\"Porażki\" to tylko informacja zwrotna."
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
