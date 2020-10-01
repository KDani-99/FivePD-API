using CitizenFX.Core;

namespace FivePD.API
{
    public abstract class Pursuit
    {
        public delegate IPursuit<PursuitStateEnum> RegisterPursuitDelegate(Ped suspect);
        public static RegisterPursuitDelegate RegisterPursuit;

        public delegate IPursuit<PursuitStateEnum>[] GetPursuitsByStateDelegate(PursuitStateEnum pursuitState);
        public static GetPursuitsByStateDelegate GetPursuitsByState;

        public delegate IPursuit<PursuitStateEnum> GetPursuitByHandleDelegate(int handle);
        public static GetPursuitByHandleDelegate GetPursuitByHandle;
    }
    
}
