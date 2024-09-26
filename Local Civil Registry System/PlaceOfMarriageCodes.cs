using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local_Civil_Registry_System
{
    class PlaceOfMarriageCodes
    {
        private Dictionary<string, string> POMC;

        public PlaceOfMarriageCodes()
        {
            POMC = new Dictionary<string, string>
            {
                { "BISLIG, SURIGAO DEL SUR" , "68031" },
                { "HINATUAN, SURIGAO DEL SUR" , "68032" },
            };
        }

        public string GetPlacesCodes(string value)
        {
            foreach (var GPC in POMC)
            {
                if (GPC.Value == value)
                {
                    return GPC.Key;
                }
            }
            return value;
        }


    }
}
