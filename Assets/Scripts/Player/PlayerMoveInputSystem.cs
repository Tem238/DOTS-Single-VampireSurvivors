using Unity.Entities;
using UnityEngine;

namespace DOTS_Single.VampireSurvivors
{
    public partial class PlayerMoveInputSystem : SystemBase
    {
        // InputSystemで設定したアクション
        private InputActions _inputActions;
        private Entity _playerEntity;

        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
            _inputActions = new InputActions();
        }

        protected override void OnStartRunning()
        {
            _inputActions.Enable();
            _playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        }

        protected override void OnStopRunning()
        {
            _inputActions.Disable();
            _playerEntity = Entity.Null;
        }
        protected override void OnUpdate()
        {
            // プレイヤーの入力をコンポーネントにセット
            EntityManager.SetComponentData(_playerEntity, new PlayerMoveInput
            {
                Value = _inputActions.MainGameMap.PlayerMovement.ReadValue<Vector2>()
            });
        }

    }
}