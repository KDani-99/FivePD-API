using CitizenFX.Core;

namespace FivePD.API
{
    public enum PursuitStateEnum
    {
        Awaiting,
        Active,
        Finished
    }
    public interface IPursuit<PursuitStateEnum>
    {
        int Handle { get; }

        bool IsInPursuit { get; }
        bool IsVehiclePursuit { get; }

        Ped Suspect { get; }
        Ped FleeingFrom { get; }

        PursuitStateEnum PursuitState { get; }

        void Init(bool vehiclePursuit, float fleeDistance, float areaRadius, bool suspectBlip = true);
        void ActivatePursuit();
        void Terminate();
    }
}
