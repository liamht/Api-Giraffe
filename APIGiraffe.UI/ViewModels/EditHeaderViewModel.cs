using APIGiraffe.ApplicationServices.Requests.Commands.UpdateHeader;
using APIGiraffe.UI.Navigation;
using APIGiraffe.UI.ViewModels.Commands;
using System;
using System.Windows.Input;

namespace APIGiraffe.UI.ViewModels
{
    public class EditHeaderViewModel : ViewModelWithSuccessCallback
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

        public override void OnSuccess()
        {
            _command.Execute(_headerId, ItemName, ItemValue);
            _navigation.DestroyModal();
            OnDeleteSuccessful.Invoke(this, new EventArgs());
        }
    }
}
