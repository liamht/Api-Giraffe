using System;
using System.Collections.Generic;
using System.Linq;
using APIGiraffe.ApplicationServices.RequestGroups.Commands.DeleteRequestGroup;
using APIGiraffe.Data.Entities;
using APIGiraffe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace APIGiraffe.ApplicationServices.Test.Requests.Commands.DeleteRequestGroup
{
    public class DeleteRequestGroupCommandTests
    {
        private readonly DeleteRequestGroupCommand _subject;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<DbSet<RequestGroup>> _requestGroupSet;
        private readonly Mock<DbSet<Request>> _requestSet;

        public DeleteRequestGroupCommandTests()
        {
            var requestGroups = GetRequestGroups().AsQueryable();

            _requestGroupSet = new Mock<DbSet<RequestGroup>>();
            _requestGroupSet.As<IQueryable<RequestGroup>>().Setup(c => c.Provider).Returns(requestGroups.Provider);
            _requestGroupSet.As<IQueryable<RequestGroup>>().Setup(c => c.Expression).Returns(requestGroups.Expression);
            _requestGroupSet.As<IQueryable<RequestGroup>>().Setup(c => c.ElementType).Returns(requestGroups.ElementType);
            _requestGroupSet.As<IQueryable<RequestGroup>>().Setup(c => c.GetEnumerator()).Returns(requestGroups.GetEnumerator());

            _requestGroupSet.Setup(c => c.Find(1)).Returns(requestGroups.Single(c => c.Id == 1));
            
            var requests = new List<Request>().AsQueryable();

            _requestSet = new Mock<DbSet<Request>>();
            _requestSet.As<IQueryable<Request>>().Setup(c => c.Provider).Returns(requests.Provider);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.Expression).Returns(requests.Expression);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.ElementType).Returns(requests.ElementType);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.GetEnumerator()).Returns(requests.GetEnumerator());

            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(u => u.RequestGroups).Returns(_requestGroupSet.Object);
            _uow.Setup(u => u.Requests).Returns(_requestSet.Object);

            _subject = new DeleteRequestGroupCommand(_uow.Object);
        }

        [Fact]
        public void Execute_FindsGroupInDb()
        {
            _subject.Execute(1);

            _requestGroupSet.Verify(c => c.Find(1), Times.Once);
        }

        [Fact]
        public void Execute_WhenGroupCannotBeFound_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _subject.Execute(99);
            });
        }

        [Fact]
        public void Execute_RemovesRequestGroupFromDbSet()
        {
            _subject.Execute(1);

            _requestGroupSet.Verify(c => c.Remove(It.IsAny<RequestGroup>()), Times.Once);
        }

        [Fact]
        public void Execute_RemovesRequestsForMatchingGroupFromDbSet()
        {
            _subject.Execute(1);
            
            _requestSet.Verify(c => c.RemoveRange(It.IsAny<IQueryable<Request>>()), Times.Once);
        }

        [Fact]
        public void Execute_SavesChangesToDb()
        {
            _subject.Execute(1);

            _uow.Verify(c => c.SaveChanges(), Times.Once);
        }

        private IEnumerable<RequestGroup> GetRequestGroups()
        {
            return new List<RequestGroup>()
            {
                new RequestGroup() {Id = 1, Name = "Test 1"},
                new RequestGroup() {Id = 2, Name = "Test 2"},
                new RequestGroup() {Id = 3, Name = "Test 3"}
            };
        }
    }
}
