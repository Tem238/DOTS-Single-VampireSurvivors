using Unity.Entities;
using UnityEngine;

namespace DOTS_Single.VampireSurvivors
{
    /// <summary>
    /// �G�l�~�[�G���e�B�e�B�F���p�^�O
    /// </summary>
    public struct EnemyTag : IComponentData { }

    /// <summary>
    /// �G�l�~�[�����������邽�߂̃^�O
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
