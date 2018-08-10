using APIGiraffe.Domain.Entities;
using APIGiraffe.Domain.Factories;
using Xunit;

namespace APIGiraffe.ApplicationServices.Test.RequestGroups.Commands.AddNewRequestGroup.Factory
{
    public class RequestGroupFactoryTests
    {
        private readonly RequestGroupFactory _subject;

        public RequestGroupFactoryTests()
        {
            _subject = new RequestGroupFactory();
        }

        [Fact]
        public void Create_CreatesNewHeader()
        {
            var result = _subject.Create("test");

            Assert.IsType<RequestGroup>(result);
        }

        [Fact]
        public void Create_SetsValueOfRequestName()
        {
            var result = _subject.Create("hello");

            Assert.Equal("hello", result.Name);
        }

        [Fact]
        public void Create_AddsNoRequestsToTheGroup()
        {
            var result = _subject.Create("hello");
            
            Assert.Null(result.Requests);
        }

        [Fact]
        public void Create_WhenNameIsNull_SetsNullValueInReturnObject()
        {
            var result = _subject.Create(null);

            Assert.Null(result.Name);
        }
    }
}
