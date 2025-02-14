using Kumbuthane.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kumbuthane.Utility
{
    public class KumbuthaneDbContext:IdentityDbContext
    {

        public KumbuthaneDbContext(DbContextOptions<KumbuthaneDbContext> options) : base(options) { }
        public DbSet<KitapTuru> KitapTurleri { get; set; }
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Kiralama> Kiralamalar { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
