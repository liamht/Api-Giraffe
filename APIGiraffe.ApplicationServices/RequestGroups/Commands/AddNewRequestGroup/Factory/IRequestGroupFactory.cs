using APIGiraffe.Domain;

namespace APIGiraffe.ApplicationServices.RequestGroups.Commands.AddNewRequestGroup.Factory
{
    public interface IRequestGroupFactory
    {
        RequestGroup Create(string name);
    }
}