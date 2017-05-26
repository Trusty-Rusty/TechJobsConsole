using System.Collections.Generic;           //classes/methods for data collections
using System.IO;                            //classes/methods for reading and writing to other files
using System.Reflection;                    //classes/methods for ?????????
using System.Text;                          //classes/methods for dealing with ASCII char and strings

namespace TechJobsConsole
{
    class JobData                                                   //class that contains all text in file
    {
        static List<Dictionary<string, string>> AllJobs = new List<Dictionary<string, string>>();   //List of dict where each is a job...static b/c it refers to entire class
        static bool IsDataLoaded = false;                           //bool object set to false

        public static List<Dictionary<string, string>> FindAll()    //method that can be seen outside the JobData class and exists identically for all instances    
        {
            LoadData();                                                 //execute LoadData method
            return AllJobs;                                             
        }

        /*
         * Returns a list of all values contained in a given column,
         * without duplicates. 
         */
        public static List<string> FindAll(string column)           //Method that takes string and returns a list of strings for all jobs in a column
        {
            LoadData();                                                 //Call LoadData func

            List<string> values = new List<string>();                   //New list with string values

            foreach (Dictionary<string, string> job in AllJobs)         //for each job in AllJobs
            {
                string aValue = job[column];                                //New string that contains the value for a certain row of a  certain column

                if (!values.Contains(aValue))                               //If values does not contain aValue...
                {
                    values.Add(aValue);                                         //add aValue to values
                }
            }
            return values;                                              //return values
        }

        public static List<Dictionary<string, string>> FindByColumnAndValue(string column, string value)    //??????????????????????
        {
            // load data, if not already loaded
            LoadData();

            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>(); //Create list of dict jobs

            foreach (Dictionary<string, string> row in AllJobs)     //for each dict in list
            {
                string aValue = row[column];                            //new string with value of a certain column and certain row
                string upperaValue = aValue.ToUpper();
                string upperValue = value.ToUpper();
                
                if (upperaValue.Contains(upperValue))                             
                {
                    jobs.Add(row);
                }
            }

            return jobs;
        }

        /*
         * Load and parse data from job_data.csv
         */
        private static void LoadData()                              //Method to load and parse data from the .CSV
        {

            if (IsDataLoaded)                                           //Bool from line 11...Return if true 
            {
                return;
            }

            List<string[]> rows = new List<string[]>();                 //Create a list of string arrays where each array of strings is a row.

            using (StreamReader reader = File.OpenText("job_data.csv")) //Use some outside method to access the .CSV??????????????????
            {
                while (reader.Peek() >= 0)                                  //Use .Peek to check for end of .CSV file        
                {
                    string line = reader.ReadLine();                            //Create a string of char read from the .CSV
                    string[] rowArrray = CSVRowToStringArray(line);             //Use method from 108 to parse CSV row to string array
                    if (rowArrray.Length > 0)                                   //If the array has any char...
                    {                   
                        rows.Add(rowArrray);                                        //...then add it to rows
                    }
                }
            }

            string[] headers = rows[0];                                 //Create string array headers that contains the first line of rows????????????
            rows.Remove(headers);                                       //Remove first line (headers) from rows???????????????

            // Parse each row array into a more friendly Dictionary
            foreach (string[] row in rows)                              //For each string array in rows...
            {
                Dictionary<string, string> rowDict = new Dictionary<string, string>();  //Create a dict (for each row) where K and V are strings

                for (int i = 0; i < headers.Length; i++)                    //Iterate through each heading in headers
                {
                    rowDict.Add(headers[i], row[i]);                            //NOT WORKING DOWN ROWS...Pairs each heading KEY with VALUE data from that row 
                }
                AllJobs.Add(rowDict);                                       //Add this dict of single job to AllJobs list
            }

            IsDataLoaded = true;                                        //After AllJobs is created, set IsDataLoaded to true
        }

        /*
         * Parse a single line of a CSV file into a string array
         */
        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',', char stringSeparator = '\"') //method takes CSV row and separates by "," and creates string array
        {
            bool isBetweenQuotes = false;                               //Bool set to false
            StringBuilder valueBuilder = new StringBuilder();           //Declare new instance of SB called valueBuilder
            List<string> rowValues = new List<string>();                //Create a list of strings rowValue

            // Loop through the row string one char at a time
            foreach (char c in row.ToCharArray())                       //For each char in an array made from row
            {
                if ((c == fieldSeparator && !isBetweenQuotes))              //If the char is , and not between quotes...
                {
                    rowValues.Add(valueBuilder.ToString());                     //Add a row number???????????????????
                    valueBuilder.Clear();                                       //Remove all char from the StringBuilder
                }
                else                                                        //if not...
                {
                    if (c == stringSeparator)                                   //if char == string separator (but not in quotes)
                    {
                        isBetweenQuotes = !isBetweenQuotes;                         //??????Set bool to false or opposite of what it was
                    }
                    else                                                        //if char not string separator...
                    {
                        valueBuilder.Append(c);                                     //Add the char to the string
                    }
                }
            }

            // Add the final value
            rowValues.Add(valueBuilder.ToString());                     //Add the StringBuilder string to rowValues list
            valueBuilder.Clear();                                       //Clear the StringBuilder

            return rowValues.ToArray();                                 //return an array version of the list????????
        }

        public static List<Dictionary<string, string>> FindByValue(string value)    //
        {
            // load data, if not already loaded
            LoadData();

            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>(); //Create list of dict jobs

            foreach (Dictionary<string, string> row in AllJobs)     //for each dict in list
            {

                foreach (KeyValuePair<string, string> criteria in row)
                {  //for each KVP in the dict

                    string upperCriteria = criteria.Value.ToUpper();
                    string upperValue = value.ToUpper();

                    if (upperCriteria.Contains(upperValue))                               //if value matches the dict value
                    {
                        jobs.Add(row);                                          //add the jobs to jobs list<dict..>                                                                                     
                    }
                }
            }

            return jobs;
        }
    }   
}
