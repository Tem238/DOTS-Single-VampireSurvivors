using Unity.Entities;

namespace DOTS_Single.VampireSurvivors
{
    public struct DamageBufferElement : IBufferElementData
    {
        public int Value;
    }

    public struct DamageCoolDown : IComponentData
    {
        public float Value;
    }

    public struct AlreadyDamageBufferElement : IBufferElementData
    {
        public Entity Value;
    }
}
