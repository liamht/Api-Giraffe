using APIGiraffe.ApplicationServices.Requests.Commands.AddNewHeader;
using APIGiraffe.UI.Navigation;
using System;

namespace APIGiraffe.UI.ViewModels
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

        public override void OnSuccess()
        {
            _command.Execute(_requestId, ItemName, ItemValue);

            _navigation.DestroyModal();
            _navigation.RefreshMenu();

            OnSuccessCallback.Invoke(this, new EventArgs());
        }
    }
}
