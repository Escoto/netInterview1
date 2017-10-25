using System.Collections.Generic;
using System.Linq;
using testingApp.Models;


/*
 * The application is only going to run, non stop.
 * Go to the bin/debug folder to see output
 * 
 * I built a simple score validation system to check every person's viability to be a client.
 * With some mocked data I compare personsdata against data sets of roles, Indutries and Countries 
 * which have some weight assigned.
 * 
 */


namespace testingApp
{
    class Program
    {
        private const string smallFile = "peopleSmall.in";
        private const string largeFile = "peopleLarge.in";

        private static List<Person> people;
        private static readonly Dictionary<string, int> keyRoles = DataSets.getKeyRoles();
        private static readonly Dictionary<string, int> keyIndustries = DataSets.getKeyIndustries();
        private static readonly Dictionary<string, int> keyCountries = DataSets.getKeyCountries();

        private static void setPeople(bool large) {
            PeopleFileManager.PeopleInFile = large ? largeFile : smallFile ;
            people = PeopleFileManager.GetAll();
        }

        static void Main(string[] args)
        {
            //send true to work with the large test file.
            setPeople(large:true);

            //get the id-value sets we want to evaluate
            var peopleByRole = people.Select(p => new EvaluablePerson { PersonId = p.PersonId, Value = p.CurrentRole }).ToList();
            var peopleByIndustry = people.Select(p => new EvaluablePerson { PersonId = p.PersonId, Value = p.Industry }).ToList();
            var peopleByCountry = people.Select(p => new EvaluablePerson { PersonId = p.PersonId, Value = p.Country }).ToList();

            //evaluate id-value sets against key-score sets
            var byRoleScors     = evaluatePeople(peopleByRole, keyRoles);
            var byIndustryScors = evaluatePeople(peopleByIndustry, keyIndustries);
            var byCountryScors  = evaluatePeople(peopleByCountry, keyCountries);

            //sum person's scores and order fro high to low score
            var scoreMerge = byRoleScors
                .Select(brs => 
                    new PersonValue {
                        PersonId = brs.PersonId,
                        Score = brs.Score //role's score
                            + byIndustryScors.Where(bis => bis.PersonId == brs.PersonId).Select(bis => bis.Score).FirstOrDefault() //industry's score
                            + byCountryScors.Where(bcs => bcs.PersonId == brs.PersonId).Select(bcs => bcs.Score).FirstOrDefault() //country's score
                    })
                .OrderByDescending(x => x.Score)
                .Take(100);

            PeopleFileManager.WriteOutput(scoreMerge.Select(x => x.PersonId).ToList());
        }

        //Generic function to evaluate people against a data set to generate every persons score
        static private List<PersonValue> evaluatePeople(List<EvaluablePerson> people, Dictionary<string, int> dictionary) {
            return people.Select(p => new PersonValue {
                PersonId = p.PersonId,
                Score = dictionary.Where(d => p.Value.Contains(d.Key)).Sum(d => d.Value) //get every value from dictionary and sum it, for every disctionary's key found in person's value
            }).ToList();
        }
    }
}
