using System.Collections.Generic;

namespace testingApp
{
    class DataSets
    {
        /*
         * Some Random mock data to evaluate people
         * 
         */

        public static Dictionary<string, int> getKeyRoles() {
            Dictionary<string, int> values = new Dictionary<string, int>();
            values.Add("ceo",3);
            values.Add("co-founder", 3);
            values.Add("cto", 3);
            values.Add("vice", 2);
            values.Add("president", 2);
            values.Add("chief", 3);
            values.Add("director", 1); 
            values.Add("executive", 2);
            values.Add("financial", 3);
            values.Add("officer", 2);
            values.Add("vp", 2);
            values.Add("board", 1);
            values.Add("account", 2);
            values.Add("manager", 1);

            return values;
        }

        public static Dictionary<string, int> getKeyIndustries()
        {
            Dictionary<string, int> values = new Dictionary<string, int>();
            values.Add("Business", 1);
            values.Add("Services", 2);
            values.Add("Insurance", 3);
            values.Add("Management", 2);
            values.Add("Banking", 4);
            values.Add("Financial", 4);
            values.Add("Investment", 3);
            values.Add("Manufacturing", 3);
            values.Add("Publishing", 1);
            values.Add("Automobiles", 2);
            values.Add("Marketing", 1);
            values.Add("Hospitality", 2);
            values.Add("Construction", 3);

            return values;
        }

        public static Dictionary<string, int> getKeyCountries()
        {
            Dictionary<string, int> values = new Dictionary<string, int>();
            values.Add("argentina", 3);
            values.Add("united", 1);
            values.Add("states", 2);
            values.Add("kingdom", 2);
            values.Add("Canada", 1);
            values.Add("España", 15);

            return values;
        }
    }
}
