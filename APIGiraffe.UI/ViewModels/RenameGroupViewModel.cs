using APIGiraffe.ApplicationServices.Requests.Commands.RenameRequestGroup;
using APIGiraffe.UI.Navigation;

namespace APIGiraffe.UI.ViewModels
{
    public class RenameGroupViewModel : NewItemViewModel
    {
        public override string Title => "Rename Group";

        private readonly IRenameRequestGroupCommand _command;

        private int _groupId;

        public RenameGroupViewModel(INavigationHelper navigation, IRenameRequestGroupCommand command) 
            : base(navigation)
        {
            _command = command;
        }

        public void SetValues(int id, string currentName)
        {
            _groupId = id;
            ItemName = currentName;
        }

        public override void OnSuccess()
        {
            _command.Execute(_groupId, ItemName);

            Navigation.DestroyModal();
            Navigation.RefreshMenu();
        }
    }
}
