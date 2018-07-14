using APIGirrafe.Data.UnitOfWork;
using System.Threading.Tasks;

namespace APIGirrafe.ApplicationServices.Requests.Commands.DeleteRequestGroup
{
    public class DeleteRequestGroupCommand : IDeleteRequestGroupCommand
    {
        private readonly IUnitOfWork _uow;

        public DeleteRequestGroupCommand(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task ExecuteAsync(int requestGroupId)
        {
            var group = _uow.RequestGroups.FindAsync(requestGroupId);
            _uow.RequestGroups.Remove(await group);
        }
    }
}
