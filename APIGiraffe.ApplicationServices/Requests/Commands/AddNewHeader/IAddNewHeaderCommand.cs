using System.Threading.Tasks;

namespace APIGiraffe.ApplicationServices.Requests.Commands.AddNewHeader
{
    public interface IAddNewHeaderCommand
    {
        void Execute(int requestId, string name, string value);
    }
}