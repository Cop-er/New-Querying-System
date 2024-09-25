using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Local_Civil_Registry_System
{
    class MongodbConnect
    {

        private readonly string DatabaseConnection = "mongodb://admin:Losser989@localhost:27017/";
        public MongoClient Client { get; private set; }
        public IMongoDatabase Database { get; private set; }

        public void ConnectToMongoDB(string DatabaseName)
        {
            try
            {
                Client = new MongoClient(DatabaseConnection);
                Database = Client.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while connecting to MongoDB: {ex.Message}");
                throw;
            }
        }

        public async Task QueryBirthChild(string dt1, string dt2)
        {
            ConnectToMongoDB("Birth");

            for (int year = 1920; year <= DateTime.Now.Year; year++)
            {
                var _collection = Database.GetCollection<BsonDocument>(year.ToString());
                var _filterFirst = Builders<BsonDocument>.Filter.Regex("FIRST", new BsonRegularExpression(dt1, "i"));
                var _filterLast = Builders<BsonDocument>.Filter.Regex("LAST", new BsonRegularExpression(dt2, "i"));
                var _combinedFilter = Builders<BsonDocument>.Filter.And(_filterFirst, _filterLast);
                var _result = await _collection.Find(_combinedFilter).ToListAsync();
                foreach (var x in _result)
                {
                    Console.WriteLine(x.ToString());
                }
            }

        }

        public async Task QueryMotherChild(string dt1, string dt2)
        {
            ConnectToMongoDB("Birth");

            for (int year = 1920; year <= DateTime.Now.Year; year++)
            {
                var _collection = Database.GetCollection<BsonDocument>(year.ToString());
                var _filterFirst = Builders<BsonDocument>.Filter.Regex("MFIRST", new BsonRegularExpression(dt1, "i"));
                var _filterLast = Builders<BsonDocument>.Filter.Regex("MLAST", new BsonRegularExpression(dt2, "i"));
                var _combinedFilter = Builders<BsonDocument>.Filter.And(_filterFirst, _filterLast);
                var _result = await _collection.Find(_combinedFilter).ToListAsync();
                foreach (var x in _result)
                {
                    Console.WriteLine(x.ToString());
                }
            }

        }

        public async Task QueryFatherChild(string dt1, string dt2)
        {
            ConnectToMongoDB("Birth");

            for (int year = 1920; year <= DateTime.Now.Year; year++)
            {
                var _collection = Database.GetCollection<BsonDocument>(year.ToString());
                var _filterFirst = Builders<BsonDocument>.Filter.Regex("FFIRST", new BsonRegularExpression(dt1, "i"));
                var _filterLast = Builders<BsonDocument>.Filter.Regex("FLAST", new BsonRegularExpression(dt2, "i"));
                var _combinedFilter = Builders<BsonDocument>.Filter.And(_filterFirst, _filterLast);
                var _result = await _collection.Find(_combinedFilter).ToListAsync();
                foreach (var x in _result)
                {
                    Console.WriteLine(x.ToString());
                }
            }

        }



    }
}
