using System;
using System.Collections.Generic;
using System.Linq;
using APIGiraffe.ApplicationServices.Requests.Commands.DeleteRequest;
using APIGiraffe.Data.Entities;
using APIGiraffe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace APIGiraffe.ApplicationServices.Test.Requests.Commands.DeleteRequest
{
    public class DeleteRequestCommandTests
    {
        private readonly DeleteRequestCommand _subject;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<DbSet<Request>> _requestSet;

        public DeleteRequestCommandTests()
        {
            var requests = GetRequests().AsQueryable();

            _requestSet = new Mock<DbSet<Request>>();
            _requestSet.As<IQueryable<Request>>().Setup(c => c.Provider).Returns(requests.Provider);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.Expression).Returns(requests.Expression);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.ElementType).Returns(requests.ElementType);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.GetEnumerator()).Returns(requests.GetEnumerator());

            _requestSet.Setup(c => c.Find(1)).Returns(requests.Single(c => c.Id == 1));
            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(u => u.Requests).Returns(_requestSet.Object);

            _subject = new DeleteRequestCommand(_uow.Object);
        }

        [Fact]
        public void Execute_FindsInDb()
        {
            _subject.Execute(1);

            _requestSet.Verify(c => c.Find(1), Times.Once);
        }

        [Fact]
        public void Execute_WhenCannotBeFound_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _subject.Execute(99);
            });
        }

        [Fact]
        public void Execute_RemovesRequestFromDbSet()
        {
            _subject.Execute(1);

            _requestSet.Verify(c => c.Remove(It.IsAny<Request>()), Times.Once);
        }

        [Fact]
        public void Execute_SavesChangesToDb()
        {
            _subject.Execute(1);

            _uow.Verify(c => c.SaveChanges(), Times.Once);
        }

        private IEnumerable<Request> GetRequests()
        {
            return new List<Request>()
            {
                new Request() {Id = 1, Name = "Test 1"},
                new Request() {Id = 2, Name = "Test 2"},
                new Request() {Id = 3, Name = "Test 3"}
            };
        }
    }
}
