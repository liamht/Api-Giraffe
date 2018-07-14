﻿using System.Windows.Input;
using APIGirrafe.UI.Navigation;

namespace APIGirrafe.UI.ViewModels
{
    public abstract class NewItemViewModel : BasePageViewModel
    {
        protected readonly INavigationHelper Navigation;

        public ICommand OnConfirmCommand => new ActionCommand(OnSuccess);

        private string _itemName;
        public string ItemName
        {
            get => _itemName;
            set
            {
                _itemName = value;
                NotifyPropertyChanged(nameof(ItemName));
            }
        }

        protected NewItemViewModel(INavigationHelper navigation)
        {
            Navigation = navigation;
        }

        public abstract void OnSuccess();
    }
}
