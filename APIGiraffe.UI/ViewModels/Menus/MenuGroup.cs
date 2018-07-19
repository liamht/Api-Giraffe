using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using APIGiraffe.UI.ViewModels.Commands;

namespace APIGiraffe.UI.ViewModels.Menus
{
    public class MenuGroup : NotifyableViewModel
    {
        public ICommand AddNewItemCommand { get; }

        public ICommand DeleteGroupCommand { get; }

        public ICommand RenameGroupCommand { get; }

        public string Name { get; set; }

        public ObservableCollection<RequestMenuItem> Items { get; set; }

        private RequestMenuItem _selectedItem;
        public RequestMenuItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                value?.OnSelected?.Invoke();
                NotifyPropertyChanged(nameof(SelectedItem));
            }
        }

        public MenuGroup(string name, Action addNewItemAction, Action deleteGroupAction, 
            Action renameGroupAction, params RequestMenuItem[] items)
        {
            AddNewItemCommand = new ActionCommand(addNewItemAction);
            DeleteGroupCommand = new ActionCommand(deleteGroupAction);
            RenameGroupCommand = new ActionCommand(renameGroupAction);

            Items = new ObservableCollection<RequestMenuItem>(items);
            Name = name;
        }
    }
}
