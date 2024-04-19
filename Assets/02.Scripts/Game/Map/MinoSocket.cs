using TetrisDefence.Data.Enums;
using TetrisDefence.Data.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisDefence.Game.Map
{
    public class MinoSocket : SocketBase
    {
        public EMino minoIndex;
        public bool isValid;
        [field: SerializeField] public int Tier {  get; private set; }

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

        public bool CheckValid()
        {
            if (minoIndex == EMino.None)
            {
                isValid = false;
            }

            if (Tier < TowerManager.Instance.selectedTower.towerInfo.TowerTier)
            {
                isValid = false;
            }

            return isValid;
        }

        public void Activate()
        {
            GetComponent<Image>().color = Color.white;
        }

        public void Deactivate()
        {
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }
}
