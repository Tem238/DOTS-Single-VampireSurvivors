using Unity.Entities;
using UnityEngine;

namespace DOTS_Single.VampireSurvivors
{
    public partial class MainCameraInitializeSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<MainCameraTag>();
        }
        protected override void OnUpdate()
        {
            // ���C���J�����̏����������͈�x�̂ݎ��s
            Enabled = false;
            // ���C���J�����̎Q�Ƃ�ێ�����p�̃G���e�B�e�B���擾
            var mainCameraEntity = SystemAPI.GetSingletonEntity<MainCameraTag>();
            // ���݂̃��C���J�����̎Q�Ƃ��R���|�[�l���g�ɃZ�b�g
            EntityManager.AddComponentObject(mainCameraEntity, new MainCamera { Value = Camera.main });
        }
    }
}


