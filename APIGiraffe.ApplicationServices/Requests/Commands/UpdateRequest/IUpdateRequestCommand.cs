using System.Threading.Tasks;

namespace APIGiraffe.ApplicationServices.Requests.Commands.UpdateRequest
{
    public interface IUpdateRequestCommand
    {
        void Execute(int id, string url);
    }
}