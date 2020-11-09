using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using DataAccessLogic;
using DTO_s;

namespace TestProgram
{
    class Program
    {
       private static ReadFromFile read = new ReadFromFile();

       //List<blodtryk> test = new List<blodtryk>();
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello World!");

            Console.WriteLine(read.HentFraCsvFil());


            //foreach (DTO_Raw dtoRaw in read.HentFraCsvFil())
            //{
            //    Console.WriteLine("Blodtrykket er " + dtoRaw.mmHg + " til tiden " + dtoRaw.Tid);
            //}

        }

        //public void getList()
        //{
        //    test = read.HentFraCsvFil();

        //}
    }
}
