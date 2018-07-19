namespace APIGiraffe.ApplicationServices.Requests.Commands.RenameRequestGroup
{
    public interface IRenameRequestGroupCommand
    {
        void Execute(int requestGroupId, string newName);
    }
}