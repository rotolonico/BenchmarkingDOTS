using Unity.Entities;

namespace Scenarios.DOTS.FrustumCulling
{
    public struct FrustumCullingDisablerTag : IComponentData
    {
        public bool Initialized;
    }
}