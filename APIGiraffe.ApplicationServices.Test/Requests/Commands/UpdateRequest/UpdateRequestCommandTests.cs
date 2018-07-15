using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APIGiraffe.ApplicationServices.Requests.Commands.UpdateRequest;
using APIGiraffe.Data.Entities;
using APIGiraffe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace APIGiraffe.ApplicationServices.Test.Requests.Commands.UpdateRequest
{
    public class UpdateRequestCommandTests
    {
        private readonly IQueryable<Request> _requests;
        private readonly Mock<DbSet<Request>> _requestSet;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly UpdateRequestCommand _subject;

        public UpdateRequestCommandTests()
        {
            _requests = GetRequests().AsQueryable();

            _requestSet = new Mock<DbSet<Request>>();
            _requestSet.As<IQueryable<Request>>().Setup(c => c.Provider).Returns(_requests.Provider);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.Expression).Returns(_requests.Expression);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.ElementType).Returns(_requests.ElementType);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.GetEnumerator()).Returns(_requests.GetEnumerator());

            _requestSet.Setup(c => c.Find(1)).Returns(_requests.Single(c => c.Id == 1));

            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(c => c.Requests).Returns(_requestSet.Object);

            _subject = new UpdateRequestCommand(_uow.Object);
        }

        [Fact]
        public void Execute_FindsRequestById()
        {
            _subject.Execute(1, "Updated URL");

            _requestSet.Verify(c => c.Find(1), Times.Once);
        }

        [Fact]
        public void Execute_UpdatesUrlOfObject()
        {
            var updatedUrl = "Updated URL";
            _subject.Execute(1, updatedUrl);

            var requestUpdated = _requests.Single(c => c.Id == 1);

            Assert.Equal(updatedUrl, requestUpdated.Url);
        }

        [Fact]
        public void Execute_DoesNotUpdateNonMatchingRequests()
        {
            var updatedUrl = "Updated URL";
            _subject.Execute(1, updatedUrl);

            var requestNotUpdated = _requests.Single(c => c.Id == 2);

            Assert.NotEqual(updatedUrl, requestNotUpdated.Url);
        }

        [Fact]
        public void Execute_UpdatesUnderlyingDbObject()
        {
            _subject.Execute(1, "test");

            _requestSet.Verify(c => c.Update(It.IsAny<Request>()), Times.Once());
        }

        [Fact]
        public void Execute_SavesChangesToDb()
        {
            _subject.Execute(1, "test");

            _uow.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Execute_WhenMatchingRequestCannotBeFound_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _subject.Execute(99, "test");
            });
        }

        [Fact]
        public void Execute_WhenNewUrlIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _subject.Execute(1, null);
            });
        }

        private List<Request> GetRequests()
        {
            return new List<Request>()
            {
                new Request() {Id = 1, Url = "Test Url"},
                new Request() {Id = 2, Url = "Test Url"}
            };
        }
    }
}
