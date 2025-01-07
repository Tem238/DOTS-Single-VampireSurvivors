using Unity.Entities;
using UnityEngine;

namespace DOTS_Single.VampireSurvivors
{
    /// <summary>
    /// エネミーエンティティ認識用タグ
    /// </summary>
    public struct EnemyTag : IComponentData { }

    /// <summary>
    /// エネミーを初期化するためのタグ
    /// </summary>
    public struct NewEnemyTag : IComponentData { }

    public class EnemyAuthoring : MonoBehaviour
    {
        public class Baker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring enemyAuthoring)
            {
                var enemyEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<EnemyTag>(enemyEntity);
                AddComponent<NewEnemyTag>(enemyEntity);
            }
        }
    }
}
