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
            // メインカメラの初期化処理は一度のみ実行
            Enabled = false;
            // メインカメラの参照を保持する用のエンティティを取得
            var mainCameraEntity = SystemAPI.GetSingletonEntity<MainCameraTag>();
            // 現在のメインカメラの参照をコンポーネントにセット
            EntityManager.AddComponentObject(mainCameraEntity, new MainCamera { Value = Camera.main });
        }
    }
}


