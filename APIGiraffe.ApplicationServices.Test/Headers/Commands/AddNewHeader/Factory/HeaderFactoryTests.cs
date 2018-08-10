using APIGiraffe.Domain.Entities;
using APIGiraffe.Domain.Factories;
using Xunit;

namespace APIGiraffe.ApplicationServices.Test.Headers.Commands.AddNewHeader.Factory
{
    public class HeaderFactoryTests
    {
        private readonly HeaderFactory _subject;

        public HeaderFactoryTests()
        {
            _subject = new HeaderFactory();
        }

        [Fact]
        public void Create_CreatesNewHeader()
        {
            var result = _subject.Create("hello", "world");

            Assert.IsType<Header>(result);
        }

        [Fact]
        public void Create_SetsValues()
        {
            var result = _subject.Create("hello", "world");

            Assert.Equal("hello", result.Name);
            Assert.Equal("world", result.Value);
        }

        [Fact]
        public void Create_WhenNameIsNull_SetsNullValueInReturnObject()
        {
            var result = _subject.Create(null, "world");

            Assert.Null(result.Name);
            Assert.Equal("world", result.Value);
        }

        [Fact]
        public void Create_WhenValueIsNull_SetsNullValueInReturnObject()
        {
            var result = _subject.Create("hello", null);

            Assert.Equal("hello", result.Name);
            Assert.Null(result.Value);
        }
    }
}
