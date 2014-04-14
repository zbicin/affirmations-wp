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

        private bool _isRepetitionFinished;
        public bool IsRepetitionFinished
        {
            get
            {
                return _isRepetitionFinished;
            }
            set
            {
                SetProperty<bool>(ref _isRepetitionFinished, value);
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

        private bool _isFinishVisible;
        public bool IsFinishVisible
        {
            get
            {
                return _isFinishVisible;
            }
            set
            {
                SetProperty<bool>(ref _isFinishVisible, value);
            }
        }

        private bool _isNextAffirmationVisible;
        public bool IsNextAffirmationVisible
        {
            get
            {
                return _isNextAffirmationVisible;
            }
            set
            {
                SetProperty<bool>(ref _isNextAffirmationVisible, value);
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
            Affirmations = App.ViewModel.Affirmations;
            CurrentAffirmation = Affirmations[CurrentAffirmationIndex];
            IsRepetitionFinished = Affirmations.Count == 1;
            
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

        public void UpdateSwitchesAvailability()
        {
            IsNextAffirmationAvailable = true;
            IsPreviousAffirmationAvailable = CurrentAffirmationIndex > 0;
            IsNextAffirmationVisible = !(CurrentAffirmationIndex == Affirmations.Count - 1);
            IsFinishVisible = !IsNextAffirmationVisible;

            if (IsRepetitionFinished == false
                && IsNextAffirmationVisible == false)
            {
                IsRepetitionFinished = true;
            }
        }

        public void DisableSwitches()
        {
            IsNextAffirmationAvailable = IsPreviousAffirmationAvailable = false;
        }
    }
}
