namespace GoWithMe.Areas.Admin.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GoWithMeDbContext : DbContext
    {
        public GoWithMeDbContext()
            : base("name=GoWithMeDbContext")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<TourDetail> TourDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<Customer>()
                .Property(e => e.AccountID)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<News>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<News>()
                .Property(e => e.PlaceID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<News>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Place>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<Place>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Place>()
                .HasMany(e => e.News)
                .WithRequired(e => e.Place)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Place>()
                .HasMany(e => e.TourDetails)
                .WithRequired(e => e.Place)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.TourID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.CustomerID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.Quantyti)
                .HasPrecision(38, 0);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.Tatus)
                .IsUnicode(false);

            modelBuilder.Entity<Tour>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<Tour>()
                .Property(e => e.Quantyti)
                .HasPrecision(38, 0);

            modelBuilder.Entity<Tour>()
                .Property(e => e.Price)
                .HasPrecision(11, 2);

            modelBuilder.Entity<Tour>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Tour>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.Tour)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tour>()
                .HasMany(e => e.TourDetails)
                .WithRequired(e => e.Tour)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TourDetail>()
                .Property(e => e.TourID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TourDetail>()
                .Property(e => e.PlaceID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TourDetail>()
                .Property(e => e.Number)
                .HasPrecision(38, 0);
        }
    }
}
