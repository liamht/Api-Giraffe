using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Ninject;
using APIGirrafe.UI.ViewModels;
using APIGirrafe.Data.Entities;
using APIGirrafe.Data.Repository;
using APIGirrafe.Data.UnitOfWork;
using APIGirrafe.Domain.Services;
using APIGirrafe.UI.Navigation;
using APIGirrafe.UI.Views;

namespace APIGirrafe.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private INavigationHelper _navigation;
        private IKernel _container;

        public App()
        {
            InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            BindKernel();
            EnsureDatabaseIsUpToDate();

            _navigation = _container.Get<INavigationHelper>();

            var viewmodel = _container.Get<MainWindowViewModel>();
            viewmodel.OnNewGroupButtonClicked += (sender, args) =>
            {
                _navigation.ShowModal(new CreateGroupDialog(), _container.Get<NewGroupViewModel>());
            };

            var window = new MainWindow
            {
                DataContext = viewmodel
            };
            Current.MainWindow = window;

            _navigation.RefreshMenu();
            Current.MainWindow.Show();
        }

        private void EnsureDatabaseIsUpToDate()
        {
            var context = (SqliteUnitOfWork)_container.Get<IUnitOfWork>();
            context.Database.Migrate();
        }

        private void BindKernel()
        {
            _container = new StandardKernel();
            _container.Bind<INavigationHelper>().To<NavigationHelper>().WithConstructorArgument<Func<CurrentRequestViewModel>>(() => _container.Get<CurrentRequestViewModel>());
            _container.Bind<IRequestService>().To<RequestService>();
            _container.Bind<IGroupService>().To<GroupService>();

            _container.Bind<IRepository<Request>>().To<RequestRepository>();
            _container.Bind<IRepository<RequestGroup>>().To<RequestGroupRepository>();
            
            var dbOptions = new DbContextOptionsBuilder().UseSqlite(@"Data Source=ApiTester.db");
            _container.Bind<IUnitOfWork>().To<SqliteUnitOfWork>().WithConstructorArgument(dbOptions.Options);

            _container.Bind<MainWindowViewModel>().To<MainWindowViewModel>().InSingletonScope()
                .WithConstructorArgument<Func<NewRequestViewModel>>(() => _container.Get<NewRequestViewModel>())
                .WithConstructorArgument<Func<CurrentRequestViewModel>>(() => _container.Get<CurrentRequestViewModel>());
        }
    }
}

