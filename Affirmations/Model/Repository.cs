using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affirmations.Model
{
    public static class Repository
    {
        public static List<Affirmation> Affirmations = new List<Affirmation>
        {
            new Affirmation {
                Text = "Jesteś zwycięzcą!",
                CreatedAt = new DateTime(2014,3,19)
            },
            new Affirmation {
                Text = "Lorem lipsum",
                CreatedAt = new DateTime(2014,2,10)
            }
        };
    }
}
