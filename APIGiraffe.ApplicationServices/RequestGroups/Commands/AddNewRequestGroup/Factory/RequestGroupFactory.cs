namespace APIGiraffe.ApplicationServices.RequestGroups.Commands.AddNewRequestGroup.Factory
{
    public class RequestGroupFactory : IRequestGroupFactory
    {
        public Domain.RequestGroup Create(string name)
        {
            return new Domain.RequestGroup()
            {
                Name = name
            };
        }
    }
}
