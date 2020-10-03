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

        public class License
        {
            public enum Status
            {
                Valid = 0,
                Expired = 1,
                Revoked = 2,
                Suspended = 3
            }

            public Status LicenseStatus;
            public string ExpirationDate;
        }

        public string FirstName;
        public string LastName;
        public string Warrant;
        public License DriverLicense;
        public License HuntingLicense;
        public License FishingLicense;
        public License WeaponLicense;
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
