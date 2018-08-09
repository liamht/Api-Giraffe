using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Ninject;
using APIGiraffe.UI.ViewModels;
using APIGiraffe.Data.Entities;
using APIGiraffe.Data.Repository;
using APIGiraffe.Data.UnitOfWork;
using APIGiraffe.UI.Navigation;
using APIGiraffe.UI.Views;
using APIGiraffe.UI.ViewModels.Menus.Factory;
using APIGiraffe.ApplicationServices.Headers.Commands.AddNewHeader;
using APIGiraffe.ApplicationServices.Headers.Commands.DeleteHeader;
using APIGiraffe.ApplicationServices.Headers.Commands.UpdateHeader;
using APIGiraffe.ApplicationServices.Headers.Commands.AddNewHeader.Factory;
using APIGiraffe.ApplicationServices.RequestGroups.Commands.AddNewRequestGroup;
using APIGiraffe.ApplicationServices.RequestGroups.Commands.DeleteRequestGroup;
using APIGiraffe.ApplicationServices.RequestGroups.Queries.GetRequestGroups;
using APIGiraffe.ApplicationServices.RequestGroups.Commands.AddNewRequestGroup.Factory;
using APIGiraffe.ApplicationServices.RequestGroups.Commands.RenameRequestGroup;
using APIGiraffe.ApplicationServices.Requests.Commands.UpdateRequest;
using APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequest;
using APIGiraffe.ApplicationServices.Requests.Queries.GetRequestDetails;
using APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequest.Factory;
using APIGiraffe.ApplicationServices.Requests.Commands.RenameRequest;
using APIGiraffe.ApplicationServices.Requests.Commands.DeleteRequest;

namespace APIGiraffe.UI
{
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
            _container.Bind<INavigationHelper>().To<NavigationHelper>().WithConstructorArgument(_container);

            _container.Bind<IHeaderFactory>().To<HeaderFactory>();
            _container.Bind<IRequestFactory>().To<RequestFactory>();
            _container.Bind<IRequestGroupFactory>().To<RequestGroupFactory>();

            _container.Bind<IAddNewRequestGroupCommand>().To<AddNewRequestGroupCommand>();
            _container.Bind<IDeleteRequestGroupCommand>().To<DeleteRequestGroupCommand>();
            _container.Bind<IRenameRequestGroupCommand>().To<RenameRequestGroupCommand>();

            _container.Bind<IAddNewRequestCommand>().To<AddNewRequestCommand>();
            _container.Bind<IUpdateRequestCommand>().To<UpdateRequestCommand>();
            _container.Bind<IDeleteRequestCommand>().To<DeleteRequestCommand>();
            _container.Bind<IRenameRequestCommand>().To<RenameRequestCommand>();

            _container.Bind<IAddNewHeaderCommand>().To<AddNewHeaderCommand>();
            _container.Bind<IDeleteHeaderCommand>().To<DeleteHeaderCommand>();
            _container.Bind<IUpdateHeaderCommand>().To<UpdateHeaderCommand>();
            
            _container.Bind<IGetRequestDetailsQuery>().To<GetRequestDetailsQuery>();
            _container.Bind<IGetRequestGroupsQuery>().To<GetRequestGroupsQuery>();

            _container.Bind<IRepository<Request>>().To<RequestRepository>();
            _container.Bind<IRepository<Data.Entities.RequestGroup>>().To<RequestGroupRepository>();


            _container.Bind<IMenuGroupFactory>().To<MenuGroupFactory>();
            _container.Bind<IRequestMenuItemFactory>().To<RequestMenuItemFactory>();

            var dbOptions = new DbContextOptionsBuilder().UseSqlite(@"Data Source=ApiGiraffe.db");
            _container.Bind<IUnitOfWork>().To<SqliteUnitOfWork>().InSingletonScope().WithConstructorArgument(dbOptions.Options);

            _container.Bind<MainWindowViewModel>().To<MainWindowViewModel>().InSingletonScope();
        }
    }
}

