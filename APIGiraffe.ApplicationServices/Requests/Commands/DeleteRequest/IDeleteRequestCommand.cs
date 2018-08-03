namespace APIGiraffe.ApplicationServices.Requests.Commands.DeleteRequest
{
    public interface IDeleteRequestCommand
    {
        void Execute(int id);
    }
}