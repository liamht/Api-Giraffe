using System.Collections.Generic;

namespace APIGirrafe.Domain.Services
{
    public interface IRequestService
    {
        List<SoapRequest> FetchFromDatabase();
        SoapRequest GetById(int id);
        void UpdateRequest(SoapRequest request);
        void CreateRequest(SoapRequest request);
    }
}