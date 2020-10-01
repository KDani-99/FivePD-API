using CitizenFX.Core;
using System.Threading.Tasks;

namespace FivePD.API.Utils
{
    static class EntityExtensions
    {
        #region Blip
        public static void Sync(this Blip blip) => Utilities.SyncBlip(blip);
        public static void Sync(this Blip blip,float radius) => Utilities.SyncBlipWithRadius(blip,radius);
        public static void SyncDelete(this Blip blip) => Utilities.SyncBlipDelete(blip);
        #endregion

        #region Ped
        public static Task<PedData> GetData(this Ped ped) => Utilities.GetPedData(ped.NetworkId);
        public static void SetData(this Ped ped, PedData data) => Utilities.SetPedData(ped.NetworkId, data);
        public static Ped GetClosest(this Ped ped) => Utilities.GetClosestPed(ped);
        #endregion

        #region Vehicle
        public static Task<VehicleData> GetData(this Vehicle vehicle) => Utilities.GetVehicleData(vehicle.NetworkId);
        public static void SetData(this Vehicle vehicle, VehicleData data) => Utilities.SetVehicleData(vehicle.NetworkId, data);
        #endregion
    }
}
