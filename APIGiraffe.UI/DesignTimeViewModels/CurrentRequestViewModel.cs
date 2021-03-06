﻿using System.Collections.ObjectModel;
using System.Windows.Input;

namespace APIGiraffe.UI.DesignTimeViewModels
{
    public class CurrentRequestViewModel
    {
        public ICommand GetResponseCommand { get; set; }

        public ICommand AddHeaderCommand { get; set; }

        public ICommand DeleteHeaderCommand { get; set; }

        public ICommand EditHeaderCommand { get; set; }

        public string Url { get; set; }

        public string Response { get; set; }

        public string Name { get; set; }

        public ObservableCollection<Domain.Entities.Header> RequestHeaders { get; set; }

    }
}