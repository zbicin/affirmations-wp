using Affirmations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Affirmations.ViewModel
{
    public class EditViewModel : BindableBase
    {
        public Affirmation EditedAffirmation { get; set; }

        public EditViewModel(string affirmationIndex)
        {
            EditedAffirmation = Repository.Affirmations[Convert.ToInt16(affirmationIndex)].Clone();
        }

    }
}
