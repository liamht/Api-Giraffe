using APIGiraffe.UI.ViewModels.Commands;
using System;
using System.Windows.Input;

namespace APIGiraffe.UI.ViewModels.Menus
{
    public class RequestMenuItem
    {
        public int ItemId { get; }

        public string Text { get; }

        public Action OnSelected { get; }

        public ICommand RenameRequestCommand { get; }
        
        public RequestMenuItem(string text, int itemId, Action onSelected, 
            Action renameRequest)
        {
            Text = text;
            ItemId = itemId;
            OnSelected = onSelected;

            RenameRequestCommand = new ActionCommand(renameRequest);
        }
    }
}
