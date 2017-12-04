using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using Sgi.Core.DBService;
using Sgi.MongoDBService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sgi.MongoDBService
{
    public class MongoRepository : IDBRepository
    {
        private IMongoDBConfig _config;

        private MongoClient _mongoClient;
        private MongoServer _server;
        private IMongoDatabase _mongoDataBase;

        public MongoRepository(IMongoDBConfig config)
        {
            _config = config;
            OpenConnection();
        }

        private void OpenConnection()
        {
            if (_mongoClient == null)
            {
                var settings = new MongoClientSettings
                {
                    Credentials = new[] { MongoCredential.CreateCredential(_config.Database, _config.UserName, _config.Password) },
                    Server = new MongoServerAddress(_config.ServerIP, _config.DBPort)
                };

                _mongoClient = new MongoClient(settings);
                _mongoDataBase = _mongoClient.GetDatabase(_config.Database);
                _server = _mongoClient.GetServer();
            }
        }

        public IQueryable<T> GetData<T>(string collection)
        {
            return _mongoDataBase.GetCollection<T>(collection).AsQueryable();
        }

        public IMongoCollection<T> GetCollection<T>(string collection)
        {
            return _mongoDataBase.GetCollection<T>(collection);
        }

        public void Insert<T>(string collection, T data)
        {
            _mongoDataBase.GetCollection<T>(collection).InsertOne(data);
        }

        public void Update<T>(string collection, string id, T data)
        {
            //TODO:  need better way to update data
            var filter = Builders<T>.Filter.Eq("_id", id);
            _mongoDataBase.GetCollection<T>(collection).DeleteOne(filter);
            Insert<T>(collection, data);
        }

        public void PingDB()
        {
            _server.Ping();
        }

        public void Delete<T>(string collection, string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            _mongoDataBase.GetCollection<T>(collection).DeleteOne(filter);
        }


        public List<JObject> GetData(string collection, System.Collections.Generic.List<FilterCriteria> filters)
        {

            var builder = Builders<BsonDocument>.Filter;
            FilterDefinition<BsonDocument> filter = builder.Empty;

            IMongoCollection<BsonDocument> query = _mongoDataBase.GetCollection<BsonDocument>(collection);
            foreach (var filterParam in filters)
            {
                Operators filterOp;
                DataType dataType;

                if ((Enum.TryParse(filterParam.Operator, out filterOp)) && (Enum.TryParse(filterParam.DataType, out dataType)))

                    if (!String.IsNullOrEmpty(filterParam.Value))
                    {
                        switch (dataType)
                        {
                            case DataType.String:
                                if (filter != builder.Empty && filterOp.Equals(Operators.Contains))
                                {
                                    filter &= builder.Regex(filterParam.Key, "/" + filterParam.Value + "/i");
                                }
                                else if (filter != builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter &= builder.Regex(filterParam.Key, "/^" + filterParam.Value + "$/i");
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter = builder.Regex(filterParam.Key, "/^" + filterParam.Value + "$/i");
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.Contains))
                                {
                                    filter = builder.Regex(filterParam.Key, "/" + filterParam.Value + "/i");
                                }
                                break;
                            case DataType.StringList:
                                if (filter != builder.Empty && filterOp.Equals(Operators.Contains))
                                {
                                    filter &= builder.In(filterParam.Key, filterParam.Value.Split(',').ToList<string>());
                                }
                                else if (filter != builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter = builder.In(filterParam.Key, filterParam.Value.Split(',').ToList<string>());
                                }
                                break;

                            case DataType.NumericList:

                                if (filter != builder.Empty)
                                {
                                    filter &= builder.In(filterParam.Key, Array.ConvertAll(filterParam.Value.Split(','), int.Parse));
                                }
                                else
                                {
                                    filter = builder.In(filterParam.Key, Array.ConvertAll(filterParam.Value.Split(','), int.Parse));
                                }

                                break;

                            case DataType.Int32:
                            case DataType.Int64:

                                if (filter != builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter &= builder.Eq(filterParam.Key, Convert.ToInt32(filterParam.Value));
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter = builder.Eq(filterParam.Key, Convert.ToInt32(filterParam.Value));
                                }
                                else if (filter != builder.Empty && filterOp.Equals(Operators.GreaterThanEqualTo))
                                {
                                    filter &= builder.Gte(filterParam.Key, Convert.ToInt32(filterParam.Value));
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.GreaterThanEqualTo))
                                {
                                    filter = builder.Gte(filterParam.Key, Convert.ToInt32(filterParam.Value));
                                }
                                else if (filter != builder.Empty && filterOp.Equals(Operators.LessThanEqualTo))
                                {
                                    filter &= builder.Lte(filterParam.Key, Convert.ToInt32(filterParam.Value));
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.LessThanEqualTo))
                                {
                                    filter = builder.Lte(filterParam.Key, Convert.ToInt32(filterParam.Value));
                                }
                                break;

                            case DataType.DateTime:
                            case DataType.Double:

                                if (filter != builder.Empty && filterOp.Equals(Operators.GreaterThanEqualTo))
                                {
                                    filter &= builder.Gte(filterParam.Key, ConvertToUnixTimestamp(DateTime.Parse(filterParam.Value)));
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.GreaterThanEqualTo))
                                {
                                    filter = builder.Gte(filterParam.Key, ConvertToUnixTimestamp(DateTime.Parse(filterParam.Value)));
                                }
                                else if (filter != builder.Empty && filterOp.Equals(Operators.LessThanEqualTo))
                                {
                                    filter &= builder.Lte(filterParam.Key, ConvertToUnixTimestamp(DateTime.Parse(filterParam.Value)));
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.LessThanEqualTo))
                                {
                                    filter = builder.Lte(filterParam.Key, ConvertToUnixTimestamp(DateTime.Parse(filterParam.Value)));
                                }
                                else if (filter != builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter &= builder.Eq(filterParam.Key, ConvertToUnixTimestamp(DateTime.Parse(filterParam.Value)));
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter = builder.Eq(filterParam.Key, ConvertToUnixTimestamp(DateTime.Parse(filterParam.Value)));
                                }
                                break;
                            case DataType.EntityValues:
                                if (filter != builder.Empty && filterOp.Equals(Operators.Contains))
                                {
                                    var bsonQuery = @"{ Entities: {  $elemMatch: {Values : }}}";
                                    bsonQuery = bsonQuery.Insert(bsonQuery.LastIndexOf(':') + 1, string.Format("'{0}'",filterParam.Value));

                                    filter &= MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(bsonQuery);
                                }

                                break;
                        }
                    }
            }
            var count = Convert.ToInt32(query.Find(filter).Count());


            List<BsonDocument> pagedRecords = query.Find(filter).ToList<BsonDocument>();//  .SortByDescending(bson => bson["CreatedOn"]).ThenByDescending(bson => bson["Step"]).Skip((model.CurrentPage - 1) * model.PageSize).Limit(model.PageSize).ToList();

            List<JObject> listLogs = new List<JObject>();
            foreach (var q in pagedRecords)
            {
                var documentJson = MongoDB.Bson.BsonExtensionMethods.ToJson(q, new MongoDB.Bson.IO.JsonWriterSettings { OutputMode = MongoDB.Bson.IO.JsonOutputMode.Strict });
                listLogs.Add(JObject.Parse(documentJson));
            }

            return listLogs;

        }

        public List<JObject> GetNSortedData(string collection, System.Collections.Generic.List<FilterCriteria> filters, int numberOfRecords, string sortByField, bool sortByAscending)
        {
            var builder = Builders<BsonDocument>.Filter;
            var sortingBuilder = Builders<BsonDocument>.Sort;
            FilterDefinition<BsonDocument> filter = builder.Empty;
            SortDefinition<BsonDocument> sort;

            int sortBy = sortByAscending ? 1 : -1;

            IMongoCollection<BsonDocument> query = _mongoDataBase.GetCollection<BsonDocument>(collection);
            foreach (var filterParam in filters)
            {
                Operators filterOp;
                DataType dataType;

                if ((Enum.TryParse(filterParam.Operator, out filterOp)) && (Enum.TryParse(filterParam.DataType, out dataType)))

                    if (!String.IsNullOrEmpty(filterParam.Value))
                    {
                        switch (dataType)
                        {
                            case DataType.String:
                                if (filter != builder.Empty && filterOp.Equals(Operators.Contains))
                                {
                                    filter &= builder.Regex(filterParam.Key, "/" + filterParam.Value + "/i");
                                }
                                else if (filter != builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter &= builder.Regex(filterParam.Key, "/^" + filterParam.Value + "$/i");
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter = builder.Regex(filterParam.Key, "/^" + filterParam.Value + "$/i");
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.Contains))
                                {
                                    filter = builder.Regex(filterParam.Key, "/" + filterParam.Value + "/i");
                                }
                                break;
                            case DataType.StringList:
                                if (filter != builder.Empty && filterOp.Equals(Operators.Contains))
                                {
                                    filter &= builder.In(filterParam.Key, filterParam.Value.Split(',').ToList<string>());
                                }
                                else if (filter != builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter &= builder.In(filterParam.Key, filterParam.Value.Split(',').ToList<string>());
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter = builder.In(filterParam.Key, filterParam.Value.Split(',').ToList<string>());
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.Contains))
                                {
                                    filter = builder.In(filterParam.Key, filterParam.Value.Split(',').ToList<string>());
                                }
                                break;

                            case DataType.NumericList:

                                if (filter != builder.Empty)
                                {
                                    filter &= builder.In(filterParam.Key, Array.ConvertAll(filterParam.Value.Split(','), int.Parse));
                                }
                                else
                                {
                                    filter = builder.In(filterParam.Key, Array.ConvertAll(filterParam.Value.Split(','), int.Parse));
                                }

                                break;

                            case DataType.Int32:
                            case DataType.Int64:

                                if (filter != builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter &= builder.Eq(filterParam.Key, Convert.ToInt32(filterParam.Value));
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter = builder.Eq(filterParam.Key, Convert.ToInt32(filterParam.Value));
                                }
                                else if (filter != builder.Empty && filterOp.Equals(Operators.GreaterThanEqualTo))
                                {
                                    filter &= builder.Gte(filterParam.Key, Convert.ToInt32(filterParam.Value));
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.GreaterThanEqualTo))
                                {
                                    filter = builder.Gte(filterParam.Key, Convert.ToInt32(filterParam.Value));
                                }
                                else if (filter != builder.Empty && filterOp.Equals(Operators.LessThanEqualTo))
                                {
                                    filter &= builder.Lte(filterParam.Key, Convert.ToInt32(filterParam.Value));
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.LessThanEqualTo))
                                {
                                    filter = builder.Lte(filterParam.Key, Convert.ToInt32(filterParam.Value));
                                }
                                break;

                            case DataType.DateTime:
                            case DataType.Double:

                                if (filter != builder.Empty && filterOp.Equals(Operators.GreaterThanEqualTo))
                                {
                                    filter &= builder.Gte(filterParam.Key, ConvertToUnixTimestamp(DateTime.Parse(filterParam.Value)));
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.GreaterThanEqualTo))
                                {
                                    filter = builder.Gte(filterParam.Key, ConvertToUnixTimestamp(DateTime.Parse(filterParam.Value)));
                                }
                                else if (filter != builder.Empty && filterOp.Equals(Operators.LessThanEqualTo))
                                {
                                    filter &= builder.Lte(filterParam.Key, ConvertToUnixTimestamp(DateTime.Parse(filterParam.Value)));
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.LessThanEqualTo))
                                {
                                    filter = builder.Lte(filterParam.Key, ConvertToUnixTimestamp(DateTime.Parse(filterParam.Value)));
                                }
                                else if (filter != builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter &= builder.Eq(filterParam.Key, ConvertToUnixTimestamp(DateTime.Parse(filterParam.Value)));
                                }
                                else if (filter == builder.Empty && filterOp.Equals(Operators.EqualTo))
                                {
                                    filter = builder.Eq(filterParam.Key, ConvertToUnixTimestamp(DateTime.Parse(filterParam.Value)));
                                }
                                break;
                            case DataType.EntityValues:
                                if (filter != builder.Empty && filterOp.Equals(Operators.Contains))
                                {
                                    var bsonQuery = @"{ Entities: {  $elemMatch: {Values : }}}";
                                    bsonQuery = bsonQuery.Insert(bsonQuery.LastIndexOf(':') + 1, string.Format("'{0}'", filterParam.Value));

                                    filter &= MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(bsonQuery);
                                }
                                break;
                            case DataType.EntityType:
                                if (filter != builder.Empty && filterOp.Equals(Operators.Contains))
                                {
                                    var bsonQuery = @"{'intent.entities':{$elemMatch:{'type' : }}}";
                                    bsonQuery = bsonQuery.Insert(bsonQuery.LastIndexOf(':') + 1, string.Format("'{0}'", filterParam.Value));

                                    filter &= MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(bsonQuery);
                                }
                                else
                                {
                                    var bsonQuery = @"{'intent.entities':{$elemMatch:{'type' : }}}";
                                    bsonQuery = bsonQuery.Insert(bsonQuery.LastIndexOf(':') + 1, string.Format("'{0}'", filterParam.Value));

                                    filter = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(bsonQuery);
                                }
                                break;
                        }
                    }
            }

            var sortQuery = string.Format("{{{0}:{1}}}", sortByField,sortBy);
            sort = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(sortQuery);

            List<BsonDocument> pagedRecords = query.Find(filter).Limit(numberOfRecords).Sort(sort).ToList<BsonDocument>();

            List<JObject> listLogs = new List<JObject>();
            foreach (var q in pagedRecords)
            {
                var documentJson = MongoDB.Bson.BsonExtensionMethods.ToJson(q, new MongoDB.Bson.IO.JsonWriterSettings { OutputMode = MongoDB.Bson.IO.JsonOutputMode.Strict });
                listLogs.Add(JObject.Parse(documentJson));
            }

            return listLogs;

        }

        public double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}