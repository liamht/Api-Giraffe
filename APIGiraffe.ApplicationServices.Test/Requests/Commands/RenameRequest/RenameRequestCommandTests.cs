using APIGiraffe.ApplicationServices.Requests.Commands.RenameRequest;
using APIGiraffe.Data.Entities;
using APIGiraffe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace APIGiraffe.ApplicationServices.Test.Requests.Commands.RenameRequest
{
    public class RenameRequestCommandTests
    {
        private readonly RenameRequestCommand _subject;

        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<DbSet<Request>> _requestSet;

        public RenameRequestCommandTests()
        {
            var requests = GetRequestGroups().AsQueryable();

            _requestSet = new Mock<DbSet<Request>>();
            _requestSet.As<IQueryable<Request>>().Setup(c => c.Provider).Returns(requests.Provider);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.Expression).Returns(requests.Expression);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.ElementType).Returns(requests.ElementType);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.GetEnumerator()).Returns(requests.GetEnumerator());

            _requestSet.Setup(c => c.Find(1)).Returns(requests.Single(c => c.Id == 1));
            _requestSet.Setup(c => c.Find(2)).Returns(requests.Single(c => c.Id == 2));

            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(u => u.Requests).Returns(_requestSet.Object);

            _subject = new RenameRequestCommand(_uow.Object);
        }

        [Fact]
        public void Execute_UpdatesRecord()
        {
            const string nameToSet = "New Name";
            _subject.Execute(1, nameToSet);

            var unitOfWorkObject = _uow.Object.Requests.Find(1);

            Assert.Equal(nameToSet, unitOfWorkObject.Name);
        }

        [Fact]
        public void Execute_CommitsChangesToUnitOfWork()
        {
            const string nameToSet = "New Name";
            _subject.Execute(1, nameToSet);

            _requestSet.Verify(c => c.Update(It.IsAny<Request>()), Times.Once);
            _uow.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Execute_OnlyUpdatesMatchingElement()
        {
            var originalValue = _uow.Object.Requests.Find(2).Name;

            const string nameToSet = "New Name";
            _subject.Execute(1, nameToSet);

            var postUpdateValue = _uow.Object.Requests.Find(2).Name;

            Assert.Equal(originalValue, postUpdateValue);
        }

        [Fact]
        public void Execute_WhenNoMatchingElementIsFound_ThrowsOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _subject.Execute(99, "Anything"));
        }

        [Fact]
        public void Execute_WhenNewNameToSetIsNull_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _subject.Execute(1, null));
        }

        [Fact]
        public void Execute_WhenNameIsEmpty_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _subject.Execute(1, string.Empty));
        }

        [Fact]
        public void Execute_WhenNameIsWhitespace_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _subject.Execute(1, "  "));
        }

        private IEnumerable<Request> GetRequestGroups()
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
