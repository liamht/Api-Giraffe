using System;
using System.Collections.Generic;
using System.Linq;
using APIGiraffe.ApplicationServices.Requests.Commands.DeleteHeader;
using APIGiraffe.Data.Entities;
using APIGiraffe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace APIGiraffe.ApplicationServices.Test.Requests.Commands.DeleteHeader
{
    public class DeleteHeaderCommandTests
    {
        private readonly DeleteHeaderCommand _subject;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<DbSet<Header>> _headerSet;

        public DeleteHeaderCommandTests()
        {
            var headers = GetHeaders().AsQueryable();

            _headerSet = new Mock<DbSet<Header>>();
            _headerSet.As<IQueryable<Header>>().Setup(c => c.Provider).Returns(headers.Provider);
            _headerSet.As<IQueryable<Header>>().Setup(c => c.Expression).Returns(headers.Expression);
            _headerSet.As<IQueryable<Header>>().Setup(c => c.ElementType).Returns(headers.ElementType);
            _headerSet.As<IQueryable<Header>>().Setup(c => c.GetEnumerator()).Returns(headers.GetEnumerator());

            _headerSet.Setup(c => c.Find(1)).Returns(headers.Single(c => c.Id == 1));

            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(u => u.Headers).Returns(_headerSet.Object);

            _subject = new DeleteHeaderCommand(_uow.Object);
        }

        [Fact]
        public void Execute_FindsHeaderInDb()
        {
            _subject.Execute(1);

            _headerSet.Verify(c => c.Find(1), Times.Once);
        }

        [Fact]
        public void Execute_WhenHeaderCannotBeFound_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _subject.Execute(99));
        }

        [Fact]
        public void Execute_RemovesHeaderFromDbSet()
        {
            _subject.Execute(1);

            _headerSet.Verify(c => c.Remove(It.IsAny<Header>()), Times.Once);
        }

        [Fact]
        public void Execute_SavesChangesToDb()
        {
            _subject.Execute(1);

            _uow.Verify(c => c.SaveChanges(), Times.Once);
        }

        private IEnumerable<Header> GetHeaders()
        {
            return new List<Header>()
            {
                new Header() {Id = 1, Value = "Test 1"},
                new Header() {Id = 2, Value = "Test 2"},
                new Header() {Id = 3, Value = "Test 3"}
            };
        }
    }
}
