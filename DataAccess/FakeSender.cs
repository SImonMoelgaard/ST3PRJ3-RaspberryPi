using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DTO_s;

namespace DataAccessLogic
{
    class FakeSender : ISender

{
    public void SendDTO_Calculated(DTO_Calculated dtoCalculated)
    {
        // Folder, where a file is created.  
        // Make sure to change this folder to your own folder  
        string folder = @"C:";
        // Filename  
        string fileName = "FakeSender.txt";
        // Fullpath. You can direct hardcode it if you like.  
        string fullPath = folder + fileName;
        // An array of strings  
        string[] authors = {"Mahesh Chand", "Allen O'Neill", "David McCarter",
            "Raj Kumar", "Dhananjay Kumar"};
        // Write array of strings to a file using WriteAllLines.  
        // If the file does not exists, it will create a new file.  
        // This method automatically opens the file, writes to it, and closes file  
        File.WriteAllLines(fullPath, authors);
        // Read a file  
        string readText = File.ReadAllText(fullPath);
        Console.WriteLine(readText); 
    }

    public void SendDTO_Raw(List<DTO_Raw> dtoRaw)
    {
        throw new NotImplementedException();
    }
}
}
