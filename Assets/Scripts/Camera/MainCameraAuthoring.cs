using Unity.Entities;
using UnityEngine;

namespace DOTS_Single.VampireSurvivors
{
    /// <summary>
    /// ���C���J�����̎Q�Ƃ�ێ�����R���|�[�l���g
    /// </summary>
    /// <remarks>Camera�̓}�l�[�W�h�N���X�̂��߁Astruct�ł͂Ȃ�class�Œ�`</remarks>
    public class MainCamera : IComponentData
    {
        public Camera Value;
    }

    /// <summary>
    /// ���C���J�����ւ̎Q�Ƃ�ێ�����G���e�B�e�B��F������^�O
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