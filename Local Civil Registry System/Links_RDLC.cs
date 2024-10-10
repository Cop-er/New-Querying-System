using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local_Civil_Registry_System
{
    class Links_RDLC
    {
        //for Birth
        public static string BirthFormLink { get; private set; } = "Birth_Certified_True_Copy.rdlc";
        public static readonly string BirthFormLink_Dummy = @"C:\Users\Administrator\source\repos\Local Civil Registry System\Local Civil Registry System\Birth_Certified_True_Copy.rdlc";


        //Mongodb Connection
        public static readonly string DbConnection = "mongodb://admin:Losser989@192.168.4.56:27017/?directConnection=true";


        static Links_RDLC()
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BirthFormLink);

            if (!File.Exists(fullPath))
            {
                BirthFormLink = BirthFormLink_Dummy;
            }
            else
            {
                BirthFormLink = fullPath;

            }
        }



    }
}
