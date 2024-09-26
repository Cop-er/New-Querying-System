using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local_Civil_Registry_System
{
    class Nationality_Converter
    {

        private Dictionary<string, string> nationalityDict;

        public Nationality_Converter()
        {
            nationalityDict = new Dictionary<string, string>
            {
                { "FILIPINO", "1" },
                { "CHINESE", "2" },
                { "AMERICAN", "3" },
                { "SPANISH", "4" },
                { "JAPANESE", "5" },
                { "AUSTRALIAN", "6" },
                { "IRANIAN", "7" },
                { "GERMAN", "8" },
                { "NOT STATED", "9" },
                { "OTHERS", "0" }
            };
        }
        public string GetNationalityKey(string value)
        {
            foreach (var kvp in nationalityDict)
            {
                if (kvp.Value == value)
                {
                    return kvp.Key;
                }
            }
            return value;
            throw new KeyNotFoundException($"No nationality found for the value '{value}'.");
        }



    }
}
