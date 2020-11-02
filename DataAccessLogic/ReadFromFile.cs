using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    class ReadFromFile
    {
        private List<DateTime> midlertidigTid;
        List<double> midlertidigePunkter;
        public DTO_Raw HentFraCsvFil(String FilNavn)
        {
            midlertidigTid = new List<DateTime>();
            midlertidigePunkter = new List<double>();

            // string-objekter til at gemme det som læses fra filen
            string inputRecord;
            string[] inputFields;


            // opret de nødvendige stream-objekter
            // FileStream input = new FileStream("Test_Atrieflimmer_1.csv", FileMode.OpenOrCreate, FileAccess.Read);
            FileStream input = new FileStream(FilNavn, FileMode.OpenOrCreate, FileAccess.Read);

            StreamReader fileReader = new StreamReader(input);


            // indlæs sålænge der er data i filen
            while ((inputRecord = fileReader.ReadLine()) != null)
            {
                // split data op i tid og mmhg
                inputFields = inputRecord.Split(',');
                for (int i = 0; i < inputFields.Length; i++)
                {
                    inputFields[i] = inputFields[i].Trim('\'');

                }

                // gem data i listen
                try
                {
                    midlertidigTid.Add(Convert.ToDateTime(inputFields[0]));
                    midlertidigePunkter.Add(Convert.ToDouble(inputFields[1]));
                }
                catch (Exception)
                {

                    // throw;
                }
            }

            // luk adgang til filen
            fileReader.Close();
            DTO_Raw nyMåling = new DTO_Raw(DateTime.Now, midlertidigePunkter.ToArray());

            return nyMåling;
        }
    }
}
