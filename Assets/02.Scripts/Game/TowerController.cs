using TetrisDefence.Data.Manager;
using UnityEngine;

namespace TetrisDefence.Game
{
    public class TowerController : MonoBehaviour
    {
        public int TowerIndex { get; private set; } = default;
        public GameObject bulletPrefab;
        private int _tier = default;
        private int[] _tetris = new int[7];

        private void Awake()
        {
            TowerManager.Instance.Register(this);
            TowerIndex = TowerManager.Instance.towers.Count + 1;

            PoolManager.Instance.CreatePool
            (
                Item.Bullet,
                () =>
                {
                    return Instantiate(bulletPrefab.GetComponent<Bullet>());
                },
                (bullet) =>
                {
                    Bullet bullet1 = (Bullet)bullet;
                    bullet1.transform.SetParent(transform);
                    bullet1.transform.localPosition = Vector3.zero;
                },
                (bullet) =>
                {
                    Bullet bullet1 = (Bullet)bullet;
                    bullet1.transform.SetParent(PoolManager.Instance.pooledObject.transform.GetChild((int)Item.Bullet));
                    bullet1.gameObject.SetActive(false);
                }
            );
        }

        private void Start()
        {
            Bullet[] b = new Bullet[10];
            for (int ix = 0; ix < 10; ix++)
            {
                b[ix] = (Bullet)PoolManager.Instance.pooledItems[Item.Bullet].Get();
            }
            for (int ix = 0; ix < 10; ix++)
            {
                PoolManager.Instance.pooledItems[Item.Bullet].Release(b[ix]);
            }
        }
    }
}
