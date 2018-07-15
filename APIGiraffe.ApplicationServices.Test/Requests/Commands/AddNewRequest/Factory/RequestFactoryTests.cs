using APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequest.Factory;
using APIGiraffe.Domain;
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
        public void Create_CreatesNewHeader()
        {
            var result = _subject.Create("test");

            Assert.IsType<Request>(result);
        }

        [Fact]
        public void Create_SetsValueOfRequestName()
        {
            var result = _subject.Create("hello");

            Assert.Equal("hello", result.RequestName);
        }

        [Fact]
        public void Create_LeavesPropertiesOtherThanNameAsNull()
        {
            var result = _subject.Create("hello");

            Assert.Null(result.Url);
            Assert.Empty(result.Headers);
        }

        [Fact]
        public void Create_WhenNameIsNull_SetsNullValueInReturnObject()
        {
            var result = _subject.Create(null);

            Assert.Null(result.RequestName);
        }
    }
}
