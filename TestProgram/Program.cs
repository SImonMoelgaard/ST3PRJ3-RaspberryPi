using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using DataAccessLogic;

namespace TestProgram
{
    class Program
    {
       static ReadFromFile read = new ReadFromFile();
       //List<blodtryk> test = new List<blodtryk>();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine(read.HentFraCsvFil());
            foreach (blodtryk blodtryk in read.HentFraCsvFil())
            {
               Console.WriteLine("Blodtrykket er "+blodtryk.mmHg + " til tiden " + blodtryk.Tid); 
            }
        }

        //public void getList()
        //{
        //    test = read.HentFraCsvFil();

        //}
    }
}
