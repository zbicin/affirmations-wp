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
using Affirmations.Model;

namespace Affirmations.View
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        private string affirmationIndex;

        public DetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            affirmationIndex = "-1";

            if (NavigationContext.QueryString.TryGetValue("affirmationIndex", out affirmationIndex))
            {
                DataContext = new DetailsViewModel(affirmationIndex);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ta afirmacja zostanie usunięta z Twojej listy.", "Afirmacje", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Repository.Affirmations.RemoveAt(Convert.ToInt16(affirmationIndex));
                Repository.SaveChanges();

                try
                {
                    NavigationService.GoBack();
                }
                catch (InvalidOperationException exception)
                {
                    NavigationService.Navigate(new Uri("/View/ListPage.xaml", UriKind.Relative));
                }
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/AddPage.xaml?affirmationId=" + affirmationIndex, UriKind.Relative));
        }
    }
}