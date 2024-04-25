using System.Collections;
using TetrisDefence.Data.Manager;
using TetrisDefence.Game.Map;
using TetrisDefence.Data.Enums;
using UnityEngine;
using System;
using TMPro;

namespace TetrisDefence.Game.Pool
{
    public class EnemyBase : ItemBase
    {
        [field: SerializeField] public int CurrentRoadIndex { get; protected set; }

        public EnemyInfo enemyInfo;
        public TMP_Text hpText;

        protected float _movePercent;

        private int _hp = default;
        private float _moveSpeed = default;
        private EItem _childIndex = default;


        private void Start()
        {
            if (itemIndex != enemyInfo.index)
            {
                throw new Exception($"[EnemyBase]: {enemyInfo}가 잘못됨");
            }

            _childIndex = enemyInfo.childIndex;

            if ((int)_childIndex != (int)itemIndex - 1)
            {
                if (_childIndex == EItem.None && itemIndex != EItem.TrigonalPrism)
                {
                    print($"[{name}]: 자식{_childIndex}이 {itemIndex}와 맞지 않음");
                    _childIndex = itemIndex - 1;
                }
            }
        }

        private void OnDisable()
        {
            MobManager.Instance.EnemyUnregister(this);
        }

        public override void Born()
        {
            base.Born();

            InforamtionInitialization();
            hpText.text = _hp.ToString();
            MobManager.Instance.EnemyRegister(this);

            if (CurrentRoadIndex < 1)
            {
                transform.rotation = MapOfNodes.roads[1].transform.rotation;
            }
            else
            {
                StartCoroutine(C_RotationByRoad(CurrentRoadIndex - 1));
            }

            Move();
        }

        public override void Death()
        {
            CurrentRoadIndex = 0;
            _movePercent = 0;
            base.Death();
        }

        public void Move()
        {
            if (CurrentRoadIndex >= MapOfNodes.roads.Length)
            {
                AttackNexus();
            }
            else if (_movePercent < 0.5f)
            {
                StartCoroutine(C_MoveFirstHalf());
            }
            else
            {
                if (CurrentRoadIndex < MapOfNodes.roads.Length - 1)
                {
                    StartCoroutine(C_RotationByRoad(CurrentRoadIndex + 1));
                }

                StartCoroutine(C_MoveLastHalf());
            }
        }

        public void DamagedByBullet(int bulletDamage)
        {
            _hp -= bulletDamage;
            hpText.text = _hp.ToString();

            if (_hp <= 0)
            {
                LeftChildDeath();
            }
        }

        public void AttackNexus()
        {
            if (CurrentRoadIndex >= MapOfNodes.roads.Length)
            {
                GameManager.Instance.NexusHPChange(-_hp);
                Death();
            }
        }

        private void LeftChildDeath()
        {
            if ((int)_childIndex < 3 || CurrentRoadIndex >= MapOfNodes.roads.Length)
            {
                Death();

                return;
            }

            var child = PoolManager.Instance.GetEnemy(_childIndex);
            child.transform.SetParent(transform.parent);
            child.transform.position = transform.position;
            child.transform.rotation = transform.rotation;
            child.CurrentRoadIndex = CurrentRoadIndex;
            child._movePercent = _movePercent;
            child.ActiveSelf();

            Death();
        }

        private void InforamtionInitialization()
        {
            _hp = enemyInfo.damage;
            _moveSpeed = enemyInfo.speed;
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

        private IEnumerator C_RotationByRoad(int roadIndex)
        {
            float t = 0;

            while (t < 1)
            {
                Quaternion.Slerp(transform.rotation, MapOfNodes.roads[roadIndex].transform.rotation, t);
                t += Time.deltaTime * _moveSpeed;
                yield return null;
            }

            transform.rotation = MapOfNodes.roads[roadIndex].transform.rotation;
            hpText.transform.rotation = Quaternion.identity;
        }
    }
}
