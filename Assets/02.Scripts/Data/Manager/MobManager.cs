using TetrisDefence.Enums;
using UnityEngine;

namespace TetrisDefence.Data.Manager
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
                var enemy = PoolManager.Instance.GetEnemy(ix).transform;
                enemy.parent = transform;
                enemy.position = StartPoint.position;
                enemy.gameObject.SetActive(true);
            }
        }
    }
}
