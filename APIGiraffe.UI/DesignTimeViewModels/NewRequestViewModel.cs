using System.Windows.Input;

namespace APIGiraffe.UI.DesignTimeViewModels
{
    public class NewRequestViewModel
    {
        public ICommand OnConfirmCommand { get; set; }

        public string RequestName { get; set; }
    }
}
