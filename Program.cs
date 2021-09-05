using FileParser.Implementation;
using FileParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
namespace FileParser
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                UserParser parser = new UserParser();
                UserOutput output = new UserOutput();
                Console.WriteLine("Please enter folder path for file which contain user data : ");
                string FolderPath = Console.ReadLine();
                //List<User> listOfUsers = parser.ParseDirectory<User>(@"C:\Users\alpjain\Documents\input_files\def-method-code-test-input-files");
                Console.WriteLine("\n\nParsing the all the file within folder: " + FolderPath);
                List<User> listOfUsers = parser.ParseDirectory<User>(@FolderPath);
                if (listOfUsers != null)
                {
                    List<User> sorted;
                    Console.WriteLine("\n\n\n========== List of users sorted by Gender, Last name ========== \n");
                    sorted = listOfUsers.OrderBy(x => x.Gender).ThenBy(x => x.LastName).ToList();
                    output.DisplayUserDetails(sorted);
                    Console.WriteLine("\n\n\n========== List of users sorted by Birth date, Last name ==========\n");
                    sorted = listOfUsers.OrderBy(x => x.DOB).ThenBy(x => x.LastName).ToList();
                    output.DisplayUserDetails(sorted);
                    Console.WriteLine("\n\n\n========== List of users sorted by Last name descending ==========\n");
                    sorted = listOfUsers.OrderByDescending(x => x.LastName).ToList();
                    output.DisplayUserDetails(sorted); 
                }
                else
                {
                    Console.WriteLine("No file is present in specified folder");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
