
using APIGiraffe.ApplicationServices.Requests.Commands.RenameRequestGroup;
using APIGiraffe.Data.UnitOfWork;
using Moq;

namespace APIGiraffe.ApplicationServices.Test.Requests.Commands.RenameRequestGroup
{
    public class RenameRequestGroupCommandTests
    {
        private RenameRequestGroupCommand _subject;

        private Mock<IUnitOfWork> _uow;

        public RenameRequestGroupCommandTests()
        {
            _uow = new Mock<IUnitOfWork>();

            _subject = new RenameRequestGroupCommand(_uow.Object);
        }
    }
}
