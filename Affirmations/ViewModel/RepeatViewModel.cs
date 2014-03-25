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

        private Affirmation _currentAffirmation;
        public Affirmation CurrentAffirmation
        {
            get
            {
                return _currentAffirmation;
            }
            set
            {
                SetProperty<Affirmation>(ref _currentAffirmation, value);
            }
        }

        private bool _isNextAffirmationAvailable;
        public bool IsNextAffirmationAvailable
        {
            get
            {
                return _isNextAffirmationAvailable;
            }
            set
            {
                SetProperty<bool>(ref _isNextAffirmationAvailable, value);
            }
        }

        private bool _isPreviousAffirmationAvailable;
        public bool IsPreviousAffirmationAvailable
        {
            get
            {
                return _isPreviousAffirmationAvailable;
            }
            set
            {
                SetProperty<bool>(ref _isPreviousAffirmationAvailable, value);
            }
        }

        private bool _isFinishAvailable;
        public bool IsFinishAvailable
        {
            get
            {
                return _isFinishAvailable;
            }
            set
            {
                SetProperty<bool>(ref _isFinishAvailable, value);
            }
        }

        private int _currentAffirmationIndex;
        public int CurrentAffirmationIndex
        {
            get
            {
                return _currentAffirmationIndex;
            }
            set
            {
                SetProperty<int>(ref _currentAffirmationIndex, value);
            }
        }

        public RepeatViewModel()
        {
            CurrentAffirmationIndex = 0;
            BarMode = ApplicationBarMode.Default;
            Affirmations = Repository.Affirmations;
            CurrentAffirmation = Affirmations[CurrentAffirmationIndex];
            IsFinishAvailable = Affirmations.Count == 1;
            
            UpdateSwitchesAvailability();
        }

        public void NextAffirmation()
        {
            if (CurrentAffirmationIndex + 1 < Affirmations.Count)
            {
                CurrentAffirmationIndex++;
                CurrentAffirmation = Affirmations[CurrentAffirmationIndex];
            }

            UpdateSwitchesAvailability();
        }

        public void PreviousAffirmation()
        {
            if (CurrentAffirmationIndex > 0)
            {
                CurrentAffirmationIndex--;
                CurrentAffirmation = Affirmations[CurrentAffirmationIndex];
            }

            UpdateSwitchesAvailability();
        }

        private void UpdateSwitchesAvailability()
        {
            IsNextAffirmationAvailable = !(CurrentAffirmationIndex == Affirmations.Count - 1);
            IsPreviousAffirmationAvailable = CurrentAffirmationIndex > 0;

            if (IsFinishAvailable == false
                && IsNextAffirmationAvailable == false)
            {
                IsFinishAvailable = true;
            }
        }
    }
}
