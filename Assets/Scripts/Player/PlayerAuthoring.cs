using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DOTS_Single.VampireSurvivors
{
    /// <summary>
    /// �v���C���[��F������^�O
    /// </summary>
    public struct PlayerTag : IComponentData { }

    /// <summary>
    /// �v���C���[�����������邽�߂̃^�O
    /// </summary>
    public struct NewPlayerTag : IComponentData { }

    /// <summary>
    /// �v���C���[����̈ړ��ʂ̓��͒l��ێ�
    /// </summary>
    public struct PlayerMoveInput : IComponentData
    {
        public float2 Value;
    }

    public class PlayerAuthoring : MonoBehaviour
    {
        public class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<PlayerTag>(playerEntity);
                AddComponent<NewPlayerTag>(playerEntity);
                AddComponent(playerEntity, new PlayerMoveInput
                {
                    Value = float2.zero
                });
                AddBuffer<DamageBufferElement>(playerEntity);
            }
        }
    }
}