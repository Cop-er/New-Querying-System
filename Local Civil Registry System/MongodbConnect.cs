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
        public readonly string _birthPrintForm = Links_RDLC.BirthFormLink;
        private readonly string DatabaseConnection = Links_RDLC.DbConnection;
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
            } finally
            {

            }

        }


        public async Task<List<BsonDocument>> LCR_Signatures()
        {
            ConnectToMongoDB("Local_Civil_Registry_System");
            var _collection = Database.GetCollection<BsonDocument>("Signatures");
            var _filter = Builders<BsonDocument>.Filter.Exists("UserName");
            var _doc = await _collection.Find(_filter).ToListAsync();
            return _doc;
        }

        public async Task<BsonDocument> LCR_Signatures_Title(string data)
        {
            ConnectToMongoDB("Local_Civil_Registry_System");
            var _collection = Database.GetCollection<BsonDocument>("Signatures");
            var _filter = Builders<BsonDocument>.Filter.Regex("UserName", new BsonRegularExpression(data, "i"));
            var _doc = await _collection.Find(_filter).FirstOrDefaultAsync(); // Get only the first match
            return _doc;
        }







        public async Task<DataTable> QueryBirthChild(string dt1, string dt2)
        {
            ConnectToMongoDB("Birth");
            List<BsonDocument> _allResults = new List<BsonDocument>();

            for (int year = 1900; year <= DateTime.Now.Year; year++)
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

            for (int year = 1900; year <= DateTime.Now.Year; year++)
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

            for (int year = 1900; year <= DateTime.Now.Year; year++)
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

            for (int year = 1900; year <= DateTime.Now.Year; year++)
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
                { "MNATL", "Mother Nationality" },


                { "FFIRST", "Father First Name" },
                { "FMI", "Father MI" },
                { "FLAST", "Father Last Name" },
                { "FNATL", "Father Nationality" },

                { "DATE", "Date of Birth" },
                { "SEX", "Sex (Male: 1, Female: 2)" },
                { "FOL", "Book Number" },
                { "PAGE", "Page Number" },
                { "DATEMAR", "Date of Marriage" },
                { "PLACEMAR", "Place of Marriage" },
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

            for (int year = 1900; year <= DateTime.Now.Year; year++)
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

            for (int year = 1900; year <= DateTime.Now.Year; year++)
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

            for (int year = 1900; year <= DateTime.Now.Year; year++)
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

                { "G_FNAME", "Groom First Name" }, //2
                { "G_MI", "Groom MI" },
                { "G_LNAME", "Groom Last Name" },
                { "W_FNAME", "Bride First Name" }, //5
                { "W_MI", "Bride MI" },
                { "W_LNAME", "Bride Last Name" },

                { "DATE", "Date of Marriage" }, //8

                { "G_FFIRST", "First Name" }, //9
                { "G_FMI", "MI" },
                { "G_FLAST", "Last Name" },
                { "G_MFIRST", "Mother First Name" }, //12
                { "G_MMI", "Mother MI" },
                { "G_MLAST", "Mother Last Name" },

                { "W_FFIRST", "Bride Father First Name" }, //15
                { "W_FMI", "Bride Father MI" },
                { "W_FLAST", "Bride Father Last Name" },
                { "W_MFIRST", "Bride Mother First Name" }, //18
                { "W_MMI", "Bride Mother MI" },
                { "W_MLAST", "Bride Mother Last Name" },


                { "G_AGE", "Groom Age" },  //21
                { "W_AGE", "Bride Age" },  //22

                { "G_CITI", "Groom Citizenship" },  //23
                { "W_CITI", "Bride Citizenship" },  //24

                { "G_RELI", "Groom Religion" },   //25
                { "W_RELI", "Bride Religion" },

                { "G_STATUS", "Groom Status" },  //27
                { "W_STATUS", "Bride Status" },

                { "FOL", "Book Number" },  //29
                { "PAGE", "Page Number" },

                { "DREG", "Date of Registration" },  //31


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









        //Death

        public async Task<DataTable> QueryDeathChild(string dt1, string dt2)
        {
            ConnectToMongoDB("Death");
            List<BsonDocument> _allResults = new List<BsonDocument>();

            for (int year = 1900; year <= DateTime.Now.Year; year++)
            {
                var _collection = Database.GetCollection<BsonDocument>(year.ToString());
                var _filterFirst = Builders<BsonDocument>.Filter.Regex("FIRST", new BsonRegularExpression(dt1, "i"));
                var _filterLast = Builders<BsonDocument>.Filter.Regex("LAST", new BsonRegularExpression(dt2, "i"));
                var _combinedFilter = Builders<BsonDocument>.Filter.And(_filterFirst, _filterLast);
                var _result = await _collection.Find(_combinedFilter).ToListAsync();
                _allResults.AddRange(_result);
            }
            DataTable dataTable = ConvertToDataTableDeath(_allResults);
            return dataTable;
        }

        public async Task<DataTable> QueryDeathRegistryNumber(string dt1)
        {
            ConnectToMongoDB("Death");
            List<BsonDocument> _allResults = new List<BsonDocument>();

            for (int year = 1900; year <= DateTime.Now.Year; year++)
            {
                var _collection = Database.GetCollection<BsonDocument>(year.ToString());
                var _filterFirst = Builders<BsonDocument>.Filter.Regex("LCR_NO", new BsonRegularExpression(dt1, "i"));
                var _result = await _collection.Find(_filterFirst).ToListAsync();
                _allResults.AddRange(_result);
            }
            DataTable dataTable = ConvertToDataTableDeath(_allResults);
            return dataTable;
        }


        private DataTable ConvertToDataTableDeath(List<BsonDocument> documents)
        {
            var columnsToInclude = new Dictionary<string, string>
            {
                { "CT", "No." },
                { "LCR_NO", "Registry Number" },
                { "FIRST", "First Name" },
                { "MI", "MI" },
                { "LAST", "Last Name" },
                
                { "DATEX", "Date of Death" },
                { "SEX", "Sex (Male: 1, Female: 2)" },
                { "AGE", "Age" },
                { "FOLIO_NO", "Book Number" },
                { "PAGE_NO", "Page Number" },

                { "MFIRST", "Mother First Name" },
                { "MMI", "Mother MI" },
                { "MLAST", "Mother Last Name" },
                { "FFIRST", "Father First Name" },
                { "FMI", "Father MI" },
                { "FLAST", "Father Last Name" },

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
