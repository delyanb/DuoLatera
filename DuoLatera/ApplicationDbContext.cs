using DuoLatera.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DuoLatera
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<CardSet> CardSets { get; set; }
        public DbSet<FlashCard> FlashCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Enable Cascade Delete
            modelBuilder.Entity<FlashCard>()
                .HasOne(f => f.CardSet)
                .WithMany(c => c.FlashCards)
                .HasForeignKey(f => f.CardSetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
