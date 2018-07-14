using APIGirrafe.Domain;
using APIGirrafe.Domain.Services;
using APIGirrafe.UI.Navigation;

namespace APIGirrafe.UI.ViewModels
{
    public class NewHeaderViewModel : NewItemViewModel
    {
        private readonly IRequestService _service;

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

        public NewHeaderViewModel(INavigationHelper navigation, IRequestService service)
        : base(navigation)
        {
            _service = service;
        }

        public override void OnSuccess()
        {
            var header = new Header() { Name = ItemName, Value = ItemValue };

            var request = _service.GetById(_requestId);
            request.AddHeader(header);
            _service.UpdateRequest(request);

            Navigation.DestroyModal();
            Navigation.RefreshMenu();
        }
    }
}
