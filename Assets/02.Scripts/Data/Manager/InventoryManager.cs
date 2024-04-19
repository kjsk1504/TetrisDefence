using System;
using TetrisDefence.Data.Enums;
using TetrisDefence.Data.Utill;

namespace TetrisDefence.Data.Manager
{
    public class InventoryManager : SingletonMonoBase<InventoryManager>
    {
        int[] _quantities = default;
        public Action<int>[] onQuantitiesChanged = default;


        protected override void Awake()
        {
            base.Awake();

            _quantities = new int[Enum.GetNames(typeof(Enums.EMino)).Length];
            onQuantitiesChanged = new Action<int>[Enum.GetNames(typeof(Enums.EMino)).Length];
        }

        public void ItemUpdate(int index, int delta)
        {
            if (index < _quantities.Length && index > -1)
            {
                _quantities[index] += delta;
                onQuantitiesChanged[index]?.Invoke(delta);
            }
            else
            {
                throw new Exception($"[InventoryManager]: 인덱스 범위 넘어감");
            }
        }
    }
}
