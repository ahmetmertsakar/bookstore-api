using BookStoreAPI_Assessment.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI_Assessment.Data;

public class BookStoreContext(DbContextOptions<BookStoreContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Books)
            .WithOne(b => b.Category)
            .HasForeignKey(b => b.CategoryId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Book)
            .WithMany()
            .HasForeignKey(o => o.BookId);
    }
}
