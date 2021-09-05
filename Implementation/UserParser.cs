using FileParser.Interfaces;
using FileParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FileParser.Utils;
using System.Globalization;

namespace FileParser
{
    public class UserParser : IFileParser
    {
        UtilService util = new UtilService();
        public T ParseLine<T>(string line,char delimiter,int[] order,string fileToLoad,int noOfParameter)
        {
            try
            {
                //for M/d/YYYY format
                CultureInfo culture = new CultureInfo("en-US");
                string[] parts = line.Split(delimiter);
                //will check if count of delimiter is equal to expected count
                if (parts.Length != noOfParameter)
                    throw new Exception("File" + fileToLoad + " is not in correct format");
                User dbp = new User
                {
                    LastName = (parts[order[0]] ?? "").Trim(),
                    FirstName = (parts[order[1]] ?? "").Trim(),
                    MiddleNameInitial = order[2] > 0 ? (parts[order[2]] ?? "") : null,
                    Gender = util.getGender(parts[order[3]]),
                    DOB = util.getDate(parts[order[4]], culture),
                    FavoriteColor = (parts[order[5]] ?? "").Trim()
                };
                return (T)(object)dbp;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        //function to parse each individual file
        public List<T> ParseFile<T>(string filePath)
        {
            try
            {
                List<User> users = new List<User>();
                int[] order;//order field is used to specify the order of each field within file with respect to field within User class
                char delimiter = ' ';
                int noOfParameter = 0;
                String fileToLoad = String.Format(filePath);
                using (StreamReader r = new StreamReader(fileToLoad))
                {
                    string line;
                    //we have use filename as a way to identify the delimiter... so any filename which contain pipe will use | as delimiter respectively
                    //there can also be different way to identify delimiter...
                    if (fileToLoad.Contains("pipe"))
                    {
                        delimiter = '|';
                        order = new int[] { 0, 1, 2, 3, 5, 4 };
                        noOfParameter = 6;
                    }
                    else if (fileToLoad.Contains("comma"))
                    {
                        delimiter = ',';
                        order = new int[] { 0, 1, -1, 2, 4, 3 };
                        noOfParameter = 5;
                    }
                    else
                    {
                        delimiter = ' ';
                        order = new int[] { 0, 1, 2, 3, 4, 5 };
                        noOfParameter = 6;
                    }
                    while ((line = r.ReadLine()) != null)
                    {
                        User dbp = ParseLine<User>(line, delimiter, order, fileToLoad, noOfParameter);
                        users.Add(dbp);
                    }
                }
                return new List<T>(users as IEnumerable<T> ?? throw new InvalidOperationException());
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        //function to parse all the list of files
        public List<T> ParseFiles<T>(string[] filePath)
        {
            string[] customerBase = filePath;
            List<User> users = new List<User>();
            try
            {
                foreach (string customerFile in customerBase)
                {
                    //I've added try catch here so that if there is error in any specific file, ther process wont stop and will parse other file...
                    //However we can remove try catch so that if for any error in file, whole process will stop
                    try
                    {
                        List<User> dbp = ParseFile<User>(customerFile);
                        //if no error in parsing individual file, then add it to list of user
                        users.AddRange(dbp);
                        //added some more details so that there is info about each file parsing status
                        //however can be removed if we need the exact same output as shared in mail
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("File " + customerFile + " successfully loaded",Console.ForegroundColor);
                        Console.ResetColor();
                    }
                    catch(Exception e)
                    {
                        //added some more details so that there is info about each file parsing status
                        //however can be removed if we need the exact same output as shared in mail
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error parsing the file " +customerFile+"\nError Message: "+e.Message, Console.ForegroundColor);
                        Console.ResetColor();
                        
                    }
                }
                return new List<T>(users as IEnumerable<T> ?? throw new InvalidOperationException());
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        //function to parse directory
        public List<T> ParseDirectory<T>(string directoryPath)
        {
            try
            {
                //will get array for text file within specified directory
                string[] userFiles = Directory.GetFiles(directoryPath, "*.txt");
                if (userFiles != null && userFiles.Length > 0)
                    return ParseFiles<T>(userFiles);
                else
                    return null;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
