using APIGiraffe.ApplicationServices.Requests.Commands.UpdateHeader;
using APIGiraffe.Data.UnitOfWork;
using APIGiraffe.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace APIGiraffe.ApplicationServices.Test.Requests.Commands.UpdateHeader
{
    public class UpdateHeaderCommandTests
    {
        private readonly IQueryable<Header> _headers;
        private readonly Mock<DbSet<Header>> _headerSet;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly UpdateHeaderCommand _subject;

        public UpdateHeaderCommandTests()
        {
            _headers = GetHeaders().AsQueryable();

            _headerSet = new Mock<DbSet<Header>>();
            _headerSet.As<IQueryable<Header>>().Setup(c => c.Provider).Returns(_headers.Provider);
            _headerSet.As<IQueryable<Header>>().Setup(c => c.Expression).Returns(_headers.Expression);
            _headerSet.As<IQueryable<Header>>().Setup(c => c.ElementType).Returns(_headers.ElementType);
            _headerSet.As<IQueryable<Header>>().Setup(c => c.GetEnumerator()).Returns(_headers.GetEnumerator());

            _headerSet.Setup(c => c.Find(1)).Returns(_headers.Single(c => c.Id == 1));

            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(c => c.Headers).Returns(_headerSet.Object);

            _subject = new UpdateHeaderCommand(_uow.Object);
        }

        [Fact]
        public void Execute_FindsRequestById()
        {
            _subject.Execute(1, "Updated Name", "Updated Value");

            _headerSet.Verify(c => c.Find(1), Times.Once);
        }

        [Fact]
        public void Execute_UpdatesNameOfObject()
        {
            var updatedName = "Updated Name";
            _subject.Execute(1, updatedName, "Updated Value");

            var requestUpdated = _headers.Single(c => c.Id == 1);

            Assert.Equal(updatedName, requestUpdated.Name);
        }


        [Fact]
        public void Execute_UpdatesValueOfObject()
        {
            var updatedValue = "Updated Value";
            _subject.Execute(1, "Updated Name", updatedValue);

            var requestUpdated = _headers.Single(c => c.Id == 1);

            Assert.Equal(updatedValue, requestUpdated.Value);
        }

        [Fact]
        public void Execute_DoesNotUpdateNonMatchingRequests()
        {
            var updatedName = "Updated Name";
            _subject.Execute(1, updatedName, "Updated Value");

            var requestNotUpdated = _headers.Single(c => c.Id == 2);

            Assert.NotEqual(updatedName, requestNotUpdated.Name);
        }

        [Fact]
        public void Execute_UpdatesUnderlyingDbObject()
        {
            _subject.Execute(1, "Updated Name", "Updated Value");

            _headerSet.Verify(c => c.Update(It.IsAny<Header>()), Times.Once());
        }

        [Fact]
        public void Execute_SavesChangesToDb()
        {
            _subject.Execute(1, "Updated Name", "Updated Value");

            _uow.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact]
        public void Execute_WhenMatchingHeaderCannotBeFound_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _subject.Execute(99, "Updated Name", "Updated Value");
            });
        }

        [Fact]
        public void Execute_WhenNameIsNull_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _subject.Execute(1, null, "Updated Value");
            });
        }

        [Fact]
        public void Execute_WhenValueIsNull_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _subject.Execute(1, "Updated Name", null);
            });
        }

        private List<Header> GetHeaders()
        {
            return new List<Header>()
            {
                new Header() { Id = 1, Name = "Header 1", Value = "Header 1"},
                new Header() { Id = 2, Name = "Header 2", Value = "Header 2"},
                new Header() { Id = 3, Name = "Header 3", Value = "Header 3"},
                new Header() { Id = 4, Name = "Header 4", Value = "Header 4"}
            };
        }
    }
}
