using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using APIGiraffe.UI.Navigation;
using APIGiraffe.UI.ViewModels.Commands;

namespace APIGiraffe.UI.ViewModels
{
    public abstract class NewItemViewModel : BasePageViewModel
    {
        public ICommand OnConfirmCommand => new ActionCommand(OnSuccess);
        
        protected bool IsValid { get; set; } = true;

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

        private string _itemNameErrorMessage;
        public string ItemNameErrorMessage
        {
            get => _itemNameErrorMessage;
            set
            {
                _itemNameErrorMessage = value;
                NotifyPropertyChanged(nameof(ItemNameErrorMessage));
            }
        }

        protected NewItemViewModel()
        {

        }

        public abstract void Validate();

        public abstract void OnValidationComplete();
        
        public virtual void OnSuccess()
        {
            Validate();
            if (!IsValid)
            {
                return;
            }
            OnValidationComplete();
        }
    }
}
