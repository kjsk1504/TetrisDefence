using System.Collections;
using TetrisDefence.Data.Manager;
using TetrisDefence.Game.Map;
using UnityEngine;

namespace TetrisDefence.Game.Enemy
{
    public class EnemyBase : PoolBase
    {
        [field: SerializeField] public int CurrentRoadIndex { get; protected set; }

        public EnemyInfo enemyInfo;

        protected float _movePercent;

        private float _hp = default;
        private float _moveSpeed = default;
        private int _childIndex;


        private void Start()
        {
            _hp = enemyInfo.Damage;
            _moveSpeed = enemyInfo.Speed;
            _childIndex = (int)enemyInfo.ChildIndex;

            if (_childIndex != (int)ItemIndex - 1)
            {
                if (ItemIndex != Enums.EItem.TrigonalPrism || _childIndex != 0)
                {
                    print($"[{name}]: {_childIndex}가 {ItemIndex}와 맞지 않음");
                    _childIndex = (int)ItemIndex - 1;
                }
            }
        }

        public override void Born()
        {
            base.Born();

            

            Move();
        }

        public override void Death()
        {
            if (_childIndex < 3 || CurrentRoadIndex >= MapOfNodes.roads.Length)
            {
                base.Death();

                return;
            }

            var child = PoolManager.Instance.GetEnemy(_childIndex);

            child.transform.SetParent(transform.parent);
            child.transform.position = transform.position;
            child.transform.rotation = transform.rotation;
            child.CurrentRoadIndex = CurrentRoadIndex;
            child._movePercent = _movePercent;

            base.Death();
        }

        public void Move()
        {
            if (CurrentRoadIndex >= MapOfNodes.roads.Length)
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
                transform.position = Vector3.Lerp(MapOfNodes.roads[CurrentRoadIndex].inlet,
                                                  MapOfNodes.roads[CurrentRoadIndex].center,
                                                  (_movePercent - 0.0f) / (0.5f - 0.0f));
                _movePercent += Time.deltaTime * _moveSpeed;
                yield return null;
            }

            Move();
        }

        private IEnumerator C_MoveLastHalf()
        {
            while (_movePercent >= 0.5f && _movePercent < 1.0f)
            {
                transform.position = Vector3.Lerp(MapOfNodes.roads[CurrentRoadIndex].center,
                                                  MapOfNodes.roads[CurrentRoadIndex].outlet,
                                                  (_movePercent - 0.5f) / (1.0f - 0.5f));
                _movePercent += Time.deltaTime * _moveSpeed;
                yield return null;
            }

            _movePercent -= 1.0f;
            CurrentRoadIndex += 1;

            Move();
        }

        private IEnumerator C_RotationByRoad()
        {
            float t = 0;

            while (t < 1)
            {
                Quaternion.Slerp(transform.rotation, MapOfNodes.roads[CurrentRoadIndex].transform.rotation, t);
                t += Time.deltaTime * _moveSpeed;
                yield return null;
            }

            transform.rotation = MapOfNodes.roads[CurrentRoadIndex].transform.rotation;
        }
    }
}
