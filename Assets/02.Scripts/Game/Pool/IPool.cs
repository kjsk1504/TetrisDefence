using System;

namespace TetrisDefence.Game.Pool
{
    public interface IPool
    {
        string PoolIndex { get; }
        event Action<IPool> onBorn;
        event Action<IPool> onDeath;

        void Born();
        void Death();
    }
}
