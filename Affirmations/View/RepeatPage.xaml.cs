﻿using System;
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
        public RepeatPage()
        {
            InitializeComponent();
            DataContext = new RepeatViewModel();
        }
    }
}