using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APIGiraffe.ApplicationServices.RequestGroups.Queries.GetRequestGroups;
using APIGiraffe.Data.Entities;
using APIGiraffe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using RequestGroup = APIGiraffe.ApplicationServices.RequestGroups.Queries.GetRequestGroups.RequestGroup;

namespace APIGiraffe.ApplicationServices.Test.RequestGroups.Queries.GetRequestGroups
{
    public class GetRequestGroupsQueryTests
    {
        private GetRequestGroupsQuery _subject;
        private IQueryable<Data.Entities.RequestGroup> _requestGroups;
        private Mock<DbSet<Data.Entities.RequestGroup>> _groupSet;
        private Mock<IUnitOfWork> _uow;

        public GetRequestGroupsQueryTests()
        {
            _requestGroups = GetMockRequestGroupDbObjects().AsQueryable();

            _groupSet = new Mock<DbSet<Data.Entities.RequestGroup>>();
            _groupSet.As<IQueryable<Data.Entities.RequestGroup>>().Setup(c => c.Provider).Returns(_requestGroups.Provider);
            _groupSet.As<IQueryable<Data.Entities.RequestGroup>>().Setup(c => c.Expression).Returns(_requestGroups.Expression);
            _groupSet.As<IQueryable<Data.Entities.RequestGroup>>().Setup(c => c.ElementType).Returns(_requestGroups.ElementType);
            _groupSet.As<IQueryable<Data.Entities.RequestGroup>>().Setup(c => c.GetEnumerator()).Returns(_requestGroups.GetEnumerator());

            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(c => c.RequestGroups).Returns(_groupSet.Object);

            _subject = new GetRequestGroupsQuery(_uow.Object);
        }

        [Fact]
        public void Execute_ReturnsCorrectType()
        {
            var result = _subject.Execute();

            Assert.IsType<List<RequestGroup>>(result);
        }

        [Fact]
        public void Execute_MapsToDatabaseObjectCorrectly()
        {
            var result = _subject.Execute();

            var firstResult = result.First();
            var firstGroup = _requestGroups.First();

            Assert.Equal(firstGroup.Name, firstResult.Name);
            Assert.Equal(firstGroup.Id, firstResult.Id);
        }

        [Fact]
        public void Execute_ReturnsRequestsInsideGroup()
        {
            var result = _subject.Execute();

            var firstResult = result.First();

            Assert.NotEmpty(firstResult.Requests);
        }

        [Fact]
        public void Execute_MapsRequestsCorrectly()
        {
            var result = _subject.Execute();

            var actual = result.First().Requests.First();
            var expected = result.First().Requests.First();

            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Id, actual.Id);
        }

        [Fact]
        public void Execute_ReturnsAllObjectsInDatabase()
        {
            var result = _subject.Execute();

            Assert.Equal(_requestGroups.Count(), result.Count);
        }


        [Fact]
        public void Execute_WhenNoGroupsExist_ReturnsEmptyList()
        {
            _requestGroups = new List<Data.Entities.RequestGroup>().AsQueryable();
            _groupSet.As<IQueryable<Data.Entities.RequestGroup>>().Setup(c => c.Provider).Returns(_requestGroups.Provider);
            _groupSet.As<IQueryable<Data.Entities.RequestGroup>>().Setup(c => c.Expression).Returns(_requestGroups.Expression);
            _groupSet.As<IQueryable<Data.Entities.RequestGroup>>().Setup(c => c.ElementType).Returns(_requestGroups.ElementType);
            _groupSet.As<IQueryable<Data.Entities.RequestGroup>>().Setup(c => c.GetEnumerator()).Returns(_requestGroups.GetEnumerator());

            _uow.Setup(c => c.RequestGroups).Returns(_groupSet.Object);

            var result = _subject.Execute();

            Assert.Empty(result);
            Assert.NotNull(result);
        }

        private IEnumerable<Data.Entities.RequestGroup> GetMockRequestGroupDbObjects()
        {
            return new List<Data.Entities.RequestGroup>()
            {
                new Data.Entities.RequestGroup()
                {
                    Name = "Test",
                    Id = 1,
                    Requests = new List<Request>()
                    {
                        new Request()
                        {
                            Name = "test request",
                            Id = 1,
                            Url = "testurl"
                        }
                    }
                }
            };
        }
    }
}
