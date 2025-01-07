using Unity.Entities;
using UnityEngine;

namespace DOTS_Single.VampireSurvivors
{
    [CreateAssetMenu(fileName = "SO_Enemy", menuName = "ScriptableObject/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public int ID;
        public GameObject Prefab;
        public int HP;
        public int Attack;
        public int Deffence;
        public float MoveSpeed;
    }

    public struct EnemyStatus : IComponentData
    {
        public int ID;
        public int MaxHP;
        public int HP;
        public int Attack;
        public int Deffence;
        public float MoveSpeed;
    }
}
