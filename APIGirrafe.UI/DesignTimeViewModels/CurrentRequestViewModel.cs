﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using APIGirrafe.Domain;

namespace APIGirrafe.UI.DesignTimeViewModels
{
    public class CurrentRequestViewModel
    {

        public ICommand GetResponseCommand { get; set; }

        public string Url { get; set; }

        public string Response { get; set; }

        public string Name { get; set; }

        public ObservableCollection<Header> RequestHeaders { get; set; }

    }
}