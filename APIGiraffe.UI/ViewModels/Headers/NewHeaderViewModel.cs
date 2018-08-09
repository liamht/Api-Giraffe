using APIGiraffe.ApplicationServices.Headers.Commands.AddNewHeader;
using APIGiraffe.UI.Navigation;
using System;

namespace APIGiraffe.UI.ViewModels.Headers
{
    public class NewHeaderViewModel : NewItemViewModel
    {
        private readonly IAddNewHeaderCommand _command;

        private readonly INavigationHelper _navigation;

        public event EventHandler OnSuccessCallback;

        private int _requestId;

        public override string Title => "Add New Paramter";

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

        public NewHeaderViewModel(INavigationHelper navigation, IAddNewHeaderCommand command)
        : base()
        {
            _command = command;
            _navigation = navigation;
        }

        public void ForRequest(int requestId)
        {
            _requestId = requestId;
        }

        public override void OnValidationComplete()
        {
            _command.Execute(_requestId, ItemName, ItemValue);

            _navigation.DestroyModal();
            _navigation.RefreshMenu();

            OnSuccessCallback?.Invoke(this, new EventArgs());
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
    }
}
