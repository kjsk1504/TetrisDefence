using System;

namespace TetrisDefence.Game
{
    public interface IPool
    {
        event Action onBorn;
        event Action<IPool> onDeath;

        void Born();
        void Death();
    }
}
