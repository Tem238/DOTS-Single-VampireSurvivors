using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace DOTS_Single.VampireSurvivors
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    public partial struct EnemyInitializeSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<NewEnemyTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (physicsMass, physicsVelocity, newEnemyEntity) in SystemAPI.Query<RefRW<PhysicsMass>, RefRW<PhysicsVelocity>>().WithAny<NewEnemyTag>().WithEntityAccess())
            {
                // 物理演算による回転の無効化：[0], [1], [2] はそれぞれ X, Y, Z 軸
                physicsMass.ValueRW.InverseInertia[0] = 0;
                physicsMass.ValueRW.InverseInertia[1] = 0;
                physicsMass.ValueRW.InverseInertia[2] = 0;

                // 繰り返さないようにNewEnemyTagは削除
                ecb.RemoveComponent<NewEnemyTag>(newEnemyEntity);
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}