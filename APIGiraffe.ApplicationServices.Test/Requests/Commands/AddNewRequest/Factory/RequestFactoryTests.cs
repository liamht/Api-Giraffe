using APIGiraffe.Domain;
using APIGiraffe.Domain.Entities;
using APIGiraffe.Domain.Factories;
using Xunit;

namespace APIGiraffe.ApplicationServices.Test.Requests.Commands.AddNewRequest.Factory
{
    public class RequestFactoryTests
    {
        private readonly RequestFactory _subject;

        public RequestFactoryTests()
        {
            _subject = new RequestFactory();
        }

        [Fact]
        public void Create_CreatesNewRequest()
        {
            var result = _subject.Create(1, "test");

            Assert.IsType<Request>(result);
        }

        [Fact]
        public void Create_SetsValueOfRequestName()
        {
            var result = _subject.Create(1, "hello");

            Assert.Equal("hello", result.RequestName);
        }

        [Fact]
        public void Create_LeavesPropertiesOtherThanNameAndGroupIdAsNull()
        {
            var result = _subject.Create(1, "hello");

            Assert.Null(result.Url);
            Assert.Empty(result.Headers);
        }

        [Fact]
        public void Create_WhenNameIsNull_SetsNullValueInReturnObject()
        {
            var result = _subject.Create(1, null);

            Assert.Null(result.RequestName);
        }
    }
}
