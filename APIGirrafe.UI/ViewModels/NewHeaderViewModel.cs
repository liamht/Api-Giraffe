using APIGirrafe.Domain;
using APIGirrafe.Domain.Services;
using APIGirrafe.UI.Navigation;
using System;

namespace APIGirrafe.UI.ViewModels
{
    public class NewHeaderViewModel : NewItemViewModel
    {
        private readonly IRequestService _service;

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

        public NewHeaderViewModel(INavigationHelper navigation, IRequestService service, int requestId)
        : base(navigation)
        {
            _service = service;
            _requestId = requestId;
        }

        public override void OnSuccess()
        {
            var header = new Header() { Name = ItemName, Value = ItemValue };

            var request = _service.GetById(_requestId);
            request.AddHeader(header);
            _service.UpdateRequest(request);

            Navigation.DestroyModal();
            Navigation.RefreshMenu();

            OnSuccessCallback.Invoke(this, new EventArgs());
        }
    }
}
