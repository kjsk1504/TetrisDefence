using TetrisDefence.Data.Manager;
using UnityEngine;

namespace TetrisDefence.Game
{
    public class Bullet : MonoBehaviour, IPoolItem
    {
        private float bulletSpeed = 1.0f;
        private float bulletTime = 10.0f;


        private void Awake()
        {

        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            
        }
    }
}
