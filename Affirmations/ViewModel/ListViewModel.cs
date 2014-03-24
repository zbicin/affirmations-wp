﻿using Affirmations.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Affirmations.ViewModel
{
    public class ListViewModel : BindableBase
    {
        public ObservableCollection<Affirmation> Affirmations { get; set; }
        public bool duppa { get {
            return Affirmations != null && Affirmations.Count > 0; 
        } }
        

        public ListViewModel()
        {
            Affirmations = Repository.Affirmations;
        }
    }
}
