using Affirmations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affirmations.ViewModel
{
    public class DetailsViewModel
    {
        public Affirmation Affirmation { get; set; }
        public string CreatedAtString { get; set; }

        public DetailsViewModel(string affirmationIndex)
        {
            Affirmation = Repository.Affirmations[Convert.ToInt16(affirmationIndex)];
            CreatedAtString = Affirmation.CreatedAt.ToShortDateString();
        }
    }
}
