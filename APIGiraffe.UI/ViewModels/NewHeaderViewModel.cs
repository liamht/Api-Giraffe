using APIGiraffe.ApplicationServices.Requests.Commands.AddNewHeader;
using APIGiraffe.UI.Navigation;
using System;

namespace APIGiraffe.UI.ViewModels
{
    public class NewHeaderViewModel : NewItemViewModel
    {
        private readonly IAddNewHeaderCommand _command;

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
        : base(navigation)
        {
            _command = command;
        }


        public void ForRequest(int requestId)
        {
            _requestId = requestId;
        }

        public override void OnSuccess()
        {
            _command.Execute(_requestId, ItemName, ItemValue);

            Navigation.DestroyModal();
            Navigation.RefreshMenu();

            OnSuccessCallback.Invoke(this, new EventArgs());
        }
    }
}
