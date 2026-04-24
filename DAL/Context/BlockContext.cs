using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class BlockContext : DbContext
    {

        public BlockContext(DbContextOptions<BlockContext> options) : base(options) { }

        public DbSet<Block> Blocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Block>()
                .HasIndex(b => b.Index)
                .IsUnique();

            modelBuilder.Entity<Block>()
                .HasIndex(b => b.PreviousHash);
        }
    }
}