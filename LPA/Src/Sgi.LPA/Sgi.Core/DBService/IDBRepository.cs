using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Sgi.Core.DBService
{
    public interface IDBRepository
    {
        IQueryable<T> GetData<T>(string collection);
        List<JObject> GetData(string collection,List<FilterCriteria> filters);
        List<JObject> GetNSortedData(string collection, System.Collections.Generic.List<FilterCriteria> filters, int numberOfRecords, string sortByField, bool sortByAscending);

        //MongoCursor<T> GetData<T>(string collection, IMongoQuery query);
        void Insert<T>(string collection, T data);

        void Update<T>(string collection, string id, T data);

        void Delete<T>(string collection, string id);

        void PingDB();

        //   IMongoCollection<T> GetCollection<T>(string collection);
    }
}