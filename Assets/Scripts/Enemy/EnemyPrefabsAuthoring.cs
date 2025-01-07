using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace DOTS_Single.VampireSurvivors
{
    public struct EnemyPrefabElementBuffer : IBufferElementData
    {
        public int ID;
        public Entity Prefab;
        public int HP;
        public int Attack;
        public int Deffence;
        public float MoveSpeed;
    }

    public class EnemyPrefabsAuthoring : MonoBehaviour
    {
        [SerializeField] private List<EnemyData> enemyDataList;
        public class Baker : Baker<EnemyPrefabsAuthoring>
        {
            public override void Bake(EnemyPrefabsAuthoring authoring)
            {
                var containerEntity = GetEntity(TransformUsageFlags.None);
                AddBuffer<EnemyPrefabElementBuffer>(containerEntity);
                foreach(var enemyData in authoring.enemyDataList)
                {
                    var enemyEntity = GetEntity(enemyData.Prefab, TransformUsageFlags.Dynamic);
                    AppendToBuffer(containerEntity, new EnemyPrefabElementBuffer
                    {
                        ID = enemyData.ID,
                        Prefab = enemyEntity,
                        HP = enemyData.HP,
                        Attack = enemyData.Attack,
                        Deffence = enemyData.Deffence,
                        MoveSpeed = enemyData.MoveSpeed,
                    });
                }
            }
        }
    }
}
