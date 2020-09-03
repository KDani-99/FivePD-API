using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FivePD.API.Utils
{
    /// <summary>
    /// A collection of utilities that make it easier to select or generate random values.
    /// </summary>
    public static class RandomUtils
    {
        /* This is reasonably slower than using new Random(), but since all callouts get loaded at the same time,
         * we shouldn't use the same seed (by default the system time is used, leading to very _bad_ randomization). 
         * Unfortunately, this isn't thread-safe, but that shouldn't be a problem with FivePD. */
        /// <summary>
        /// Random instance that you can use to get random values in your callouts. This random should generate better
        /// random numbers than when you create your own randoms in code. Also have a look at the <see cref="GetRandomNumber"/> method.
        /// </summary>
        public static Random Random = new Random(Guid.NewGuid().GetHashCode());

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
                    _pedHashList = EnumExtensions.EnumAsEnumerable<PedHash>().Where(pedHash => !Utilities.UnsupportedPedHashes.Contains(pedHash)).ToList();
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
                    _weaponHashList = EnumExtensions.EnumAsEnumerable<WeaponHash>().ToList();
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
                    _vehicleHashList = EnumExtensions.EnumAsEnumerable<VehicleHash>().ToList();
                }
                return _vehicleHashList;
            }
        }

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
        public static int GetRandomNumber(int start, int end) => Random.Next(start, end);

        /* Some shorthand helper methods that are useful for Callout development. */
        #region Helper methods
        /// <summary>
        /// Get a random <see cref="PedHash"/>.
        /// By default, this method will already exclude all skins that are incompatible with FivePD.
        /// </summary>
        public static PedHash GetRandomPed() => PedHashes.SelectRandom();

        /// <summary>
        /// Get a random <see cref="PedHash"/>. Entries in the exclusion list will be ignored. 
        /// By default, this method will already exclude all skins that are incompatible with FivePD.
        /// </summary>
        /// <param name="exclusions">The list of <see cref="PedHash"/> to ignore.</param>
        public static PedHash GetRandomPed(IEnumerable<PedHash> exclusions) => PedHashes.SelectRandom(exclusions);

        /// <summary>
        /// Get a random <see cref="WeaponHash"/>.
        /// </summary>
        public static WeaponHash GetRandomWeapon() => WeaponHashes.SelectRandom();

        /// <summary>
        /// Get a random <see cref="WeaponHash"/>. Entries in the exclusion list will be ignored.
        /// </summary>
        /// <param name="exclusions">The list of <see cref="WeaponHash"/> to ignore.</param>
        public static WeaponHash GetRandomWeapon(IEnumerable<WeaponHash> exclusions) => WeaponHashes.SelectRandom(exclusions);

        /// <summary>
        /// Get a random <see cref="VehicleHash"/>.
        /// </summary>
        public static VehicleHash GetRandomVehicle() => VehicleHashes.SelectRandom();

        /// <summary>
        /// Get a random <see cref="VehicleHash"/>. Entries in the exclusion list will be ignored.
        /// </summary>
        /// <param name="exclusions">The list of <see cref="VehicleHash"/> to ignore.</param>
        public static VehicleHash GetRandomVehicle(IEnumerable<VehicleHash> exclusions) => VehicleHashes.SelectRandom(exclusions);

        /// <summary>
        /// Get a random <see cref="VehicleHash"/> from the given vehicle class.
        /// </summary>
        /// <param name="vehicleClass">The class of vehicles you wish to select a random hash from.</param>
        public static VehicleHash GetRandomVehicle(VehicleClass vehicleClass) => GetVehicleHashesForClass(vehicleClass).SelectRandom();

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
            => vehicleClasses.SelectMany(vehicleClass => GetVehicleHashesForClass(vehicleClass)).SelectRandom();

        /// <summary>
        /// Retrieve all the <see cref="VehicleHash"/>es from a given <see cref="VehicleClass"/>.
        /// </summary>
        /// <param name="vehicleClass">The class of vehicles.</param>
        public static IEnumerable<VehicleHash> GetVehicleHashesForClass(VehicleClass vehicleClass)
             => VehicleHashes.Where(vh => Vehicle.GetModelClass(new Model(vh)) == vehicleClass);
        #endregion
    }
}
