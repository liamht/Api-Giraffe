using System.Threading.Tasks;

namespace APIGiraffe.ApplicationServices.RequestGroups.Commands.AddNewRequestGroup
{
    public interface IAddNewRequestGroupCommand
    {
        void Execute(string name);
    }
}