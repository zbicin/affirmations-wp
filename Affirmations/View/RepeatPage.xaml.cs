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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                NavigationService.Navigate(new Uri("/View/ListPage.xaml", UriKind.Relative));
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            startAnimation(() => viewModel.NextAffirmation());
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            startAnimation(() => viewModel.PreviousAffirmation());
        }

        private void startAnimation(Action switchAffirmationMethod) {
            EventHandler temporalFadeOutCompletedHandler = null;
            temporalFadeOutCompletedHandler = (completedFadeOutSender, completedFadeOutEvents) =>
            {
                EventHandler temporalFadeInCompletedHandler = null;
                temporalFadeInCompletedHandler = (completedFadeInSender, completedFadeInEvents) =>
                {
                    viewModel.UpdateSwitchesAvailability();
                    sbFadeIn.Completed -= temporalFadeInCompletedHandler;
                };

                switchAffirmationMethod();

                sbFadeIn.Completed += temporalFadeInCompletedHandler;
                sbFadeIn.Begin();

                sbFadeOut.Completed -= temporalFadeOutCompletedHandler;
            };

            sbFadeOut.Completed += temporalFadeOutCompletedHandler;

            viewModel.DisableSwitches();

            sbFadeOut.Begin();
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            FinishRepetition();

            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                NavigationService.Navigate(new Uri("/View/ListPage.xaml", UriKind.Relative));
            }
        }

        private void FinishRepetition()
        {
            App.ViewModel.LastRepetitionDate = DateTime.Now;
            App.ViewModel.SaveSettings();

        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (viewModel.IsFinishAvailable)
            {
                //user has seen all of the affirmations, we can update his LastRepetitionDate
                FinishRepetition();
            }

            base.OnBackKeyPress(e);
        }
    }
}