using System;
using System.Collections.Generic;
using System.Linq;
using APIGiraffe.ApplicationServices.Headers.Commands.AddNewHeader;
using APIGiraffe.ApplicationServices.Headers.Commands.AddNewHeader.Factory;
using APIGiraffe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Request = APIGiraffe.Data.Entities.Request;

namespace APIGiraffe.ApplicationServices.Test.Headers.Commands.AddNewHeader
{
    public class AddNewHeaderCommandTests
    {
        private readonly AddNewHeaderCommand _subject;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IHeaderFactory> _factory;
        private readonly Request _request;
        private readonly Mock<DbSet<Request>> _requestSet;

        public AddNewHeaderCommandTests()
        {
            _request = GetRequestObject();

            var requests = new List<Request>() {_request}.AsQueryable();

            _requestSet = new Mock<DbSet<Request>>();
            _requestSet.As<IQueryable<Request>>().Setup(c => c.Provider).Returns(requests.Provider);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.Expression).Returns(requests.Expression);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.ElementType).Returns(requests.ElementType);
            _requestSet.As<IQueryable<Request>>().Setup(c => c.GetEnumerator()).Returns(requests.GetEnumerator);

            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(u => u.Requests).Returns(_requestSet.Object);
            
            _factory = new Mock<IHeaderFactory>();
            _factory.Setup(c => c.Create(It.IsAny<string>(), It.IsAny<string>())).Returns(new Domain.Header());

            _subject = new AddNewHeaderCommand(_uow.Object, _factory.Object);
        }

        [Fact]
        public void Execute_CreatesObjectWithFactory()
        {
            _subject.Execute(1, "2", "test");

            _factory.Verify(c => c.Create(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public void Execute_AddsHeaderToCurrentRequest()
        {
            Assert.Empty(_request.Headers);

            _subject.Execute(1, "2", "test");

            Assert.NotEmpty(_request.Headers);
        }

        [Fact]
        public void Execute_UpdatesRequestInDb()
        {
            _subject.Execute(1, "2", "test");

            _requestSet.Verify(c => c.Update(It.IsAny<Request>()), Times.Once);
            _uow.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Execute_WhenRequestCannotBeFound_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() =>  _subject.Execute(99, "2", "test"));
        }

        [Fact]
        public void Execute_WhenHeaderNameIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _subject.Execute(1, null, "test"));
        }

        [Fact]
        public void Execute_WhenHeaderValueIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _subject.Execute(1, "test", null));
        }
        
        private static Request GetRequestObject()
        {
            return new Request()
            {
                Id = 1,
                Name = "Test Name",
                Headers = new List<Data.Entities.Header>(),
                GroupId = 3,
                Url = "test url"
            };
        }
    }
}
