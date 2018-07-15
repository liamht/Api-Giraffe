using APIGiraffe.Domain;

namespace APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequestGroup.Factory
{
    public interface IRequestGroupFactory
    {
        RequestGroup Create(string name);
    }
}