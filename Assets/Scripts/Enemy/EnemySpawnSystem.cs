using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DOTS_Single.VampireSurvivors
{
    public partial struct EnemySpawnSystem : ISystem
    {
        private float _Timer;
        private Random _Random;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<EnemyPrefabElementBuffer>();
            _Timer = 0;
            _Random = new Random(100);
        }

        public void OnUpdate(ref SystemState state)
        {
            _Timer += SystemAPI.Time.DeltaTime;
            if (_Timer > 3)
            {
                _Timer = 0;
                var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
                var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

                var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
                var enemyPrefabBuffer = SystemAPI.GetSingletonBuffer<EnemyPrefabElementBuffer>();

                var playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position;
                var x = playerPosition.x + (_Random.NextBool() ? 1 : -1) * _Random.NextFloat(3f, 7f);
                var z = playerPosition.z + (_Random.NextBool() ? 1 : -1) * _Random.NextFloat(3f, 7f);
                var spawnPosition = new float3(x, 0, z);


                int i = 0;
                var enemyEntity = ecb.Instantiate(enemyPrefabBuffer[i].Prefab);
                ecb.SetComponent(enemyEntity, LocalTransform.FromPositionRotationScale(spawnPosition, quaternion.identity, 0.5f));
                ecb.AddComponent(enemyEntity, new EnemyStatus
                {
                    ID = enemyPrefabBuffer[i].ID,
                    MaxHP = enemyPrefabBuffer[i].HP,
                    HP = enemyPrefabBuffer[i].HP,
                    Attack = enemyPrefabBuffer[i].Attack,
                    Deffence = enemyPrefabBuffer[i].Deffence,
                    MoveSpeed = enemyPrefabBuffer[i].MoveSpeed,
                });
                ecb.AddBuffer<DamageBufferElement>(enemyEntity);
            }
        }
    }
}
