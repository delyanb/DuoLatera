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
        
    }
}
