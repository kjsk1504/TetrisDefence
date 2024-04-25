using TetrisDefence.Data.Enums;
using TetrisDefence.Data.Manager;
using TetrisDefence.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisDefence.Game.Map
{
    public class MinoSocket : SocketBase
    {
        [field: SerializeField] public int Tier { get; private set; } = 0;
        [field: SerializeField] public bool IsActivated { get; private set; } = false;
        [field: SerializeField] public EMino MinoIndex { get; private set; } = EMino.None;


        protected override void Awake()
        {
            base.Awake();

            if (Location[0] >= 5 || Location[1] >= 5)
            {
                if (Location[0] >= 6 || Location[1] >= 6)
                {
                    Tier = 3;
                }
                else
                {
                    Tier = 2;
                }
            }
            else
            {
                Tier = 1;
            }
        }

        private void Start()
        {
            GetComponentInParent<UITowerInformation>().sockets[Location[0] - 1, Location[1] - 1] = this;
        }

        public bool IsValid()
        {
            if (IsActivated)
            {
                if (MinoIndex != EMino.None)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckActivated(int tier)
        {
            if (Tier <= tier)
            {
                GetComponent<Image>().color = Color.white;
                IsActivated = true;
            }
            else
            {
                GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                IsActivated = false;
            }

            return IsActivated;
        }

        public EMino CheckOccupied()
        {
            return MinoIndex;
        }

        public bool ComparePosition(float xPosition, float yPosition)
        {
            return (transform.position.x - 27.5f < xPosition) && (transform.position.x + 27.5 > xPosition) 
                && (transform.position.y - 27.5f < yPosition) && (transform.position.y + 27.5 > yPosition);
        }

        public bool ComparePosition(Vector2 position)
        {
            return ComparePosition(position.x, position.y);
        }

        public bool ComparePosition(Vector3 position)
        {
            return ComparePosition(position.x, position.y);
        }
    }
}
