using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Ninject;
using APIGirrafe.UI.ViewModels;
using APIGirrafe.Data.Entities;
using APIGirrafe.Data.Repository;
using APIGirrafe.Data.UnitOfWork;
using APIGirrafe.UI.Navigation;
using APIGirrafe.UI.Views;
using APIGirrafe.ApplicationServices.Requests.Commands.AddNewHeader;
using APIGirrafe.ApplicationServices.Requests.Commands.AddNewRequest;
using APIGirrafe.ApplicationServices.Requests.Commands.AddNewRequestGroup;
using APIGirrafe.ApplicationServices.Requests.Commands.DeleteRequestGroup;
using APIGirrafe.ApplicationServices.Requests.Commands.UpdateRequest;
using APIGirrafe.ApplicationServices.Requests.Queries.GetRequestGroups;
using APIGirrafe.ApplicationServices.Requests.Queries.GetRequestDetails;
using APIGirrafe.ApplicationServices.Requests.Commands.AddNewHeader.Factory;
using APIGirrafe.ApplicationServices.Requests.Commands.AddNewRequest.Factory;
using APIGirrafe.ApplicationServices.Requests.Commands.AddNewRequestGroup.Factory;

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

            _container.Bind<IHeaderFactory>().To<HeaderFactory>();
            _container.Bind<IRequestFactory>().To<RequestFactory>();
            _container.Bind<IRequestGroupFactory>().To<RequestGroupFactory>();

            _container.Bind<IAddNewHeaderCommand>().To<AddNewHeaderCommand>();
            _container.Bind<IAddNewRequestCommand>().To<AddNewRequestCommand>();
            _container.Bind<IAddNewRequestGroupCommand>().To<AddNewRequestGroupCommand>();
            _container.Bind<IDeleteRequestGroupCommand>().To<DeleteRequestGroupCommand>();
            _container.Bind<IUpdateRequestCommand>().To<UpdateRequestCommand>();

            _container.Bind<IGetRequestDetailsQuery>().To<GetRequestDetailsQuery>();
            _container.Bind<IGetRequestGroupsQuery>().To<GetRequestGroupsQuery>();

            _container.Bind<IRepository<Request>>().To<RequestRepository>();
            _container.Bind<IRepository<Data.Entities.RequestGroup>>().To<RequestGroupRepository>();
            
            var dbOptions = new DbContextOptionsBuilder().UseSqlite(@"Data Source=ApiTester.db");
            _container.Bind<IUnitOfWork>().To<SqliteUnitOfWork>().WithConstructorArgument(dbOptions.Options);

            _container.Bind<MainWindowViewModel>().To<MainWindowViewModel>().InSingletonScope()
                .WithConstructorArgument<Func<NewRequestViewModel>>(() => _container.Get<NewRequestViewModel>())
                .WithConstructorArgument<Func<CurrentRequestViewModel>>(() => _container.Get<CurrentRequestViewModel>());
        }
    }
}

