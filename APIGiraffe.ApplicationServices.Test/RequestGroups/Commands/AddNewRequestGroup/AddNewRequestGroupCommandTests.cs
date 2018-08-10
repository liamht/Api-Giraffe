using System;
using APIGiraffe.ApplicationServices.RequestGroups.Commands.AddNewRequestGroup;
using APIGiraffe.Data.Entities;
using APIGiraffe.Data.Entities.Factory;
using APIGiraffe.Data.UnitOfWork;
using APIGiraffe.Domain.Factories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace APIGiraffe.ApplicationServices.Test.RequestGroups.Commands.AddNewRequestGroup
{
    public class AddNewRequestGroupCommandTests
    {
        private readonly AddNewRequestGroupCommand _subject;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IRequestGroupFactory> _factory;
        private readonly Mock<DbSet<RequestGroup>> _groupSet;
        private readonly Mock<IRequestGroupDataFactory> _dataFactory;

        public AddNewRequestGroupCommandTests()
        {
            _groupSet = new Mock<DbSet<RequestGroup>>();

            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(u => u.RequestGroups).Returns(_groupSet.Object);

            _factory = new Mock<IRequestGroupFactory>();
            _factory.Setup(c => c.Create(It.IsAny<string>())).Returns(new Domain.Entities.RequestGroup());


            _dataFactory = new Mock<IRequestGroupDataFactory>();
            _dataFactory.Setup(c => c.Create(It.IsAny<Domain.Entities.RequestGroup>())).Returns(new RequestGroup());

            _subject = new AddNewRequestGroupCommand(_uow.Object, _factory.Object, _dataFactory.Object);
        }

        [Fact]
        public void Execute_AddsRequestToGroup()
        {
            _subject.Execute("Test Request");
            _groupSet.Verify(c => c.Add(It.IsAny<RequestGroup>()), Times.Once);
        }

        [Fact]
        public void Execute_SavesChangesInDb()
        {
            _subject.Execute("Test Request");
            
            _uow.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Execute_WhenGroupNameIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _subject.Execute(null));
        }
    }
}
