using System.Collections.Generic;
using APIGiraffe.Domain.Entities;

namespace APIGiraffe.Domain.Factories
{
    public interface IRequestFactory
    {
        Request Create(int id, int groupid, string name, string url, List<Header> headers);
        Request Create(int groupid, string name);
        Request Create(int groupid, string name, string url, List<Header> headers);
        Request Create(string url, List<Header> headers);
    }
}