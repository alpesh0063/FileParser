using System;
using System.Collections.Generic;
using System.Text;

namespace FileParser.Model
{
    class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleNameInitial { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string FavoriteColor { get; set; }
        public string FileName { get; set; }
    }
}
