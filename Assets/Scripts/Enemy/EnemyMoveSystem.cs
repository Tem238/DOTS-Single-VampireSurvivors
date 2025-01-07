using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace DOTS_Single.VampireSurvivors
{
    [BurstCompile]
    public partial struct NewBehaviourScript : ISystem
    {
        private Entity playerEntity;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<EnemyTag>();
            playerEntity = Entity.Null;
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if(playerEntity == Entity.Null)
            {
                playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            }
            var playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position;
            new MoveJob
            {
                DeltaTime = SystemAPI.Time.DeltaTime,
                PlayerPosition = playerPosition,
            }.ScheduleParallel();
        }

        private partial struct MoveJob : IJobEntity
        {
            public float DeltaTime;
            public float3 PlayerPosition;

            private void Execute(ref LocalTransform localTransform, ref PhysicsVelocity physicsVelocity, in EnemyStatus enemyStatus, in EnemyTag enemyTag, in Simulate simulate)
            {
                if(math.distancesq(PlayerPosition, localTransform.Position) > 0)
                {
                    var direction = math.normalize(PlayerPosition - localTransform.Position);
                    localTransform.Position += direction * enemyStatus.MoveSpeed * DeltaTime;
                    //localTransform.Position.y = 0;
                    physicsVelocity.Linear.y = 0;
                }
            }
        }
    }
}