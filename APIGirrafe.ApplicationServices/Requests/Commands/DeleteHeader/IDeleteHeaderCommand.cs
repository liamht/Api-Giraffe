namespace APIGirrafe.ApplicationServices.Requests.Commands.DeleteHeader
{
    public interface IDeleteHeaderCommand
    {
        void Execute(int headerId);
    }
}