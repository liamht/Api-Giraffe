using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using APIGirrafe.ApplicationServices.Requests.Commands.AddNewHeader;
using APIGirrafe.ApplicationServices.Requests.Commands.UpdateRequest;
using APIGirrafe.ApplicationServices.Requests.Queries.GetRequestDetails;
using APIGirrafe.Domain;
using APIGirrafe.UI.Navigation;
using APIGirrafe.UI.Views;
using Header = APIGirrafe.ApplicationServices.Requests.Queries.GetRequestDetails.Header;

namespace APIGirrafe.UI.ViewModels
{
    public class CurrentRequestViewModel : BasePageViewModel
    {
        public override string Title => this.Name;
        
        private readonly IAddNewHeaderCommand _addHeaderCommand;
        private readonly IUpdateRequestCommand _updateRequestCommand;
        private readonly IGetRequestDetailsQuery _getDetailsQuery;
        private readonly INavigationHelper _navigationHelper;

        private bool _changesEnabled;

        private int _requestId;

        #region Commands

        public ICommand GetResponseCommand { get; set; }

        public ICommand AddHeaderCommand { get; set; }

        public ICommand DeleteHeaderCommand { get; set; }

        public ICommand EditHeaderCommand { get; set; }

        #endregion

        #region Page properties

        private string _url;

        public string Url
        {
            get => _url;
            set
            {
                var hasChanged = _url != value && !string.IsNullOrWhiteSpace(value) && _changesEnabled;
                _url = value;
                if (hasChanged)
                {
                    _updateRequestCommand.Execute(_requestId, Url);
                }
                NotifyPropertyChanged(nameof(Url));
            }
        }

        private string _response;

        public string Response
        {
            get => _response;
            set
            {
                _response = value;
                NotifyPropertyChanged(nameof(Response));
            }
        }

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                var hasChanged = _name != value && !string.IsNullOrWhiteSpace(value) && _changesEnabled;
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        #endregion

        public ObservableCollection<Header> RequestHeaders { get; set; }

        public CurrentRequestViewModel(IAddNewHeaderCommand addHeaderCommand, IUpdateRequestCommand updateRequestCommand,
            IGetRequestDetailsQuery getDetailsQuery, INavigationHelper nav)
        {
            Response = "The response from the server will show here when a request is sent";

            RequestHeaders = new ObservableCollection<Header>();
            GetResponseCommand = new ActionCommand(async () => await GetResponse());
            AddHeaderCommand = new ActionCommand(() => ShowAddHeaderModal());
            
            _addHeaderCommand = addHeaderCommand;
            _updateRequestCommand = updateRequestCommand;
            _getDetailsQuery = getDetailsQuery;
            _navigationHelper = nav;
        }

        private void ShowAddHeaderModal()
        {
            var vm = new NewHeaderViewModel(_navigationHelper, _addHeaderCommand, _requestId);
            vm.OnSuccessCallback += (sender, args) =>
            {
                AddHeaderFromViewModel(vm);
            };

            _navigationHelper.ShowModal(new NewHeaderDialog(), vm);
        }

        private void AddHeaderFromViewModel(NewHeaderViewModel vm)
        {
            RequestHeaders.Add(new Header() { Name = vm.ItemName, Value = vm.ItemValue });
        }

        private async Task GetResponse()
        {
            var httprequest = new Request()
            {
                Url = this.Url,
            };

            foreach (var header in RequestHeaders)
            {
                httprequest.AddHeader(new Domain.Header() { Name = header.Name, Value = header.Value });
            }

            Response = await httprequest.GetResponse();
        }

        public async Task LoadValues(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Cannot fetch ID from database");
            }

            var request = await _getDetailsQuery.ExecuteAsync(id);

            _requestId = request.Id;
            Url = request.Url;

            RequestHeaders = request.Headers == null ? new ObservableCollection<Header>() : new ObservableCollection<Header>(request.Headers);
            _changesEnabled = true;
        }
    }
}
