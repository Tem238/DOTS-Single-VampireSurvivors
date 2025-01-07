using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace DOTS_Single.VampireSurvivors
{
    public partial class MainCameraMoveSystem : SystemBase
    {
        private readonly float3 MAIN_CAMERA_OFFSET = new float3(0f, 1.73f, -3f);

        private Entity _ownerPlayerEntity;
        private Camera _mainCamera;

        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
            RequireForUpdate<MainCameraTag>();
            RequireForUpdate<MainCamera>();
        }

        protected override void OnStartRunning()
        {
            // ���삵�Ă���v���C���[�̃G���e�B�e�B���L���b�V��
            _ownerPlayerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            //���C���J�������L���b�V��
            var cameraEntity = SystemAPI.GetSingletonEntity<MainCameraTag>();
            _mainCamera = EntityManager.GetComponentObject<MainCamera>(cameraEntity).Value;
        }

        protected override void OnStopRunning()
        {
            _ownerPlayerEntity = Entity.Null;
        }

        protected override void OnUpdate()
        {
            // ���삵�Ă���v���C���[�̌��݈ʒu���擾
            var playerLocalPosition = EntityManager.GetComponentData<LocalTransform>(_ownerPlayerEntity).Position;
            // �I�t�Z�b�g��ǉ�����
            _mainCamera.transform.position = playerLocalPosition + MAIN_CAMERA_OFFSET;
        }
    }
}
