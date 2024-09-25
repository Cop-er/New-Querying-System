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
        private readonly string DatabaseName = "Birth";
        public MongoClient Client { get; private set; }
        public IMongoDatabase Database { get; private set; }

        public async Task ConnectToMongoDB()
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

        public async Task QueryBirthChild()
        {
            await ConnectToMongoDB();
            var _collection = Database.GetCollection<BsonDocument>("2000");
            var _filterFirst = Builders<BsonDocument>.Filter.Regex("FIRST", new BsonRegularExpression("john", "i"));
            var _filterLast = Builders<BsonDocument>.Filter.Regex("LAST", new BsonRegularExpression("S", "i"));
            var _combinedFilter = Builders<BsonDocument>.Filter.And(_filterFirst, _filterLast);
            var _result = await _collection.Find(_combinedFilter).ToListAsync();
            foreach (var x in _result)
            {
                Console.WriteLine(x.ToString());
            }
        }


    }
}
