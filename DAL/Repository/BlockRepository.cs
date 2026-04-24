using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Context;
using Domain.Models;

namespace DAL.Repository
{
    public class BlockRepository : IRepository<Block>
    {
        private readonly BlockContext _blockContext;
        public BlockRepository(BlockContext blockContext)
        {
            _blockContext = blockContext;
        }

        public async Task AddAsync(Block block)
        {
            await _blockContext.Blocks.AddAsync(block);
        }

        public async Task<List<Block>> GetAllAsync()
        {
            return await _blockContext.Blocks.OrderBy(b => b.Index).ToListAsync();
        }

        public async Task<Block?> GetLastAsync()
        {
            return await _blockContext.Blocks.OrderByDescending(b => b.Index).FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _blockContext.SaveChangesAsync();
        }
    }
}