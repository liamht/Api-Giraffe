namespace APIGiraffe.ApplicationServices.Headers.Commands.DeleteHeader
{
    public interface IDeleteHeaderCommand
    {
        void Execute(int headerId);
    }
}