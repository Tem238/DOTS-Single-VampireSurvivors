using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace DOTS_Single.VampireSurvivors{

    [BurstCompile]
    public partial struct PlayerMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            new PlayerMoveJob
            {
                DeltaTime = deltaTime
            }.Schedule();
        }

        [BurstCompile]
        public partial struct PlayerMoveJob : IJobEntity
        {
            public float DeltaTime;

            private void Execute(ref LocalTransform transform, in PlayerMoveInput playerInput, in Simulate simulate)
            {
                transform.Position.xz += playerInput.Value * 3.0f * DeltaTime;
                transform.Position.y = 0;
            }
        }
    }
}
