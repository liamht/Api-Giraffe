using APIGiraffe.UI.Navigation;

namespace APIGiraffe.UI.ViewModels
{
    public abstract class NewItemViewModel : ViewModelWithSuccessCallback
    {
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

        protected NewItemViewModel()
        {
        }
    }
}
