using System.Threading.Tasks;

namespace APIGiraffe.ApplicationServices.RequestGroups.Commands.DeleteRequestGroup
{
    public interface IDeleteRequestGroupCommand
    {
        void Execute(int requestGroupId);
    }
}