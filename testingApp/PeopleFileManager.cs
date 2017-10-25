using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using testingApp.Models;

namespace testingApp
{
    /*
     * File reader/writer for people's .in and .out files 
     * 
    */
    class PeopleFileManager
    {
        public static string PeopleInFile = "";
        public const string PeopleOutFile = "people.out";
        public static List<Person> GetAll() {

            var path = Path.Combine(Directory.GetCurrentDirectory(), PeopleInFile);
            List<Person> people = new List<Person>();

            using (var reader = new StreamReader(path)) {
                //read every line and separate it by |
                while (!reader.EndOfStream) 
                    people.Add(parser(reader.ReadLine().Split('|')));
            }

            return people;
        }

        public static void WriteOutput(List<long> people)
        {
            string printable = string.Join("\n", people.Select(n => n.ToString()).ToArray());
            var path = Path.Combine(Directory.GetCurrentDirectory(), PeopleOutFile);
            
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(printable);
            }
            
        }

        //when reading people from file, we need the data as an object
        private static Person parser(string[] personData) {

            int _PersonId;
            int _NumberOfRecommendations;
            int _NumberOfConnections;

            int.TryParse(personData[0].NoNull(), out _PersonId);
            int.TryParse(personData[6].NoNull(), out _NumberOfRecommendations);
            int.TryParse(personData[7].NoNull(), out _NumberOfConnections);

            return new Person() {
                PersonId = _PersonId,
                Name = personData[1].NoNull(),
                LastName = personData[2].NoNull(),
                CurrentRole = personData[3].NoNull(),
                Country = personData[4].NoNull(),
                Industry = personData[5].NoNull(),
                NumberOfRecommendations = _NumberOfRecommendations,
                NumberOfConnections = _NumberOfConnections,
            };
        }
    }

    //Some help for string validation
    public static class MyExtensions {
        public static string NoNull(this String str) => str ?? "";
    }
}
