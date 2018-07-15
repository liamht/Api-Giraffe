using System.Threading.Tasks;

namespace APIGirrafe.ApplicationServices.Requests.Commands.DeleteRequestGroup
{
    public interface IDeleteRequestGroupCommand
    {
        void Execute(int requestGroupId);
    }
}