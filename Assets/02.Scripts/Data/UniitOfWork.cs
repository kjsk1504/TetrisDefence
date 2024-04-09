

namespace TetrisDefence.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {
            _context = new InGameContext();
            loginRepository = new LoginRepository(_context);
        }

        public bool isReady => _context.hasInitialized;
        public IRepository<LoginRepository> loginRepository { get; private set; }


        IRepository<LoginRepository> IUnitOfWork.repository => throw new System.NotImplementedException();

        private InGameContext _context;
    }
}
