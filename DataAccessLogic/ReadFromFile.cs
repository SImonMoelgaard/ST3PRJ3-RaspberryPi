using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    public class ReadFromFile : IBPData
    {
        //private double midlertidigePunkter;
        //private DateTime midlertidigTid;
        private List<DTO_Raw> liste = new List<DTO_Raw>();


        /// <summary>
        /// Denne metode laver en liste med alle elementerne i physionetsfilen sample
        /// afspejler ikke virkeligheden, men har hjulpet på overblik over hvordan man fik datetime til at opfører sig rigtigt.
        /// </summary>
        /// <returns>en liste med alle blodtryksværdierne til en given tid</returns>
        public List<DTO_Raw> HentFraCsvFil()
        {
         
               // read file
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\Sample.txt");


            foreach (string line in lines)
            {
                // split in time and mmhg
                string[] splitLine = line.Split(',');
                string tid = splitLine[0];
                DateTime dateTime = DateTime.ParseExact(tid, "s.fff", System.Globalization.CultureInfo.InvariantCulture);
                

                string mmhg = splitLine[1];

                double mmhgAsDouble = Convert.ToDouble(mmhg)/1000;
                

                // create blodtryks object
                DTO_Raw b = new DTO_Raw(mmhgAsDouble, dateTime);
                liste.Add(b);

                //return liste;
                //System.Console.WriteLine(line);
            }

            return liste;
            //// indlæs sålænge der er data i filen
            //while ((inputRecord = fileReader.ReadLine()) != null)
            //{
            //    // split data op i tid og mmhg
            //    inputFields = inputRecord.Split(',');
            //    for (int i = 0; i < inputFields.Length; i++)
            //    {
            //       // inputFields[i] = inputFields[i].Trim('\'');
            //       inputFields[i]
            //    }

            //    // gem data i listen
            //    try
            //    {
            //        liste.Add(midlertidigePunkter,);
            //        //midlertidigTid.Add(Convert.ToDateTime(inputFields[0]));
            //        //midlertidigePunkter.Add(Convert.ToDouble(inputFields[1]));
            //    }
            //    catch (Exception)
            //    {

            //        // throw;
            //// luk adgang til filen
            //fileReader.Close();
            //DTO_Raw nyMåling = new DTO_Raw(DateTime.Now, midlertidigePunkter.ToArray());

            //return nyMåling;
        }
        /// <summary>
        /// denne metode kommer mere til at ligne virkeligheden mere, hvor vi kun får en blodtryksværdi af gangen
        /// </summary>
        /// <returns></returns>
        public DTO_Raw MeassureSignal()
        {
            throw new NotImplementedException();
        }
    }

}


