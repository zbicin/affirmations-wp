using Affirmations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affirmations.ViewModel
{
    public class AddViewModel : BindableBase
    {
        public Affirmation NewAffirmation { get; set; }

        public AddViewModel()
        {
            NewAffirmation = new Affirmation
            {
                Text = "Jesteś zwycięzcą!",
                CreatedAt = DateTime.Now
            };

        }
    }
}
