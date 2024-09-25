using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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

        public async Task<DataTable> QueryBirthChild(string dt1, string dt2)
        {
            ConnectToMongoDB("Birth");
            List<BsonDocument> _allResults = new List<BsonDocument>();

            for (int year = 1920; year <= DateTime.Now.Year; year++)
            {
                var _collection = Database.GetCollection<BsonDocument>(year.ToString());
                var _filterFirst = Builders<BsonDocument>.Filter.Regex("FIRST", new BsonRegularExpression(dt1, "i"));
                var _filterLast = Builders<BsonDocument>.Filter.Regex("LAST", new BsonRegularExpression(dt2, "i"));
                var _combinedFilter = Builders<BsonDocument>.Filter.And(_filterFirst, _filterLast);
                var _result = await _collection.Find(_combinedFilter).ToListAsync();
                _allResults.AddRange(_result);
            }
            DataTable dataTable = ConvertToDataTable(_allResults);
            return dataTable;
        }

        public async Task<DataTable> QueryFatherChild(string dt1, string dt2)
        {
            ConnectToMongoDB("Birth");
            List<BsonDocument> _allResults = new List<BsonDocument>();

            for (int year = 1920; year <= DateTime.Now.Year; year++)
            {
                var _collection = Database.GetCollection<BsonDocument>(year.ToString());
                var _filterFirst = Builders<BsonDocument>.Filter.Regex("FFIRST", new BsonRegularExpression(dt1, "i"));
                var _filterLast = Builders<BsonDocument>.Filter.Regex("FLAST", new BsonRegularExpression(dt2, "i"));
                var _combinedFilter = Builders<BsonDocument>.Filter.And(_filterFirst, _filterLast);
                var _result = await _collection.Find(_combinedFilter).ToListAsync();
                _allResults.AddRange(_result);
            }
            DataTable dataTable = ConvertToDataTable(_allResults);
            return dataTable;
        }

        public async Task<DataTable> QueryMotherChild(string dt1, string dt2)
        {
            ConnectToMongoDB("Birth");
            List<BsonDocument> _allResults = new List<BsonDocument>();

            for (int year = 1920; year <= DateTime.Now.Year; year++)
            {
                var _collection = Database.GetCollection<BsonDocument>(year.ToString());
                var _filterFirst = Builders<BsonDocument>.Filter.Regex("MFIRST", new BsonRegularExpression(dt1, "i"));
                var _filterLast = Builders<BsonDocument>.Filter.Regex("MLAST", new BsonRegularExpression(dt2, "i"));
                var _combinedFilter = Builders<BsonDocument>.Filter.And(_filterFirst, _filterLast);
                var _result = await _collection.Find(_combinedFilter).ToListAsync();
                _allResults.AddRange(_result);
            }
            DataTable dataTable = ConvertToDataTable(_allResults);
            return dataTable;
        }

        public async Task<DataTable> QueryRegistryNumber(string dt1)
        {
            ConnectToMongoDB("Birth");
            List<BsonDocument> _allResults = new List<BsonDocument>();

            for (int year = 1920; year <= DateTime.Now.Year; year++)
            {
                var _collection = Database.GetCollection<BsonDocument>(year.ToString());
                var _filterFirst = Builders<BsonDocument>.Filter.Regex("LCR", new BsonRegularExpression(dt1, "i"));
                var _result = await _collection.Find(_filterFirst).ToListAsync();
                _allResults.AddRange(_result);
            }
            DataTable dataTable = ConvertToDataTable(_allResults);
            return dataTable;
        }






        //Birth DataTable
        private DataTable ConvertToDataTable(List<BsonDocument> documents)
        {
            var columnsToInclude = new Dictionary<string, string>
            {
                { "CT", "No." },
                { "LCR", "Registry Number" },
                { "FIRST", "First Name" },
                { "MI", "MI" },
                { "LAST", "Last Name" },
                { "MFIRST", "Mother First Name" },
                { "MMI", "Mother MI" },
                { "MLAST", "Mother Last Name" },
                { "FFIRST", "Father First Name" },
                { "FMI", "Father MI" },
                { "FLAST", "Father Last Name" },
                { "DATE", "Date of Birth" },
                { "SEX", "Sex (Male: 1, Female: 2)" },
                { "FOL", "Book Number" },
                { "PAGE", "Page Number" },
                { "DATEMAR", "Date of Marriage" },
                { "DREG", "Date of Registration" },


            };

            DataTable dataTable = new DataTable();

            foreach (var field in columnsToInclude)
            {
                dataTable.Columns.Add(field.Value, typeof(string));
            }

            int i = 1;

            foreach (var document in documents)
            {
                var row = dataTable.NewRow();
                foreach (var column in columnsToInclude.Keys)
                {
                    if (document.Contains(column))
                    {
                        if (column == "CT") // Use the key directly for comparison
                        {
                            row[columnsToInclude[column]] = i++; // Assign the incrementing number
                        }
                        else
                        {
                            row[columnsToInclude[column]] = document[column].ToString();
                        }
                    }
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }





















        //Marriage Part

        public async Task<DataTable> QueryMarriageHusband(string dt1, string dt2)
        {
            ConnectToMongoDB("Marriage");
            List<BsonDocument> _allResults = new List<BsonDocument>();

            for (int year = 1952; year <= DateTime.Now.Year; year++)
            {
                var _collection = Database.GetCollection<BsonDocument>(year.ToString());
                var _filterFirst = Builders<BsonDocument>.Filter.Regex("G_FNAME", new BsonRegularExpression(dt1, "i"));
                var _filterLast = Builders<BsonDocument>.Filter.Regex("G_LNAME", new BsonRegularExpression(dt2, "i"));
                var _combinedFilter = Builders<BsonDocument>.Filter.And(_filterFirst, _filterLast);
                var _result = await _collection.Find(_combinedFilter).ToListAsync();
                _allResults.AddRange(_result);
            }
            DataTable dataTable = ConvertToDataTableMarriage(_allResults);
            return dataTable;
        }

        public async Task<DataTable> QueryMarriageWife(string dt1, string dt2)
        {
            ConnectToMongoDB("Marriage");
            List<BsonDocument> _allResults = new List<BsonDocument>();

            for (int year = 1952; year <= DateTime.Now.Year; year++)
            {
                var _collection = Database.GetCollection<BsonDocument>(year.ToString());
                var _filterFirst = Builders<BsonDocument>.Filter.Regex("W_FNAME", new BsonRegularExpression(dt1, "i"));
                var _filterLast = Builders<BsonDocument>.Filter.Regex("W_LNAME", new BsonRegularExpression(dt2, "i"));
                var _combinedFilter = Builders<BsonDocument>.Filter.And(_filterFirst, _filterLast);
                var _result = await _collection.Find(_combinedFilter).ToListAsync();
                _allResults.AddRange(_result);
            }
            DataTable dataTable = ConvertToDataTableMarriage(_allResults);
            return dataTable;
        }

        public async Task<DataTable> QueryMarriageRegistryNumber(string dt1)
        {
            ConnectToMongoDB("Marriage");
            List<BsonDocument> _allResults = new List<BsonDocument>();

            for (int year = 1920; year <= DateTime.Now.Year; year++)
            {
                var _collection = Database.GetCollection<BsonDocument>(year.ToString());
                var _filterFirst = Builders<BsonDocument>.Filter.Regex("LCR", new BsonRegularExpression(dt1, "i"));
                var _result = await _collection.Find(_filterFirst).ToListAsync();
                _allResults.AddRange(_result);
            }
            DataTable dataTable = ConvertToDataTableMarriage(_allResults);
            return dataTable;
        }


        //Marriage
        private DataTable ConvertToDataTableMarriage(List<BsonDocument> documents)
        {
            var columnsToInclude = new Dictionary<string, string>
            {
                { "CT", "No." },
                { "LCR", "Registry Number" },

                { "G_FNAME", "Groom First Name" },
                { "G_MI", "Groom MI" },
                { "G_LNAME", "Groom Last Name" },
                { "W_FNAME", "Bride First Name" },
                { "W_MI", "Bride MI" },
                { "W_LNAME", "Bride Last Name" },

                { "DATE", "Date of Marriage" },

                { "G_FFIRST", "First Name" },
                { "G_FMI", "MI" },
                { "G_FLAST", "Last Name" },
                { "G_MFIRST", "Mother First Name" },
                { "G_MMI", "Mother MI" },
                { "G_MLAST", "Mother Last Name" },

                { "W_FFIRST", "Bride Father First Name" },
                { "W_FMI", "Bride Father MI" },
                { "W_FLAST", "Bride Father Last Name" },
                { "W_MFIRST", "Bride Mother First Name" },
                { "W_MMI", "Bride Mother MI" },
                { "W_MLAST", "Bride Mother Last Name" },


                { "G_AGE", "Groom Age" },
                { "W_AGE", "Bride Age" },

                { "G_CITI", "Groom Citizenship" },
                { "W_CITI", "Bride Citizenship" },

                { "G_RELI", "Groom Religion" },
                { "W_RELI", "Bride Religion" },

                { "G_STATUS", "Groom Status" },
                { "W_STATUS", "Bride Status" },

                { "FOL", "Book Number" },
                { "PAGE", "Page Number" },

                { "DREG", "Date of Registration" },


            };

            DataTable dataTable = new DataTable();

            foreach (var field in columnsToInclude)
            {
                dataTable.Columns.Add(field.Value, typeof(string));
            }

            int i = 1;

            foreach (var document in documents)
            {
                var row = dataTable.NewRow();
                foreach (var column in columnsToInclude.Keys)
                {
                    if (document.Contains(column))
                    {
                        if (column == "CT") // Use the key directly for comparison
                        {
                            row[columnsToInclude[column]] = i++; // Assign the incrementing number
                        }
                        else
                        {
                            row[columnsToInclude[column]] = document[column].ToString();
                        }
                    }
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

    }
}
