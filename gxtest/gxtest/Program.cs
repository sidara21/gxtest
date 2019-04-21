using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace gxtest
{
    public class Name : IComparable
    {
        // Firstname can be between one and three words, words are separated by a space
        public string Firstnames;
        // Surname must be a single word, but can contain hyphens
        public string Surname;

        // Implement a comparison function for the List to use
        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Argument should not be null");
            Name compName = obj as Name;
            if (compName == null)
                throw new ArgumentException("Object to compare is not a name");

            if (this.Surname.Equals(compName.Surname))
            {
                // surnames are the same - test first names
                return this.Firstnames.CompareTo(compName.Firstnames);
            }
            else
                return this.Surname.CompareTo(compName.Surname);
        }

        public void Print(System.IO.StreamWriter sw)
        {
            if (sw == null)
                Console.WriteLine(Firstnames + " " + Surname);
            else
                sw.WriteLine(Firstnames + " " + Surname);
        }

    }

    // using an List as memory for the list will be dynamically allocated
    // saves wasting 1MB+ when there are only 10 names in the list
    public class NameList : List<Name>
    {
        public void ParseFile(string[] lines)
        {
            if (lines == null)
                throw new ArgumentNullException("Parameter 'lines' was null");

            foreach (string line in lines)
            {
                // remove leading and trailing whitespace
                line.Trim();

                // everything after the last space character is now the surname
                // split by space
                string[] names = line.Split(" ");

                // check we have at least two words in the line
                if (names.Length < 2)
                    throw new FormatException("Names must contain at least a first name and a surname");

                Name newName = new Name();
                newName.Surname = names[names.Length - 1];

                // couple of choices here - could use the original string to make the firstname string
                // (taking out the surname) or use the first n members of the names array
                
                // do the latter as it also removes the situation of multiple spaces between words
                for (int i = 0; i < names.Length - 1; i++)
                {
                    if (i != 0)
                        newName.Firstnames += " ";
                    newName.Firstnames += names[i];
                }
                Add(newName);
            }
        }

        public void PrintList(System.IO.StreamWriter sw = null)
        {
            foreach (Name thisName in this)
            {
                PrintName(thisName, sw);
            }
        }

        public void PrintName(Name thisname, System.IO.StreamWriter sw)
        {
            thisname.Print(sw);
        }



    }

    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length != 1)
            {
                throw new ArgumentException("One argument only must be passed - the name of the input file");
            }

            NameList nameList = new NameList();
            if (!File.Exists(args[0]))
            {
                throw new FileNotFoundException("File " + args[0] + " was not found");
            }

            string[] lines = File.ReadAllLines(args[0]);
            nameList.ParseFile(lines);

            nameList.Sort();

            nameList.PrintList();

            // write the list to sorted-names-list.txt
            System.IO.StreamWriter sw = new System.IO.StreamWriter("sorted-names-list.txt");          
            nameList.PrintList(sw);
            sw.Close();
        }

    }
}

    
