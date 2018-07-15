using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using APIGirrafe.ApplicationServices.Requests.Commands.AddNewHeader;
using APIGirrafe.ApplicationServices.Requests.Commands.DeleteHeader;
using APIGirrafe.ApplicationServices.Requests.Commands.UpdateRequest;
using APIGirrafe.ApplicationServices.Requests.Queries.GetRequestDetails;
using APIGirrafe.Domain;
using APIGirrafe.UI.Navigation;
using APIGirrafe.UI.Views;
using APIGirrafe.UI.ViewModels.Commands;
using Header = APIGirrafe.ApplicationServices.Requests.Queries.GetRequestDetails.Header;
using System.Linq;

namespace APIGirrafe.UI.ViewModels
{
    public class CurrentRequestViewModel : BasePageViewModel
    {
        public override string Title => this.Name;
        
        private readonly IAddNewHeaderCommand _addHeaderCommand;
        private readonly IUpdateRequestCommand _updateRequestCommand;
        private readonly IDeleteHeaderCommand _deleteHeaderCommand;
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
            IDeleteHeaderCommand deleteHeaderCommand, IGetRequestDetailsQuery getDetailsQuery, INavigationHelper nav)
        {
            Response = "The response from the server will show here when a request is sent";

            RequestHeaders = new ObservableCollection<Header>();
            GetResponseCommand = new ActionCommand(async () => await GetResponse());
            AddHeaderCommand = new ActionCommand(() => ShowAddHeaderModal());
            DeleteHeaderCommand = new DeleteCommand(headerId => DeleteHeader(headerId));
            
            _addHeaderCommand = addHeaderCommand;
            _updateRequestCommand = updateRequestCommand;
            _deleteHeaderCommand = deleteHeaderCommand;
            _getDetailsQuery = getDetailsQuery;
            _navigationHelper = nav;
        }

        private void DeleteHeader(int headerId)
        {
            _deleteHeaderCommand.Execute(headerId);
            RefreshHeaders();
        }

        private void ShowAddHeaderModal()
        {
            _navigationHelper.ShowModal<NewHeaderDialog, NewHeaderViewModel>(vm =>
            {
                vm.OnSuccessCallback += (sender, args) =>
                {
                    RefreshHeaders();
                };
            });
        }

        private void RefreshHeaders()
        {
            var details = _getDetailsQuery.Execute(_requestId);
            var headers = details.Headers;
            RequestHeaders.Clear();
            headers.ForEach(header => RequestHeaders.Add(header));
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

        public void LoadValues(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Cannot fetch ID from database");
            }

            var request = _getDetailsQuery.Execute(id);

            _requestId = request.Id;
            Url = request.Url;

            RequestHeaders = request.Headers == null ? new ObservableCollection<Header>() : new ObservableCollection<Header>(request.Headers);
            _changesEnabled = true;
        }
    }
}
