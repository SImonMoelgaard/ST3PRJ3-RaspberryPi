using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DTO_s;

namespace PresentationLogic
{
    public class ReadFromFile : IBPData
    {
        /// <summary>
        /// DTO_Raw objekt bestående af blodtryk og tiden nu
        /// </summary>
        private DTO_Raw raw;
        /// <summary>
        /// opretter en liste bestående af DTO_Raw objekter
        /// </summary>
        private List<DTO_Raw> liste = new List<DTO_Raw>();
        /// <summary>
        /// en double atribut, der kan sættes ind i et DTO_Raw objekt 
        /// </summary>
        private double mmhgAsDouble;
        /// <summary>
        /// For at tallene ligner hinanden, uanset om de kommer fra physionet eller måleren
        /// </summary>
        private double mmhgAsV;


        /// <summary>
        /// Denne metode laver en liste med alle elementerne i physionetsfilen sample
        /// afspejler ikke virkeligheden, men har hjulpet på overblik over hvordan man fik datetime til at opfører sig rigtigt.
        /// </summary>
        /// <returns>en liste med alle blodtryksværdierne til en given tid</returns>
        //public List<DTO_Raw> HentFraCsvFil()
        //{
         
        //       // read file
        //    string[] lines = System.IO.File.ReadAllLines(@"..\..\..\Sample.txt");


        //    foreach (string line in lines)
        //    {
        //        // split in time and mmhg
        //        string[] splitLine = line.Split(',');
        //        string tid = splitLine[0];
        //        DateTime dateTime = DateTime.ParseExact(tid, "s.fff", System.Globalization.CultureInfo.InvariantCulture);
                

        //        string mmhg = splitLine[1];

        //        mmhgAsDouble = Convert.ToDouble(mmhg)/1000;

                
                

        //        // create blodtryks object
        //        DTO_Raw b = new DTO_Raw(mmhgAsDouble, dateTime);
        //        liste.Add(b);

        //        //return liste;
        //        //System.Console.WriteLine(line);
        //    }

        //    return liste;
            
        //}
        /// <summary>
        /// denne metode kommer mere til at ligne virkeligheden mere, hvor vi kun får en blodtryksværdi af gangen
        /// </summary>
        /// <returns></returns>
        public DTO_Raw MeassureSignal()
        {

            // read file
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\Sample.txt");


            foreach (string line in lines)
            {
                // split in time and mmhg
                string[] splitLine = line.Split(',');
                //string tid = splitLine[0];
                //DateTime dateTime = DateTime.ParseExact(tid, "s.fff", System.Globalization.CultureInfo.InvariantCulture);


                string mmhg = splitLine[1];

                mmhgAsDouble = Convert.ToDouble(mmhg) / 1000;
                mmhgAsV = mmhgAsDouble * 0.000005 * 5 * 559;
            }
            return raw = new DTO_Raw(mmhgAsV, DateTime.Now);
        }

    }

}


