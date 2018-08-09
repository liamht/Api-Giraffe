using System.Collections.Generic;

namespace APIGiraffe.ApplicationServices.RequestGroups.Queries.GetRequestGroups
{
    public interface IGetRequestGroupsQuery
    {
        List<RequestGroup> Execute();
    }
}