using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local_Civil_Registry_System
{
    class MyFormatter
    {
        public string formatRegistry(string data)
        {
            string rawData = data.StartsWith("B") ? data.Substring(1) : data;
            if (!long.TryParse(rawData, out _))
            {
                return null;
            }
            if (rawData.Length < 3)
            {
                return null;
            }

            string year;
            string number;
            if (data.StartsWith("B"))
            {
                year = "20" + rawData.Substring(0, 2);
                number = rawData.Substring(2).TrimStart('0');
            }
            else
            {
                year = rawData.Substring(0, 2);
                number = rawData.Substring(2).TrimStart('0');
            }

            if (string.IsNullOrEmpty(number))
            {
                number = "0";
            }

            return $"{year}-{number}";
        }


        public string DateParser(string data)
        {
            try
            {
                DateTime dt = DateTime.Parse(data);
                string fdt = dt.ToString("MMMM dd, yyyy").ToUpper();
                return fdt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return data;
            }
        }



    }
}
