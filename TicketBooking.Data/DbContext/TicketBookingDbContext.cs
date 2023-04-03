using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketBooking.Data.DataModel;
using TicketBooking.Model.DataModel;

namespace TicketBooking.Data.DbContext
{
    public class TicketBookingDbContext : IdentityDbContext<ApplicationUser>
    {
        public TicketBookingDbContext(DbContextOptions<TicketBookingDbContext> options) : base(options) { }

        #region
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<SeatClass> SeatClasses { get; set; }
        public DbSet<FlightSchedule> FlightSchedules { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<ExtraService> ExtraBaggages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<BookingList> BookingLists { get; set; }
        public DbSet<BookingSeat> ListSeats { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<BookingExtraService> BookingServices { get; set; }
        public DbSet<Bill> Bills { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Aircraft>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasMany(a => a.DepartureAirports).WithOne(fs => fs.AirportDepart)
                    .HasForeignKey(fs => fs.DepartureAirportId);
                entity.HasMany(a => a.ArrivalAirports).WithOne(fs => fs.AirportArrival)
                    .HasForeignKey(fs => fs.ArrivalAirportId);
            });
            modelBuilder.Entity<ContactDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<FlightSchedule>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(e => e.AirportDepart)
                .WithMany(e => e.DepartureAirports)
                .HasForeignKey(e => e.DepartureAirportId)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.AirportArrival)
                    .WithMany(e => e.ArrivalAirports)
                    .HasForeignKey(e => e.ArrivalAirportId)
                 .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(entity => entity.IsFlightActive).HasDefaultValue("false");

                entity.HasOne(e => e.Schedule)
                .WithMany(e => e.Flights)
                .HasForeignKey(e => e.ScheduleId);
            });

            modelBuilder.Entity<SeatClass>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(e => e.SeatClass)
                .WithMany(e => e.Seats)
                .HasForeignKey(e => e.SeatClassId)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.Aircraft)
                .WithMany(e => e.Seats)
                .HasForeignKey(e => e.AirCraftId)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.ListSeat)
                .WithOne(e => e.Seat)
                .HasForeignKey<BookingSeat>(e => e.SeatId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ContactDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ExtraService>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IsPaid).HasDefaultValue("false");

                entity.Property(e => e.IsRoundFlight).HasDefaultValue("false");


                entity.HasOne(e => e.ContactDetail)
                .WithMany(e => e.Bookings)
                .HasForeignKey(e => e.ContactId);

                entity.HasOne(e => e.User)
               .WithMany(e => e.Bookings)
               .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<BookingList>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(e => e.Booking)
                .WithMany(e => e.BookingLists)
                .HasForeignKey(e => e.BookingId)
                 .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.Flight)
               .WithMany(e => e.BookingLists)
               .HasForeignKey(e => e.FlightId)
                 .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<BookingSeat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(e => e.BookingList)
               .WithMany(e => e.ListSeats)
               .HasForeignKey(e => e.BookingListId)
               .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(e => e.Booking)
               .WithMany(e => e.Passengers)
               .HasForeignKey(e => e.BookingId);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(e => e.Booking)
               .WithMany(e => e.Tickets)
               .HasForeignKey(e => e.BookingId)
               .OnDelete(DeleteBehavior.ClientSetNull);
                
                entity.HasOne(e => e.Passenger)
                .WithMany(e => e.Tickets)
                .HasForeignKey(e => e.PassengerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(e => e.Booking)
               .WithMany(e => e.Tickets)
               .HasForeignKey(e => e.BookingId)
               .OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<BookingExtraService>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(e => e.BookingList)
               .WithMany(e => e.BookingServices)
               .HasForeignKey(e => e.BookingListId)
               .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.ExtraService)
               .WithMany(e => e.BookingServices)
               .HasForeignKey(e => e.ExtraServiceId)
               .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(e => e.Booking)
                .WithMany(e => e.Bills)
                .HasForeignKey(e => e.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}