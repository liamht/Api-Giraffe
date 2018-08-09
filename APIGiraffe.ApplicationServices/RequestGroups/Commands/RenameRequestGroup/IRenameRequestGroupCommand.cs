namespace APIGiraffe.ApplicationServices.RequestGroups.Commands.RenameRequestGroup
{
    public interface IRenameRequestGroupCommand
    {
        void Execute(int requestGroupId, string newName);
    }
}