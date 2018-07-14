﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using APIGirrafe.Domain;
using APIGirrafe.Domain.Services;
using APIGirrafe.UI.Navigation;
using APIGirrafe.UI.Views;

namespace APIGirrafe.UI.ViewModels
{
    public class CurrentRequestViewModel : BasePageViewModel
    {
        public override string Title => this.Name;

        private readonly IRequestService _service;

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
                    SaveChanges();
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
                if (hasChanged)
                {
                    SaveChanges();
                }
                NotifyPropertyChanged(nameof(Name));
            }
        }

        #endregion

        public ObservableCollection<Header> RequestHeaders { get; set; }

        public CurrentRequestViewModel(IRequestService service, INavigationHelper nav)
        {
            Response = "The response from the server will show here when a request is sent";
            RequestHeaders = new ObservableCollection<Header>();
            GetResponseCommand = new ActionCommand(async () => await GetResponse());
            AddHeaderCommand = new ActionCommand(() => ShowAddHeaderModal());
            _service = service;
            _navigationHelper = nav;
        }

        private void ShowAddHeaderModal()
        {
            var vm = new NewHeaderViewModel(_navigationHelper, _service, _requestId);
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
            var httprequest = new SoapRequest()
            {
                Url = this.Url,
            };

            foreach (var header in RequestHeaders)
            {
                httprequest.AddHeader(header);
            }

            Response = await httprequest.GetResponse();
        }

        public void LoadValues(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Cannot fetch ID from database");
            }

            var request = _service.GetById(id);

            _requestId = request.Id;
            Url = request.Url;

            RequestHeaders = request.Headers == null ? new ObservableCollection<Header>() : new ObservableCollection<Header>(request.Headers);
            _changesEnabled = true;
        }

        public void SaveChanges()
        {

            var request = new SoapRequest()
            {
                Id = _requestId,
                Url = Url,
                RequestName = Name
            };

            foreach (var header in RequestHeaders)
            {
                request.AddHeader(header);
            }

            _service.UpdateRequest(request);
        }
    }
}