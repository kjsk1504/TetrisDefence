using UnityEngine;

namespace TetrisDefence.Data.Base
{
    [CreateAssetMenu(fileName = "new EnemyInfo", menuName = "ScriptableObjects/EnemyInfo")]
    public class ItemInfo : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public GameObject Child { get; private set; }
        [field: SerializeField] public Sprite icon { get; private set; }
    }
}
