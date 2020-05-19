using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;

namespace CalloutAPI
{
    /// <summary>
    /// A collection of utilities that make it easier to select or generate random values.
    /// </summary>
    public static class CalloutUtils
    {
        /* This is reasonably slower than using new Random(), but since all callouts get loaded at the same time,
         * we shouldn't use the same seed (by default the system time is used, leading to very _bad_ randomization). 
         * Unfortunately, this isn't thread-safe, but that shouldn't be a problem with FivePD. */
        private static Random _random = new Random(Guid.NewGuid().GetHashCode());

        /// <summary>
        /// A list of PedHashes that do not work with FivePD.
        /// </summary>
        public readonly PedHash[] UnsupportedPedHashes =
        {
            PedHash.Humpback,
            PedHash.Dolphin,
            PedHash.KillerWhale,
            PedHash.Fish,
            PedHash.HammerShark,
            PedHash.TigerShark,
            PedHash.Boar,
            PedHash.Cat,
            PedHash.ChickenHawk,
            PedHash.Chimp,
            PedHash.Coyote,
            PedHash.Cow,
            PedHash.Deer,
            PedHash.Pig,
            PedHash.Rabbit,
            PedHash.Crow,
            PedHash.Cormorant,
            PedHash.Husky,
            PedHash.Rottweiler,
            PedHash.Pug,
            PedHash.Poodle,
            PedHash.Retriever,
            PedHash.Seagull,
            PedHash.Pigeon,
            PedHash.MountainLion,
            PedHash.BradCadaverCutscene,
            PedHash.Chop,
            PedHash.Hen,
            PedHash.JohnnyKlebitz,
            PedHash.LamarDavisCutscene,
            PedHash.MagentaCutscene,
            PedHash.Marston01,
            PedHash.Misty01,
            PedHash.MovAlien01,
            PedHash.MoviePremFemaleCutscene,
            PedHash.MoviePremMaleCutscene,
            PedHash.MrsPhillipsCutscene,
            PedHash.MrKCutscene,
            PedHash.NataliaCutscene,
            PedHash.NigelCutscene,
            PedHash.NervousRonCutscene,
            PedHash.Niko01,
            PedHash.PaigeCutscene,
            PedHash.OscarCutscene,
            PedHash.OrtegaCutscene,
            PedHash.OrleansCutscene,
            PedHash.Orleans,
            PedHash.Pogo01,
            PedHash.Rat,
            PedHash.Rhesus,
            PedHash.Stingray,
            PedHash.SteveHainsCutscene,
            PedHash.Westy
        };

        private static List<PedHash> _pedHashList;

        /// <summary>
        /// A list of all entries in the <see cref="PedHash"/> enum, minus the ones that break FivePD.
        /// </summary>
        public static List<PedHash> PedHashes
        {
            get
            {
                if (_pedHashList == null)
                {
                    _pedHashList = EnumAsEnumerable<PedHash>().ToList().Where(pedHash => !UnsupportedPedHashes.Contains(pedHash));
                }
                return _pedHashList;
            }
        }

        private static List<WeaponHash> _weaponHashList;

        /// <summary>
        /// A list of all entries in the <see cref="WeaponHash"/> enum.
        /// </summary>
        public static List<WeaponHash> WeaponHashes
        {
            get
            {
                if (_weaponHashList == null)
                {
                    _weaponHashList = EnumAsEnumerable<WeaponHash>().ToList();
                }
                return _weaponHashList;
            }
        }

        private static List<VehicleHash> _vehicleHashList;

        /// <summary>
        /// A list of all entries in the <see cref="VehicleHash"/> enum.
        /// </summary>
        public static List<VehicleHash> VehicleHashes
        {
            get
            {
                if (_vehicleHashList == null)
                {
                    _vehicleHashList = EnumAsEnumerable<VehicleHash>().ToList();
                }
                return _vehicleHashList;
            }
        }

        /// <summary>
        /// Get all values of <typeparamref name="TEnum"/> in an iterable collection.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum</typeparam>
        public static IEnumerable<TEnum> EnumAsEnumerable<TEnum>() where TEnum : struct
            => Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

        /// <summary>
        /// Retrieve a random element from the provided list.
        /// </summary>
        /// <param name="enumerable">The collection of elements.</param>
        public static T RandomFromList<T>(IEnumerable<T> enumerable)
            => RandomFromList(enumerable, 0);

        /// <summary>
        /// Retrieve a random element from the provided list.
        /// </summary>
        /// <param name="enumerable">The collection of elements.</param>
        /// <param name="exclusions">The elements you don't want to include in the result.</param>
        public static T RandomFromList<T>(IEnumerable<T> enumerable, IEnumerable<T> exclusions)
            => RandomFromList(enumerable, 0, exclusions);

