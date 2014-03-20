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
    public partial class DetailsPage : PhoneApplicationPage
    {
        public DetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string affirmationIndex = "-1";

            if (NavigationContext.QueryString.TryGetValue("affirmationIndex", out affirmationIndex))
            {
                DataContext = new DetailsViewModel(affirmationIndex);
            }
        }
    }
}