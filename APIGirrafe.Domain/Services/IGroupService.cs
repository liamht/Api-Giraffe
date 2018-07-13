using System.Collections.Generic;

namespace APIGirrafe.Domain.Services
{
    public interface IGroupService
    {
        List<RequestGroup> FetchFromDatabase();

        void AddNewGroup(RequestGroup requestGroup);

        void Delete(RequestGroup requestGroup);
    }
}
