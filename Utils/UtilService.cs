using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FileParser.Utils
{
    class UtilService
    {
        public string getGender(string gender)
        {
            if (gender != null && gender != "")
            {
                string temp = gender.Trim().ToLower();
                if (temp == "m" || temp == "male")
                    return "Male";
                else if (temp == "f" || temp == "female")
                    return "Female";
                else return gender;
            }
            else return gender;
        }
        public DateTime getDate(string pDate, CultureInfo culture)
        {
            try
            {
                if (pDate != null && pDate != "")
                {
                    return Convert.ToDateTime((pDate).Trim(), culture);
                }
                else
                {
                    throw new Exception("Date is not present");
                }
            }
            catch(Exception e)
            {
                throw new Exception("Date is not in correct format...Expected date format is " + culture.DateTimeFormat.ShortDatePattern);
            }
        }
    }
}
