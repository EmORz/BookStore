using BookStore.Model;
using BookStore.Model.Address;
using BookStore.Model.Orders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookStoreDbContext : IdentityDbContext<BookStoreUser, IdentityRole, string>
    {
        public DbSet<BookStoreUser> BookStoreUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<BookStoreInformation> BookStoreInformations { get; set; }
        public DbSet<Personal> Personals { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<IncomeMoney> IcIncomeMonies { get; set; }

        //public DbSet<Music> Musics { get; set; }
        //public DbSet<Film> Films { get; set; }
 
        //public DbSet<Other> Others { get; set; }
        //public DbSet<Book> Books { get; set; }
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

      




            base.OnModelCreating(builder);
        }
    }
}