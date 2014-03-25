using Affirmations.Model;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affirmations.ViewModel
{
    public class RepeatViewModel : BindableBase
    {
        public ObservableCollection<Affirmation> Affirmations { get; set; }

        private ApplicationBarMode _barMode;
        public ApplicationBarMode BarMode
        {
            get
            {
                return _barMode;
            }
            set
            {
                SetProperty<ApplicationBarMode>(ref _barMode, value);
            }
        }

        public RepeatViewModel()
        {
            BarMode = ApplicationBarMode.Minimized;
            Affirmations = Repository.Affirmations;
        }
    }
}
