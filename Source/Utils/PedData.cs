using System.Collections.Generic;
using CitizenFX.Core;

namespace FivePD.API.Utils
{
    public class PedData
    {
        public enum Drugs
        {
            Meth,
            Cocaine,
            Marijuana
        }
        public string FirstName;
        public string LastName;
        public string Warrant;
        public string License;
        public string DateOfBirth;
        public double BloodAlcoholLevel;
        public Drugs [] UsedDrugs;
        public Gender Gender;
        public int Age;
        public string Address;
        public List<Item> Items;
        public List<Violation> Violations;
    }
}
