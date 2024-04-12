using System;
using System.Collections;
using TetrisDefence.Data.Manager;
using TetrisDefence.Game.Map;
using UnityEngine;

namespace TetrisDefence.Game.Enemy
{
    public class EnemyBase : PoolBase
    {
        public int currentIndex;
        public EnemyBase Child;
        protected float _movePercent;
        private float _hp = default;
		public float moveSpeed = 1.0f;


        public override void Born()
        {
            base.Born();

            Move();
        }

        public override void Death()
        {
            base.Death();

            if ((int)_itemIndex < 4 || currentIndex >= MapOfNodes.roads.Length)
            {
                return;
            }

            var child = PoolManager.Instance.GetEnemy(_itemIndex - 1);

            child.transform.SetParent(transform.parent);
            child.transform.position = transform.position;
            child.transform.rotation = transform.rotation;
            child.currentIndex = currentIndex;
            child._movePercent = _movePercent;
            child.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            Born();
        }

        public void Move()
        {
            if (currentIndex >= MapOfNodes.roads.Length)
            {
                Death();
            }
            else if (_movePercent < 0.5f)
            {
                StartCoroutine(C_MoveFirstHalf());
            }
            else
            {
                StartCoroutine(C_RotationByRoad());
                StartCoroutine(C_MoveLastHalf());
            }
        }

        private IEnumerator C_MoveFirstHalf()
        {
            while (_movePercent < 0.5f)
            {
                transform.position = Vector3.Lerp(MapOfNodes.roads[currentIndex].inlet,
                                                  MapOfNodes.roads[currentIndex].center,
                                                  (_movePercent - 0.0f) / (0.5f - 0.0f));
                _movePercent += Time.deltaTime * moveSpeed;
                yield return null;
            }

            Move();
        }

        private IEnumerator C_MoveLastHalf()
        {
            while (_movePercent >= 0.5f && _movePercent < 1.0f)
            {
                transform.position = Vector3.Lerp(MapOfNodes.roads[currentIndex].center,
                                                  MapOfNodes.roads[currentIndex].outlet,
                                                  (_movePercent - 0.5f) / (1.0f - 0.5f));
                _movePercent += Time.deltaTime * moveSpeed;
                yield return null;
            }

            _movePercent -= 1.0f;
            currentIndex += 1;
            
            Move();
        }

        private IEnumerator C_RotationByRoad()
        {
            float t = 0;

            while (t < 1)
            {
                Quaternion.Slerp(transform.rotation, MapOfNodes.roads[currentIndex].transform.rotation, t);
                t += Time.deltaTime * moveSpeed;
                yield return null;
            }

            transform.rotation = MapOfNodes.roads[currentIndex].transform.rotation;
        }
    }
}
