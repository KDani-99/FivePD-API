using CitizenFX.Core;
using System;

namespace FivePD.API.Utils
{
    // Made by LukeD (Thank you!)
    static class Vector3Extension
    {
        /// <summary>
        /// Returns a random Vector3 relative to the specified position, given a set radius as distance
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        internal static Vector3 Around(this Vector3 pos, float radius)
        {
            var direction = RandomXy();
            var around = pos + (direction * radius);
            return around;
        }

        /// <summary>
        /// Returns a normalized Vector3 given randomized X and Y coordinates
        /// </summary>
        /// <returns></returns>
        private static Vector3 RandomXy()
        {
            var rnd = new Random(Environment.TickCount);

            var v3 = new Vector3();
            v3.X = (float)rnd.NextDouble() - 0.5f;
            v3.Y = (float)rnd.NextDouble() - 0.5f;
            v3.Z = 0.0f;
            v3.Normalize();
            return v3;
        }

    }
}
