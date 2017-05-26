using System;
using System.Collections.Generic;
using System.Linq;

namespace TechJobsConsole
{
    class Program                   //All code falls within classes in C# because it is object-oriented.
    {
        static void Main(string[] args)     
        {
            // Create two Dictionary vars to hold info for menu and data

            // Top-level menu options
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();    //Dict "actionChoices" where both key and value are strings
            actionChoices.Add("search", "Search");                                          //add key "search" with value "Search"
            actionChoices.Add("list", "List");                                              //add key "list" with value "List

            // Column options
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();    //Dict "columnChoices" where both key and value will be strings
            columnChoices.Add("core competency", "Skill");                                  //Add key/value pairs to columnChoices
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");                     //Welcome the user

            // Allow user to search/list until they manually quit with ctrl+c
            while (true)                                                                    //repeat all these steps until user kills prog
            {

                string actionChoice = GetUserSelection("View Jobs", actionChoices);             //create string with call to GUS method and pass in "View Jobs" and actionChoices

                if (actionChoice.Equals("list"))                                                //if user chooses list...
                {
                    string columnChoice = GetUserSelection("List", columnChoices);                  //Create string with GUS call using list and their choice of column

                    if (columnChoice.Equals("all"))                                                 //if they choose all...
                    {
                        PrintJobs(JobData.FindAll());                                                   //print list of everything
                    }
                    else                                                                            //if not...
                                        {
                        List<string> results = JobData.FindAll(columnChoice);                           //create strList results with list of desired column 

                        Console.WriteLine("\n*** All " + columnChoices[columnChoice] + " Values ***");  //Print heading for list
                        foreach (string item in results)                                                //For each string in results
                        {
                            Console.WriteLine(item);                                                    //Print it to console
                        }
                    }
                }
                else // choice is "search"                                                      //if not, then the choice is "search"...
                {
                    // How does the user want to search (e.g. by skill or employer)
                    string columnChoice = GetUserSelection("Search", columnChoices);                //str columnChoice 

                    // What is their search term?                                                   
                    Console.WriteLine("\nSearch term: ");                                           //request user search term
                    string searchTerm = Console.ReadLine();                                         //retrieve user search term

                    List<Dictionary<string, string>> searchResults;                                 //Declare list searchResults where each item is a dict

                    // Fetch results
                    if (columnChoice.Equals("all"))                                                 //if user picked all...
                    {
                        searchResults = JobData.FindByValue(searchTerm);     //Initialize searchResults list with method from JobData file user columnChoice and searchTerm as args
                        PrintJobs(searchResults);                                          
                    }
                    else                                                                            //if user picked something other than all...
                    {
                        searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);    
                        PrintJobs(searchResults);                                                   
                    }
                }
            }
        }

        /*
         * Returns the key of the selected item from the choices Dictionary
         */
        private static string GetUserSelection(string choiceHeader, Dictionary<string, string> choices) //GetUserSelection Class which will return a string and takes args choiceHeader and choices
        {
            int choiceIdx;                                                      //integer variable
            bool isValidChoice = false;                                         //bool set to false
            string[] choiceKeys = new string[choices.Count];                    //string array choiceKeys 

            int i = 0;                                                          //set i to 0
            foreach (KeyValuePair<string, string> choice in choices)            //iterate through each kvp in choices dict
            {
                choiceKeys[i] = choice.Key;                                         //??????Make list of choice keys???????
                i++;                                                                
            }

            do                                                                  //start do-while loop
            {                                                                   
                Console.WriteLine("\n" + choiceHeader + " by:");                //print list heading using user choice

                for (int j = 0; j < choiceKeys.Length; j++)                     //iterate from 0 to choiceKeys.Length by 1
                {
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);          //Print number of choice + "-" + the choice option
                }

                string input = Console.ReadLine();                              //create input string from user input
                choiceIdx = int.Parse(input);                                   //converts input string to int 

                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)            //throw error if they choose a number not on the list
                {
                    Console.WriteLine("Invalid choices. Try again.");               //print this error
                }
                else
                {
                    isValidChoice = true;                                           //if choice is valid then set bool to True
                }

            } while (!isValidChoice);                                           //rerun the method as long as false.  Ends do-while.

            return choiceKeys[choiceIdx];                                       //return the users choice    
        }

        private static void PrintJobs(List<Dictionary<string, string>> someJobs)
        {
            if (someJobs.Any() == false)
            {
                Console.WriteLine("There are no jobs fitting this description.");
            }

            else
            {

                //for each dict in the list...foreach (Dictionary<string, string> job in AllJobs) 
                foreach (Dictionary<string, string> job in someJobs)
                {
                    Console.WriteLine("*****");
                    //for each KVP in the dict
                    foreach (KeyValuePair<string, string> criteria in job)
                    {
                        //Print key: value
                        Console.WriteLine("{0}: {1}", criteria.Key, criteria.Value);
                    }
                    Console.WriteLine("*****");
                }
            }


        }
    }
}
