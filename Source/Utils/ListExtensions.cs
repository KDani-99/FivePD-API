using System.Collections.Generic;
using System.Linq;

namespace FivePD.API.Utils
{
    public static class ListExtensions
    {
        /// <summary>
        /// Retrieve a random element from the provided list.
        /// </summary>
        /// <param name="enumerable">The collection of elements.</param>
        public static T SelectRandom<T>(this IEnumerable<T> enumerable)
            => SelectRandom(enumerable, 0);

        /// <summary>
        /// Retrieve a random element from the provided list.
        /// </summary>
        /// <param name="enumerable">The collection of elements.</param>
        /// <param name="exclusions">The elements you don't want to include in the result.</param>
        public static T SelectRandom<T>(this IEnumerable<T> enumerable, IEnumerable<T> exclusions)
            => SelectRandom(enumerable, 0, exclusions);

        /// <summary>
        /// Retrieve a random element from the provided list (with exceptions).
        /// </summary>
        /// <param name="enumerable">The collection of elements.</param>
        /// <param name="skip">The amount of elements at the beginning of the collection to skip.</param>
        /// <param name="exclusions">The elements you don't want to include in the result.</param>
        public static T SelectRandom<T>(this IEnumerable<T> enumerable, int skip, IEnumerable<T> exclusions)
            => SelectRandom(enumerable.Where(element => !exclusions.Contains(element)), skip);

        /// <summary>
        /// Retrieve a random element from the provided list, allowing you to skip the specified number of elements from the start.
        /// </summary>
        /// <param name="enumerable">The collection of elements.</param>
        /// <param name="skip">The amount of elements at the beginning of the collection to skip.</param>
        public static T SelectRandom<T>(this IEnumerable<T> enumerable, int skip)
            => enumerable.ElementAt(RandomUtils.GetRandomNumber(skip, enumerable.Count()));
    }
}