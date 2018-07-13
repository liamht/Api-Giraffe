using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using APIGirrafe.Domain;
using APIGirrafe.UI.ViewModels.Menus;
using APIGirrafe.Domain.Services;
using APIGirrafe.UI.Navigation;
using APIGirrafe.UI.Views;
using Menu = APIGirrafe.UI.ViewModels.Menus.Menu;

namespace APIGirrafe.UI.ViewModels
{
    public class MainWindowViewModel : NotifyableViewModel
    {
        public event EventHandler OnNewGroupButtonClicked;

        private readonly INavigationHelper _navigation;
        private readonly IGroupService _groupService;

        private readonly Func<NewRequestViewModel> _newRequestViewModelCreator;
        private readonly Func<CurrentRequestViewModel> _currentRequestViewModelCreator;

        #region Properties

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        private UserControl _currentPage;
        public UserControl CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                NotifyPropertyChanged(nameof(CurrentPage));
            }
        }

        private UserControl _currentDialog;
        public UserControl CurrentDialog
        {
            get => _currentDialog;
            set
            {
                _currentDialog = value;
                NotifyPropertyChanged(nameof(CurrentDialog));
            }
        }

        private bool _isDialogShowing;
        public bool IsDialogShowing
        {
            get => _isDialogShowing;
            set
            {
                _isDialogShowing = value;
                NotifyPropertyChanged(nameof(IsDialogShowing));
            }
        }

        private Menu _menu;
        public Menu Menu
        {
            get => _menu;
            set
            {
                _menu = value;
                NotifyPropertyChanged(nameof(Menu));
            }
        }

        public ICommand NewRequestCommand { get; set; }

        #endregion

        public MainWindowViewModel(IGroupService groupService, INavigationHelper navigation, 
            Func<NewRequestViewModel> newRequestInitiator, Func<CurrentRequestViewModel> currentRequestInitiator)
        {
            _navigation = navigation;
            _groupService = groupService;
            _newRequestViewModelCreator = newRequestInitiator;
            _currentRequestViewModelCreator = currentRequestInitiator;

            NewRequestCommand = new ActionCommand(() => OnNewGroupButtonClicked?.Invoke(this, new EventArgs()));
        }

        public void ShowDialog(UserControl dialogContent)
        {
            CurrentDialog = dialogContent;
            IsDialogShowing = true;
        }

        public void HideDialog()
        {
            CurrentDialog = null;
            IsDialogShowing = false;
        }

        public void LoadMenu()
        {
            var groups = _groupService.FetchFromDatabase();

            var menuGroups = groups.Select(group => new MenuGroup(group.Name,
                GetNewRequestDialogAction(group),
                GetDeleteGroupAction(group),
                group.Requests.Select(request => new RequestMenuItem(request.RequestName, request.Id, GetNavigateToCurrentRequestPageAction(request))).ToArray()
            ));

            Menu = new Menu()
            {
                Groups = new ObservableCollection<MenuGroup>(menuGroups)
            };
        }

        private Action GetNewRequestDialogAction(Domain.RequestGroup requestGroup)
        {
            return () =>
            {
                var vm = _newRequestViewModelCreator.Invoke();
                vm.SetGroupId(requestGroup);
                _navigation.ShowModal(new NewRequestDialog(), vm);
            };
        }

        private Action GetDeleteGroupAction(Domain.RequestGroup requestGroup)
        {
            return () =>
            {
                _groupService.Delete(requestGroup);
                _navigation.RefreshMenu();
            };
        }

        private Action GetNavigateToCurrentRequestPageAction(SoapRequest request)
        {
            return () =>
            {
                var vm = _currentRequestViewModelCreator.Invoke();
                vm.Name = request.RequestName;
                _navigation.NavigateTo(new CurrentRequestPage(), vm);
                vm.LoadValues(request.Id);
            };
        }
    }
}

