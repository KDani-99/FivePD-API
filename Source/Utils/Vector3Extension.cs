using System;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace FivePD.API.Utils
{
    
    static class Vector3Extension
    {
        /// <summary>
        /// Returns a random Vector3 relative to the specified position, given a set radius as distance
        /// </summary>
        /// Made by LukeD (Thank you!)
        /// <param name="pos"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        internal static Vector3 Around(this Vector3 pos, float radius) => pos + (RandomXy() * radius);

        /// <summary>
        /// Returns a normalized Vector3 given randomized X and Y coordinates
        /// </summary>
        /// Made by LukeD (Thank you!)
        /// <returns></returns>
        internal static Vector3 RandomXy()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            Vector3 vector = new Vector3(
                (float)rnd.NextDouble() - 0.5f,
                (float)rnd.NextDouble() - 0.5f,
                0.0f
            );

            vector.Normalize();

            return vector;
        }

        /// <summary>
        /// Returns the distance between this position the provided position
        /// </summary>
        internal static float DistanceTo(this Vector3 source, Vector3 target) => World.GetDistance(source, target);

        internal static Vector3 ApplyOffset(this Vector3 source, Vector3 offset) => new Vector3(source.X + offset.X, source.Y + offset.Y, source.Z + offset.Z);

        /// <summary>
        /// Returns the height of the ground at this position
        /// </summary>
        internal static float GroundHeight(this Vector3 source) => World.GetGroundHeight(source);

        /// <summary>
        /// Returns the street name at this position
        /// </summary>
        internal static string ClosestStreetName(this Vector3 source) => World.GetStreetName(source);

        /// <summary>
        /// Sets the player's waypoint to this position
        /// </summary>
        internal static void SetWaypointHere(this Vector3 source) => World.WaypointPosition = source;

        /// <summary>
        /// Returns the closest location relative to this position, that a ped can be placed on the side walk
        /// </summary>
        internal static Vector3 ClosestPedPlacement(this Vector3 source) => World.GetNextPositionOnSidewalk(source);

        /// <summary>
        /// Returns the closest location relative to this position, a car can be placed on the side of the road
        /// </summary>
        /// <param name="unoccupied">If true, only unoccupied spots will be searched</param>
        internal static Vector3 ClosestParkedCarPlacement(this Vector3 source, bool unoccupied = true) => World.GetNextPositionOnStreet(source, unoccupied);

        /// <summary>
        /// Returns the closest parked car relative to this position
        /// </summary>
        internal static Vehicle GetClosestParkedCar(this Vector3 source)
        {
            Vector3 location = ClosestParkedCarPlacement(source, false);

            if (location.IsZero)
            {
                return null;
            }

            RaycastResult raycast = World.RaycastCapsule(location.ApplyOffset(new Vector3(0.0f, 0.0f, 3.0f)), location, 3, (IntersectOptions)10);

            if (raycast.DitHitEntity == false || !raycast.HitEntity.Model.IsVehicle)
            {
                return null;
            }

            return (Vehicle)raycast.HitEntity;
        }
    }
}
