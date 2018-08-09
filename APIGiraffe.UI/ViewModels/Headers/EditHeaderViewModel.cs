using APIGiraffe.ApplicationServices.Headers.Commands.UpdateHeader;
using APIGiraffe.UI.Navigation;
using System;

namespace APIGiraffe.UI.ViewModels.Headers
{
    public class EditHeaderViewModel : NewItemViewModel
    {
        public event EventHandler OnDeleteSuccessful;

        private readonly IUpdateHeaderCommand _command;

        private readonly INavigationHelper _navigation;               

        public override string Title => "Edit Header";
        
        private int _headerId;

        private string _itemValue;
        public string ItemValue
        {
            get => _itemValue;
            set
            {
                _itemValue = value;
                NotifyPropertyChanged(nameof(ItemValue));
            }
        }

        private string _itemValueErrorMessage;
        public string ItemValueErrorMessage
        {
            get => _itemValueErrorMessage;
            set
            {
                _itemValueErrorMessage = value;
                NotifyPropertyChanged(nameof(ItemValueErrorMessage));
            }
        }
        public EditHeaderViewModel(INavigationHelper navigation, IUpdateHeaderCommand command)
        {
            _command = command;
            _navigation = navigation;
        }
        
        public void SetExistingValues(int headerId, string name, string value)
        {
            _headerId = headerId;
            ItemName = name;
            ItemValue = value;
        }

        public override void Validate()
        {
            ItemNameErrorMessage = string.Empty;
            ItemValueErrorMessage = string.Empty;
            IsValid = true;

            if (string.IsNullOrWhiteSpace(ItemName))
            {
                ItemNameErrorMessage = "Header name cannot be empty";
                IsValid = false;
            }

            if (string.IsNullOrWhiteSpace(ItemValue))
            {
                ItemValueErrorMessage = "Header value cannot be empty";
                IsValid = false;
            }
        }

        public override void OnValidationComplete()
        {
            _command.Execute(_headerId, ItemName, ItemValue);
            _navigation.DestroyModal();
            OnDeleteSuccessful?.Invoke(this, new EventArgs());
        }
    }
}
