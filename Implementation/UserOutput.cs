using FileParser.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileParser.Implementation
{
    class UserOutput
    {
        public void DisplayUserDetails(List<User> listOfUsers)
        {
            foreach (var user in listOfUsers)
            {
                Console.WriteLine(user.LastName + " " + user.FirstName + " " + user.Gender + " " + user.DOB.ToString("M/d/yyyy") + " " + user.FavoriteColor);
            }
        }
    }
}
