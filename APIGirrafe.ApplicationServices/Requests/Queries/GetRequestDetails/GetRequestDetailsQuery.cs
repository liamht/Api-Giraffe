﻿using APIGirrafe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace APIGirrafe.ApplicationServices.Requests.Queries.GetRequestDetails
{
    public class GetRequestDetailsQuery : IGetRequestDetailsQuery
    {
        private readonly IUnitOfWork _uow;

        public GetRequestDetailsQuery(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<RequestDetails> ExecuteAsync(int requestId)
        {
            var request = await _uow.Requests
                    .Include(c => c.Headers)
                    .SingleAsync(c => c.Id == requestId);

            var headers = request.Headers.Select(header => new Header()
            {
                Id = header.Id,
                Name = header.Name,
                Value = header.Value
            }).ToList();

            return new RequestDetails()
            {
                Id = request.Id,
                Headers = headers,
                RequestName = request.Name,
                Url = request.Url
            };
        }
    }
}