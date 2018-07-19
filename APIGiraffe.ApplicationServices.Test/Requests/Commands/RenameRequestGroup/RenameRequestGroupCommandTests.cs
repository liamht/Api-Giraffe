using System;
using System.Collections.Generic;
using System.Linq;
using APIGiraffe.ApplicationServices.Requests.Commands.RenameRequestGroup;
using APIGiraffe.Data.Entities;
using APIGiraffe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace APIGiraffe.ApplicationServices.Test.Requests.Commands.RenameRequestGroup
{
    public class RenameRequestGroupCommandTests
    {
        private readonly RenameRequestGroupCommand _subject;

        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<DbSet<RequestGroup>> _requestGroupSet;

        public RenameRequestGroupCommandTests()
        {
            var requestGroups = GetRequestGroups().AsQueryable();

            _requestGroupSet = new Mock<DbSet<RequestGroup>>();
            _requestGroupSet.As<IQueryable<RequestGroup>>().Setup(c => c.Provider).Returns(requestGroups.Provider);
            _requestGroupSet.As<IQueryable<RequestGroup>>().Setup(c => c.Expression).Returns(requestGroups.Expression);
            _requestGroupSet.As<IQueryable<RequestGroup>>().Setup(c => c.ElementType).Returns(requestGroups.ElementType);
            _requestGroupSet.As<IQueryable<RequestGroup>>().Setup(c => c.GetEnumerator()).Returns(requestGroups.GetEnumerator());

            _requestGroupSet.Setup(c => c.Find(1)).Returns(requestGroups.Single(c => c.Id == 1));
            _requestGroupSet.Setup(c => c.Find(2)).Returns(requestGroups.Single(c => c.Id == 2));

            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(u => u.RequestGroups).Returns(_requestGroupSet.Object);

            _subject = new RenameRequestGroupCommand(_uow.Object);
        }

        [Fact]
        public void Execute_UpdatesRecord()
        {
            const string nameToSet = "New Name";
            _subject.Execute(1, nameToSet);

            var unitOfWorkObject = _uow.Object.RequestGroups.Find(1);

            Assert.Equal(nameToSet, unitOfWorkObject.Name);
        }

        [Fact]
        public void Execute_CommitsChangesToUnitOfWork()
        {
            const string nameToSet = "New Name";
            _subject.Execute(1, nameToSet);

            _requestGroupSet.Verify(c => c.Update(It.IsAny<RequestGroup>()), Times.Once);
            _uow.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Execute_OnlyUpdatesMatchingElement()
        {
            var originalValue = _uow.Object.RequestGroups.Find(2).Name;

            const string nameToSet = "New Name";
            _subject.Execute(1, nameToSet);

            var postUpdateValue = _uow.Object.RequestGroups.Find(2).Name;

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
