using APIGirrafe.Domain;

namespace APIGirrafe.ApplicationServices.Requests.Commands.AddNewRequestGroup.Factory
{
    public interface IRequestGroupFactory
    {
        RequestGroup Create(string name);
    }
}