        /// <summary>
        /// Retrieve a random element from the provided list (with exceptions).
        /// </summary>
        /// <param name="enumerable">The collection of elements.</param>
        /// <param name="skip">The amount of elements at the beginning of the collection to skip.</param>
        /// <param name="exclusions">The elements you don't want to include in the result.</param>
        public static T RandomFromList<T>(IEnumerable<T> enumerable, int skip, IEnumerable<T> exclusions)
            => RandomFromList(enumerable.Where(element => !exclusions.Contains(element)), skip);

        /// <summary>
        /// Retrieve a random element from the provided list, allowing you to skip the specified number of elements from the start.
        /// </summary>
        /// <param name="enumerable">The collection of elements.</param>
        /// <param name="skip">The amount of elements at the beginning of the collection to skip.</param>
        public static T RandomFromList<T>(IEnumerable<T> enumerable, int skip)
            => enumerable.ElementAt(GetRandomNumber(skip, enumerable.Count()));

        /// <summary>
        /// Select a random number in the specified range. <paramref name="start"/> is inclusive, <paramref name="end"/> exclusive
        /// (so the number that is passed for <paramref name="start"/> can also be returned, but <paramref name="end"/> cannot).<br /><br />
        /// <example>
        /// To generate an inclusive number between 1 and 100, you would use the following code:
        /// <code>
        /// var myRandomNumber = GetRandomNumber(1, 101);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="start">The inclusive minimum value</param>
        /// <param name="end">The exclusive maximum value.</param>
        /// <returns>A number between <paramref name="start"/> and <paramref name="end"/> (could be equal to <paramref name="start"/>)</returns>
        public static int GetRandomNumber(int start, int end) => _random.Next(start, end);

        /* Some shorthand helper methods that are useful for Callout development. */
        #region Helper methods
        /// <summary>
        /// Get a random <see cref="PedHash"/>.
        /// </summary>
        public static PedHash GetRandomPed() => RandomFromList(PedHashes);

        /// <summary>
        /// Get a random <see cref="PedHash"/>. Entries in the exclusion list will be ignored.
        /// </summary>
        /// <param name="exclusions">The list of <see cref="PedHash"/> to ignore.</param>
        public static PedHash GetRandomPed(IEnumerable<PedHash> exclusions) => RandomFromList(PedHashes, exclusions);

        /// <summary>
        /// Get a random <see cref="WeaponHash"/>.
        /// </summary>
        public static WeaponHash GetRandomWeapon() => RandomFromList(WeaponHashes);

        /// <summary>
        /// Get a random <see cref="WeaponHash"/>. Entries in the exclusion list will be ignored.
        /// </summary>
        /// <param name="exclusions">The list of <see cref="WeaponHash"/> to ignore.</param>
        public static WeaponHash GetRandomWeapon(IEnumerable<WeaponHash> exclusions) => RandomFromList(WeaponHashes, exclusions);

        /// <summary>
        /// Get a random <see cref="VehicleHash"/>.
        /// </summary>
        public static VehicleHash GetRandomVehicle() => RandomFromList(VehicleHashes);

        /// <summary>
        /// Get a random <see cref="VehicleHash"/>. Entries in the exclusion list will be ignored.
        /// </summary>
        /// <param name="exclusions">The list of <see cref="VehicleHash"/> to ignore.</param>
        public static VehicleHash GetRandomVehicle(IEnumerable<VehicleHash> exclusions) => RandomFromList(VehicleHashes, exclusions);

        /// <summary>
        /// Get a random <see cref="VehicleHash"/> from the given vehicle class.
        /// </summary>
        /// <param name="vehicleClass">The class of vehicles you wish to select a random hash from.</param>
        public static VehicleHash GetRandomVehicle(VehicleClass vehicleClass) => RandomFromList(GetVehicleHashesForClass(vehicleClass));

        /// <summary>
        /// Get a random <see cref="VehicleHash"/> from the given vehicle classes.
        /// </summary>
        /// <param name="vehicleClasses">The classes of vehicles you wish to select a random hash from.</param>
        public static VehicleHash GetRandomVehicle(params VehicleClass[] vehicleClasses) => GetRandomVehicle(vehicleClasses.ToList());

        /// <summary>
        /// Get a random <see cref="VehicleHash"/> from the given vehicle classes.
        /// </summary>
        /// <param name="vehicleClasses">The classes of vehicles you wish to select a random hash from.</param>
        public static VehicleHash GetRandomVehicle(IEnumerable<VehicleClass> vehicleClasses)
            => RandomFromList(vehicleClasses.SelectMany(vehicleClass => GetVehicleHashesForClass(vehicleClass)));

        /// <summary>
        /// Retrieve all the <see cref="VehicleHash"/>es from a given <see cref="VehicleClass"/>.
        /// </summary>
        /// <param name="vehicleClass">The class of vehicles.</param>
        public static IEnumerable<VehicleHash> GetVehicleHashesForClass(VehicleClass vehicleClass)
             => VehicleHashes.Where(vh => Vehicle.GetModelClass(new Model(vh)) == vehicleClass);
        #endregion
    }
}
