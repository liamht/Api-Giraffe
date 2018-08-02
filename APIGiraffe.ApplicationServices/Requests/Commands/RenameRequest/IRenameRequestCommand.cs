namespace APIGiraffe.ApplicationServices.Requests.Commands.RenameRequest
{
    public interface IRenameRequestCommand
    {
        void Execute(int id, string newName);
    }
}