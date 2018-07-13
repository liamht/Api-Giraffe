using System;
using APIGirrafe.Domain;
using APIGirrafe.Domain.Services;
using APIGirrafe.UI.Navigation;

namespace APIGirrafe.UI.ViewModels
{
    public class NewRequestViewModel : NewItemViewModel
    {
        public override string Title => "Create New Request";

        private readonly IRequestService _service;

        private RequestGroup _group;

        public NewRequestViewModel(INavigationHelper navigation, IRequestService service) 
            : base(navigation)
        {
            _service = service;
        }

        public void SetGroupId(RequestGroup group)
        {
            _group = group;
        }

        public override void OnSuccess()
        {
            if (_group == null)
            {
                throw new ArgumentException("group for request not set");
            }

            var request = new SoapRequest() { RequestName = ItemName, GroupId = _group.Id};

            _service.CreateRequest(request);
            Navigation.DestroyModal();
            Navigation.RefreshMenu();
        }
    }
}
