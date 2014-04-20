using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Affirmations.ViewModel;
using System.Windows.Media.Animation;

namespace Affirmations.View
{
    public partial class RepeatPage : PhoneApplicationPage
    {
        private RepeatViewModel viewModel;
        public RepeatPage()
        {
            InitializeComponent();

            viewModel = new RepeatViewModel();
            DataContext = viewModel;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            startAnimation(sbRightFadeIn, sbLeftFadeOut, () => viewModel.NextAffirmation());
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            startAnimation(sbLeftFadeIn, sbRightFadeOut, () => viewModel.PreviousAffirmation());
        }

        private void startAnimation(Storyboard animationIn, Storyboard animationOut, Action switchAffirmationMethod) {
            EventHandler temporalFadeOutCompletedHandler = null;
            temporalFadeOutCompletedHandler = (completedFadeOutSender, completedFadeOutEvents) =>
            {
                EventHandler temporalFadeInCompletedHandler = null;
                temporalFadeInCompletedHandler = (completedFadeInSender, completedFadeInEvents) =>
                {
                    viewModel.UpdateSwitchesAvailability();
                    animationIn.Completed -= temporalFadeInCompletedHandler;
                };

                switchAffirmationMethod();

                animationIn.Completed += temporalFadeInCompletedHandler;
                animationIn.Begin();

                animationOut.Completed -= temporalFadeOutCompletedHandler;
            };

            animationOut.Completed += temporalFadeOutCompletedHandler;

            viewModel.DisableSwitches();

            animationOut.Begin();
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            UpdateLastRepetitionDate();

            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                NavigationService.Navigate(new Uri("/View/ListPage.xaml", UriKind.Relative));
            }
        }

        private void UpdateLastRepetitionDate()
        {
            App.ViewModel.LastRepetitionDate = DateTime.Now;
            App.ViewModel.SaveSettings();

        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (viewModel.IsRepetitionFinished)
            {
                //user has seen all of the affirmations, we can update his LastRepetitionDate
                UpdateLastRepetitionDate();
            }

            base.OnBackKeyPress(e);
        }
    }
}