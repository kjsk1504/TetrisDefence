using TetrisDefence.Data.Manager;

namespace TetrisDefence.Game.Enemy
{
    public interface IEnemy : IPoolItem
    {
        void Move();
        void Born();
        void Death();
    }
}
