using TetrisDefence.Data.Manager;
using UnityEngine;

namespace TetrisDefence.Game.Enemy
{
    public class MobManager : MonoBehaviour
    {
        public Transform StartPoint;
        private int _stage;
        private int _wave;

        private void Start()
        {
            for (int ix = 3; ix < 9; ix++)
            {
                var enemy = PoolManager.Instance.GetEnemy((Item)ix).transform;
                enemy.parent = transform;
                enemy.position = StartPoint.position;
            }
        }

        public void Spawn()
        {

        }
    }
}
