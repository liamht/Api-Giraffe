using System.Threading.Tasks;

namespace APIGiraffe.ApplicationServices.Requests.Commands.DeleteRequestGroup
{
    public interface IDeleteRequestGroupCommand
    {
        void Execute(int requestGroupId);
    }
}