using System.Threading.Tasks;

namespace APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequestGroup
{
    public interface IAddNewRequestGroupCommand
    {
        Task ExecuteAsync(string name);
    }
}