using System;
using TetrisDefence.Data.Enums;
using UnityEngine;

namespace TetrisDefence.Game.Pool
{
    [Serializable]
    [CreateAssetMenu(fileName = "new EnemyInfo", menuName = "ScriptableObjects/EnemyInfo")]
    public class EnemyInfo : ScriptableObject
    {
        public EItem index = EItem.None;
        public int damage = 0;
        public float speed = 0.0f;
        public EItem childIndex = EItem.None;
    }
}
