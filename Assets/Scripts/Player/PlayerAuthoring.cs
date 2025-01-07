using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DOTS_Single.VampireSurvivors
{
    /// <summary>
    /// プレイヤーを認識するタグ
    /// </summary>
    public struct PlayerTag : IComponentData { }

    /// <summary>
    /// プレイヤーを初期化するためのタグ
    /// </summary>
    public struct NewPlayerTag : IComponentData { }

    /// <summary>
    /// プレイヤーからの移動量の入力値を保持
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