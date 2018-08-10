using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequest;
using APIGiraffe.Data.Entities;
using APIGiraffe.Data.UnitOfWork;
using APIGiraffe.Domain.Factories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace APIGiraffe.ApplicationServices.Test.Requests.Commands.AddNewRequest
{
    public class AddNewRequestCommandTests
    {
        private readonly AddNewRequestCommand _subject;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IRequestFactory> _factory;
        private readonly RequestGroup _group;
        private readonly Mock<DbSet<RequestGroup>> _groupSet;

        public AddNewRequestCommandTests()
        {
            _group = GetRequestGroup();

            var groups = new List<RequestGroup>() { _group }.AsQueryable();

            _groupSet = new Mock<DbSet<RequestGroup>>();
            _groupSet.As<IQueryable<RequestGroup>>().Setup(c => c.Provider).Returns(groups.Provider);
            _groupSet.As<IQueryable<RequestGroup>>().Setup(c => c.Expression).Returns(groups.Expression);
            _groupSet.As<IQueryable<RequestGroup>>().Setup(c => c.ElementType).Returns(groups.ElementType);
            _groupSet.As<IQueryable<RequestGroup>>().Setup(c => c.GetEnumerator()).Returns(groups.GetEnumerator);

            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(u => u.RequestGroups).Returns(_groupSet.Object);

            _factory = new Mock<IRequestFactory>();
            _factory.Setup(c => c.Create(It.IsAny<int>(), It.IsAny<string>())).Returns(new Domain.Entities.Request());

            _subject = new AddNewRequestCommand(_uow.Object, _factory.Object);
        }

        [Fact]
        public void Execute_AddsRequestToGroup()
        {
            Assert.Empty(_group.Requests);

            _subject.Execute(1, "Test Request");

            Assert.NotEmpty(_group.Requests);
        }

        [Fact]
        public void Execute_SavesChangesToRequestInDb()
        {
            _subject.Execute(1, "Test Request");

            _groupSet.Verify(c => c.Update(It.IsAny<RequestGroup>()), Times.Once);
            _uow.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Execute_GeneratesObjectToAddToDbUsingFactory()
        {
            _subject.Execute(1, "Test Request");

            _factory.Verify(c => c.Create(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Execute_WhenGroupToAddRequestToCannotBeFound_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => _subject.Execute(99, "Test Request"));
        }

        [Fact]
        public void Execute_WhenGroupNameIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _subject.Execute(1, null));
        }

        private static RequestGroup GetRequestGroup()
        {
            return new RequestGroup()
            {
                Id = 1,
                Name = "Test request group",
                Requests = new List<Request>()
            };
        }
    }
}
