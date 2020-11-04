using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    public class ReadFromFile
    {
        //private double midlertidigePunkter;
        //private DateTime midlertidigTid;
        private List<blodtryk> liste = new List<blodtryk>();

        public ReadFromFile()
        {
        }

        //private List<DateTime> midlertidigTid;
        //List<double> midlertidigePunkter;
        public List<blodtryk> HentFraCsvFil()
        {
          //  midlertidigTid = new DateTime();
            //midlertidigePunkter = new double();

            //// string-objekter til at gemme det som læses fra filen
            //string inputRecord;
            //string[] inputFields;


            //// opret de nødvendige stream-objekter
            //// FileStream input = new FileStream("Test_Atrieflimmer_1.csv", FileMode.OpenOrCreate, FileAccess.Read);
            //FileStream input = new FileStream(FilNavn, FileMode.OpenOrCreate, FileAccess.Read);

            //StreamReader fileReader = new StreamReader(input);


               // read file
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\Sample.txt");


            foreach (string line in lines)
            {
                // split in time and mmhg
                string[] splitLine = line.Split(',');
                string tid = splitLine[0];
                DateTime dateTime = DateTime.ParseExact(tid, "s.fff", System.Globalization.CultureInfo.InvariantCulture);
                //DateTime tidTime = Convert.ToDateTime(tid);

                string mmhg = splitLine[1];

                double mmhgAsDouble = Convert.ToDouble(mmhg)/1000;
                //DateTime tidDateTime = Convert.ToDateTime(dateTime);

                // create blodtryks object
                blodtryk b = new blodtryk(mmhgAsDouble, dateTime);
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
            }

            
        }
    

