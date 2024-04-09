using System;
using System.Collections;
using TetrisDefence.Game.Map;
using UnityEngine;
using UnityEngine.Pool;

namespace TetrisDefence.Game.Enemy
{
    [Serializable]
    public class EnemyBase : MonoBehaviour, IEnemy
    {
        public int currentIndex = default;
        public GameObject Child = null;
        private float _hp = default;
		private float _moveSpeed = 1.0f;
        private float _movePercent = 0.0f;
        

        public void Move()
        {
            StartCoroutine(C_Move(currentIndex));
        }
        public void Born()
        {
            throw new NotImplementedException();
        }

        public void Death()
        {
            throw new NotImplementedException();
        }

        private void OnEnable()
        {
            Born();
            Move();
        }

        private void OnDisable()
        {
            Death();
        }

        private IEnumerator C_Move(int index)
        {
            for (int ix = index; ix < MapOfNodes.roads.Length; ix++)
            {
                float t = 0;
                while (_movePercent < 0.5f)
                {
                    transform.position = Vector3.Lerp(MapOfNodes.roads[ix].inlet,
                                                      MapOfNodes.roads[ix].center,
                                                      t);
                    t += Time.deltaTime * _moveSpeed;
                    _movePercent = t / 2;
                    yield return null;
                }
                transform.position = MapOfNodes.roads[ix].center;
                t = 0;

                while (_movePercent >= 0.5f && _movePercent < 1.0f)
                {
                    transform.position = Vector3.Lerp(MapOfNodes.roads[ix].center,
                                                      MapOfNodes.roads[ix].outlet,
                                                      t);
                    t += Time.deltaTime * _moveSpeed;
                    _movePercent = 0.5f + t / 2;
                    yield return null;
                }
                transform.position = MapOfNodes.roads[ix].outlet;        
                
                currentIndex = ix + 1;
                _movePercent = 0.0f;
            }
        }
    }
}
