using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using APIGiraffe.ApplicationServices.Requests.Queries.GetRequestDetails;
using APIGiraffe.Data.Entities;
using APIGiraffe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Xunit;

namespace APIGiraffe.ApplicationServices.Test.Requests.Queries.GetRequestDetails
{
    public class GetRequestDetailsQueryTests
    {
        private readonly GetRequestDetailsQuery _subject;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<DbSet<Request>> _requestSet;

        public GetRequestDetailsQueryTests()
        {
            var requests = GetMockRequestDbObjects().AsQueryable();
            
            _requestSet = new Mock<DbSet<Request>>();
            _requestSet.As<IQueryable<Request>>().Setup(c => c.Provider).Returns(requests.Provider);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.Expression).Returns(requests.Expression);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.ElementType).Returns(requests.ElementType);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.GetEnumerator()).Returns(requests.GetEnumerator);

            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(c => c.Requests).Returns(_requestSet.Object);

            _subject = new GetRequestDetailsQuery(_uow.Object);
        }
        
        [Fact]
        public void Execute_ReturnsCorrectType()
        {
            var result = _subject.Execute(1);

            Assert.IsType<RequestDetails>(result);
        }

        [Fact]
        public void Execute_ReturnsCorrectItem()
        {
            var result = _subject.Execute(1);

            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void Execute_MapsRequestValuesCorrectly()
        {
            var result = _subject.Execute(1);

            var dbObject = _requestSet.Object.Single(c => c.Id == 1);

            Assert.Equal(dbObject.Id, result.Id);
            Assert.Equal(dbObject.Url, result.Url);
            Assert.Equal(dbObject.Name, result.RequestName);
        }

        [Fact]
        public void Execute_ReturnsHeaders()
        {
            var result = _subject.Execute(1);

            Assert.NotEmpty(result.Headers);
        }

        [Fact]
        public void Execute_MapsHeadersCorrectly()
        {
            var result = _subject.Execute(1).Headers.First();

            var expected = _requestSet.Object.Single(c => c.Id == 1).Headers.First();

            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Name, result.Name);
            Assert.Equal(expected.Value, result.Value);
        }

        [Fact]
        public void Execute_WhenRequestCannotBeFound_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => _subject.Execute(99));
        }

        private IEnumerable<Request> GetMockRequestDbObjects()
        {
            return new List<Request>()
            {
                new Request()
                {
                    Id = 1,
                    Name = "Request in DB",
                    GroupId = 4,
                    Url = "testurl",
                    Headers = new List<Data.Entities.Header>()
                    {
                        new Data.Entities.Header() {Id = 1, Name = "Test Header", Value = "test"}
                    }
                }
            };
        }
    }
}
