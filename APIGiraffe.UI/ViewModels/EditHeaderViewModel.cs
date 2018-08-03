using APIGiraffe.ApplicationServices.Requests.Commands.UpdateHeader;
using APIGiraffe.UI.Navigation;
using APIGiraffe.UI.ViewModels.Commands;
using System.Windows.Input;

namespace APIGiraffe.UI.ViewModels
{
    public class EditHeaderViewModel : BasePageViewModel
    {
        private readonly IUpdateHeaderCommand _command;

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

        public ICommand EditCommand { get; private set; }

        public EditHeaderViewModel(INavigationHelper navigation, IUpdateHeaderCommand command)
        {
            _command = command;
            EditCommand = new ActionCommand(() =>
            {
                command.Execute(_headerId, ItemName, ItemValue);
                navigation.DestroyModal();
            });
        }
        
        public void SetExistingValues(int headerId, string name, string value)
        {
            _headerId = headerId;
            ItemName = name;
            ItemValue = value;
        }
    }
}
