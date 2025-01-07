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
                // �������Z�ɂ���]�̖������F[0], [1], [2] �͂��ꂼ�� X, Y, Z ��
                physicsMass.ValueRW.InverseInertia[0] = 0;
                physicsMass.ValueRW.InverseInertia[1] = 0;
                physicsMass.ValueRW.InverseInertia[2] = 0;

                // �J��Ԃ��Ȃ��悤��NewPlayerTag�͍폜
                ecb.RemoveComponent<NewPlayerTag>(newPlayerEntity);
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}