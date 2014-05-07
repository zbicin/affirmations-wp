using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Affirmations.Resources;
using Affirmations.Model;
using Affirmations.ViewModel;
using System.Collections.ObjectModel;
using System.Text;

namespace Affirmations.View
{
    public partial class ListPage : PhoneApplicationPage
    {
        private Uri RepeatUri;

        // Constructor
        public ListPage()
        {
            InitializeComponent();

            DataContext = App.ViewModel;
            RepeatUri = new Uri("/View/RepeatPage.xaml", UriKind.Relative);

            UpdateListVisibility();

#if DEBUG
            pvPages.Title += " WERSJA DEWELOPERSKA";
#endif
        }

        private void ListedAffirmation_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel spSender = (StackPanel)sender;
            Affirmation affirmation = (Affirmation)spSender.DataContext;
            int affirmationIndex = App.ViewModel.Affirmations.IndexOf(affirmation);
         
            NavigationService.Navigate(new Uri("/View/EditPage.xaml?affirmationIndex=" + affirmationIndex, UriKind.Relative));
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/AddPage.xaml", UriKind.Relative));
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/SettingsPage.xaml", UriKind.Relative));
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            string aboutContents = String.Format("Afirmacje {0}\n\u00a92014 Krzysztof Zbiciński\n\nW razie pytań, problemów czy sugestii, łap mnie mailowo: k.zbicinski@gmail.com.", App.GetVersion());

            MessageBox.Show(aboutContents, "O programie", MessageBoxButton.OK);
        }

        private void htRepeat_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
             NavigationService.Navigate(RepeatUri);
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/SettingsPage.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateListVisibility();
        }

        private void UpdateListVisibility()
        {
            // hide list page if there are no affirmations
            if (App.ViewModel.Affirmations.Count < 1)
            {
                pvPages.SelectedIndex = 0;
                pvPages.IsLocked = true;
            }
            else
            {
                pvPages.IsLocked = false;
            }
        }

        private void buttonRepeat_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/RepeatPage.xaml", UriKind.Relative));
        }

        private void buttonRate_Click(object sender, EventArgs e)
        {
            Microsoft.Phone.Tasks.MarketplaceReviewTask marketplaceReviewTask = new Microsoft.Phone.Tasks.MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        private void miPinRepetition_Click(object sender, RoutedEventArgs e)
        {
            StandardTileData newStartTile = new StandardTileData
            {
                Title = "powtórz afirmacje",
                BackgroundImage = new Uri("/Assets/Tiles/FlipCycleTileMedium.png", UriKind.Relative),
                Count = 0
            };

            ShellTile.Create(RepeatUri, newStartTile);
        }

        private void cmRepeat_Opened(object sender, RoutedEventArgs e)
        {
            ShellTile repeatTile = ShellTile.ActiveTiles.SingleOrDefault(t => t.NavigationUri.Equals(RepeatUri));

            miPinRepetition.IsEnabled = repeatTile == null;
        }
    }
}