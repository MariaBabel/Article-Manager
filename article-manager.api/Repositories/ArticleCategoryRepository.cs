using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using article_manager.api.Data;
using backendlib.Models;
using Microsoft.EntityFrameworkCore;

namespace article_manager.api.Repositories
{
    public class ArticleCategoryRepository : ICRUDRepository<ArticleCategory>
    {
        private readonly BDArticleManagementContext _context;

        public ArticleCategoryRepository(BDArticleManagementContext context)
        {
            _context = context;
        }

        public async Task Create(ArticleCategory item)
        {
            _context.ArticleCategories.Add(item);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var entity = _context.ArticleCategories.SingleOrDefault(categoryDb => categoryDb.Id == id);
            if (entity == null) throw new ArgumentException("Item not found", "item");
            _context.Remove(entity);
            return _context.SaveChangesAsync();
        }

        public async Task<ArticleCategory> Get(int id)
        {
            return await _context.ArticleCategories
           .Where(categoryDb => categoryDb.Id == id)
           .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ArticleCategory>> GetList()
        {
            return await _context.ArticleCategories.ToListAsync();
        }

        public Task Update(ArticleCategory item)
        {
            var entity = _context.ArticleCategories.SingleOrDefault(categoryDb => categoryDb.Id == item.Id);
            if (entity == null) throw new ArgumentException("Item not found", "item");
            entity.Name = item.Name;
            entity.Description = item.Description;
            return _context.SaveChangesAsync();
        }
    }
}