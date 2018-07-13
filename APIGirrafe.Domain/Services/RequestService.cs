using System;
using System.Collections.Generic;
using System.Linq;
using APIGirrafe.Data.Entities;
using APIGirrafe.Data.Repository;

namespace APIGirrafe.Domain.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRepository<Request> _repo;
        public RequestService(IRepository<Request> repo)
        {
            _repo = repo;
        }

        public List<SoapRequest> FetchFromDatabase()
        {
            var soapRequests = _repo.Get().Select(c => SoapRequest.FromDatabaseEntity(c)).ToList();
            return soapRequests;
        }

        public SoapRequest GetById(int id)
        {
            var entity = _repo.Get().Single(c => c.Id == id);
            return SoapRequest.FromDatabaseEntity(entity);
        }

        public void CreateRequest(SoapRequest request)
        {
            _repo.Add(request.ToDatabaseEntity());
        }

        public void UpdateRequest(SoapRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException("request cannot be null");
            }

            if (request.Id == 0)
            {
                throw new ArgumentException("Request ID not set");
            }

            _repo.Update(request.ToDatabaseEntity());
        }
    }
}
