using System;
using TetrisDefence.Enums;
using UnityEngine;

namespace TetrisDefence.Game.Enemy
{
    [Serializable]
    [CreateAssetMenu(fileName = "new EnemyInfo", menuName = "ScriptableObjects/EnemyInfo")]
    public class EnemyInfo : ScriptableObject
    {
        [field: SerializeField] public EItem Index { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public EItem ChildIndex { get; private set; }
    }
}
