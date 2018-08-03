namespace APIGiraffe.ApplicationServices.Requests.Commands.UpdateHeader
{
    public interface IUpdateHeaderCommand
    {
        void Execute(int id, string name, string value);
    }
}