using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Affirmations.View
{
    public partial class WelcomePage : PhoneApplicationPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/ListPage.xaml", UriKind.Relative));
        }
    }
}