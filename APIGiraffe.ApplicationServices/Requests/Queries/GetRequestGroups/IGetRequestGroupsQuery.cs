using System.Collections.Generic;

namespace APIGiraffe.ApplicationServices.Requests.Queries.GetRequestGroups
{
    public interface IGetRequestGroupsQuery
    {
        List<RequestGroup> Execute();
    }
}