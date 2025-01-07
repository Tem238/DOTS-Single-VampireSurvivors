using Unity.Entities;
using UnityEngine;

namespace DOTS_Single.VampireSurvivors
{
    /// <summary>
    /// メインカメラの参照を保持するコンポーネント
    /// </summary>
    /// <remarks>Cameraはマネージドクラスのため、structではなくclassで定義</remarks>
    public class MainCamera : IComponentData
    {
        public Camera Value;
    }

    /// <summary>
    /// メインカメラへの参照を保持するエンティティを認識するタグ
    /// </summary>
    public struct MainCameraTag : IComponentData { }

    public class MainCameraAuthoring : MonoBehaviour
    {
        public class Baker : Baker<MainCameraAuthoring>
        {
            public override void Bake(MainCameraAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<MainCameraTag>(entity);
            }
        }
    }
}