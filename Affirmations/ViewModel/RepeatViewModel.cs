using Affirmations.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affirmations.ViewModel
{
    public class RepeatViewModel
    {
        public ObservableCollection<Affirmation> Affirmations { get; set; }

        public RepeatViewModel()
        {
            Affirmations = Repository.Affirmations;
        }
    }
}
