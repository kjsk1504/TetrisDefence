using System;
using TetrisDefence.Data.Enums;
using TetrisDefence.Data.Manager;
using TetrisDefence.Game.Map;
using TetrisDefence.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TetrisDefence.Game.Pool
{
    public class MinoBase : PoolBase, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public MinoInfo minoInfo;
        public bool isDrag;
        public EMino minoIndex;
        public int rotationNumber;
        public int[,] relativeLocations = new int[4, 2];
        // todo: 미노의 위치, 크기, 회전각 등을 저장해서 타워인포에 전달
        // todo: 움직일때마다 mino및 _tetris에 변화


        private void Start()
        {
            if (minoIndex != minoInfo.index)
            {
                if (!Enum.TryParse(name, out minoIndex))
                {
                    print($"[MinoBase]: {name}의 인덱스가 {minoIndex}와 {minoInfo.index}로 다름");
                }
            }

            PoolIndex = minoIndex.ToString();

            InformationInitialization();
        }

        private void Update()
        {
            if (InputManager.Instance.isRKeyDown)
            {
                print($"[{name}]: pressed R");
                if (isDrag)
                {
                    Rotation(rotationNumber + 1);
                    print($"[{name}]: rotate");
                }
            }
        }

        public override void Born()
        {
            if (minoIndex == 0)
            {
                if (!Enum.TryParse(name, out minoIndex))
                {
                    print($"[MinoBase]: {name}: {minoIndex} error");
                }
            }

            isDrag = false;

            base.Born();
        }

        public override void Death()
        {
            isDrag = false;
            InformationInitialization();

            base.Death();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //todo: 이미 타워 UI에 저장된 경우 이동 불가
            isDrag = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isDrag)
            {
                transform.position = eventData.position;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isDrag)
            {
                isDrag = false;

                RaycastResult result = eventData.pointerCurrentRaycast;

                if (result.gameObject != null)
                {
                    var canvas = result.gameObject.GetComponentInParent<UITowerInformation>();
                    if (canvas != null)
                    {
                        canvas.RegisterMino(this);

                        foreach (var socket in canvas.sockets)
                        {
                            if (socket.ComparePosition(eventData.position))
                            {
                                print(socket.name);
                                transform.position = socket.transform.position;
                                //todo: 미노의 다른 부분도 소켓에 영향을 주도록 
                                return;
                            }
                        }
                        canvas.UnregisterMino(this);
                    }
                }

                InventoryManager.Instance.ItemUpdate((int)minoIndex, +1);
                Death();
            }
        }

        private void InformationInitialization()
        {
            rotationNumber = 0;
            Rotation(rotationNumber);
        }

        private void Rotation(int rotate)
        {
            rotationNumber = rotate % 4;
            transform.eulerAngles = new Vector3(0, 0, 90 * rotationNumber);

            relativeLocations = new int[4, 2]
            {
                { Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(minoInfo.block1[0], 2) + Mathf.Pow(minoInfo.block1[1], 2)) * Mathf.Cos(Mathf.PI / 2 * rotationNumber + Mathf.Atan2(minoInfo.block1[1], minoInfo.block1[0]))), Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(minoInfo.block1[0], 2) + Mathf.Pow(minoInfo.block1[1], 2)) * Mathf.Sin(Mathf.PI / 2 * rotationNumber + Mathf.Atan2(minoInfo.block1[1], minoInfo.block1[0]))) },
                { Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(minoInfo.block2[0], 2) + Mathf.Pow(minoInfo.block2[1], 2)) * Mathf.Cos(Mathf.PI / 2 * rotationNumber + Mathf.Atan2(minoInfo.block2[1], minoInfo.block2[0]))), Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(minoInfo.block2[0], 2) + Mathf.Pow(minoInfo.block2[1], 2)) * Mathf.Sin(Mathf.PI / 2 * rotationNumber + Mathf.Atan2(minoInfo.block2[1], minoInfo.block2[0]))) },
                { Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(minoInfo.block3[0], 2) + Mathf.Pow(minoInfo.block3[1], 2)) * Mathf.Cos(Mathf.PI / 2 * rotationNumber + Mathf.Atan2(minoInfo.block3[1], minoInfo.block3[0]))), Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(minoInfo.block3[0], 2) + Mathf.Pow(minoInfo.block3[1], 2)) * Mathf.Sin(Mathf.PI / 2 * rotationNumber + Mathf.Atan2(minoInfo.block3[1], minoInfo.block3[0]))) },
                { Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(minoInfo.block4[0], 2) + Mathf.Pow(minoInfo.block4[1], 2)) * Mathf.Cos(Mathf.PI / 2 * rotationNumber + Mathf.Atan2(minoInfo.block4[1], minoInfo.block4[0]))), Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(minoInfo.block4[0], 2) + Mathf.Pow(minoInfo.block4[1], 2)) * Mathf.Sin(Mathf.PI / 2 * rotationNumber + Mathf.Atan2(minoInfo.block4[1], minoInfo.block4[0]))) },
            };
        }

        private void Check()
        {

        }
    }
}
