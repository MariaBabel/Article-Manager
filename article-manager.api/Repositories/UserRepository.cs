using System.Collections.Generic;
using System.Threading.Tasks;
using article_manager.api.Data;
using backendlib.Models;
using Microsoft.EntityFrameworkCore;

namespace article_manager.api.Repositories
{
    public class UserRepository : ICRUDRepository<User>
    {
        private readonly BDArticleManagementContext _context;
        public UserRepository(BDArticleManagementContext context)
        {
            _context = context;
        }

        public Task Create(User item)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> Get(int id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            return result;
        }

        public Task<IEnumerable<User>> GetList()
        {
            throw new System.NotImplementedException();
        }

        public Task Update(User item)
        {
            throw new System.NotImplementedException();
        }
    }
}