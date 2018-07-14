using System.Threading.Tasks;

namespace APIGirrafe.ApplicationServices.Requests.Commands.UpdateRequest
{
    public interface IUpdateRequestCommand
    {
        void Execute(int id, string url);
    }
}