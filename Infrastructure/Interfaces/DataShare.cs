using DomainLayer.Entity;
using DomainLayer.Interface;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces
{
    public class DataShare : IDataShare
    {
        private readonly CodeshareDbContext _context;
        public DataShare(CodeshareDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<CodeshareEntity> CreateAsync(CodeshareEntity dto)
        {
            _context.Code.Add(dto);
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<CodeshareEntity?> GetAsync(string urlKey)
        {
            var Data = await _context.Code.FirstOrDefaultAsync(x => x.UrlKey == urlKey);
            if (Data == null)
            {
                return null;
            }
            return Data;
        }

        public async Task<CodeshareEntity> UpdateAsync(CodeshareEntity entity)
        {
            var existingEntity = await _context.Code
                .FirstOrDefaultAsync(c => c.UrlKey == entity.UrlKey);

            if (existingEntity == null)
            {
                return null;
            }

            existingEntity.Description = entity.Description;

            _context.Code.Update(existingEntity);
            await _context.SaveChangesAsync();

            return existingEntity;
        }
    }
}
