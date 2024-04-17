using System;
using System.Collections.Generic;

namespace TetrisDefence.Data
{
    public class LoginRepository : IRepository<LoginRepository>
    {
        public LoginRepository(InGameContext context)
        {
            _context = context;
        }

        public event Action<int, LoginRepository> onDataUpdated;

        private InGameContext _context;


        public void DeleteData(LoginRepository data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LoginRepository> GetAllDatas()
        {
            throw new NotImplementedException();
        }

        public LoginRepository GetDataByID(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertData(LoginRepository data)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateData(int id, LoginRepository data)
        {
            onDataUpdated?.Invoke(id, data);
            throw new NotImplementedException();
        }
    }
}
