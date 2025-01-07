using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace DOTS_Single.VampireSurvivors
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    public partial struct PlayerInitializeSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<NewPlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (physicsMass, physicsVelocity, newPlayerEntity) in SystemAPI.Query<RefRW<PhysicsMass>, RefRW<PhysicsVelocity>>().WithAny<NewPlayerTag>().WithEntityAccess())
            {
                // ï®óùââéZÇ…ÇÊÇÈâÒì]ÇÃñ≥å¯âªÅF[0], [1], [2] ÇÕÇªÇÍÇºÇÍ X, Y, Z é≤
                physicsMass.ValueRW.InverseInertia[0] = 0;
                physicsMass.ValueRW.InverseInertia[1] = 0;
                physicsMass.ValueRW.InverseInertia[2] = 0;

                // åJÇËï‘Ç≥Ç»Ç¢ÇÊÇ§Ç…NewPlayerTagÇÕçÌèú
                ecb.RemoveComponent<NewPlayerTag>(newPlayerEntity);
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}