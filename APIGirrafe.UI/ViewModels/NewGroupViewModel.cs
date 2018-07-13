using APIGirrafe.Domain;
using APIGirrafe.Domain.Services;
using APIGirrafe.UI.Navigation;

namespace APIGirrafe.UI.ViewModels
{
    public class NewGroupViewModel : NewItemViewModel
    {
        public override string Title => "Create New Group";

        private readonly IGroupService _service;

        public NewGroupViewModel(INavigationHelper navigation, IGroupService service) 
            : base(navigation)
        {
            _service = service;
        }

        public override void OnSuccess()
        {
            var group = new RequestGroup() { Name = ItemName };

            _service.AddNewGroup(group);
            Navigation.DestroyModal();
            Navigation.RefreshMenu();
        }
    }
}
