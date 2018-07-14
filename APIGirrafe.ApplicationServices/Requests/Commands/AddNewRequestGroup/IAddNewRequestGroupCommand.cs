using System.Threading.Tasks;

namespace APIGirrafe.ApplicationServices.Requests.Commands.AddNewRequestGroup
{
    public interface IAddNewRequestGroupCommand
    {
        Task ExecuteAsync(string name);
    }
}