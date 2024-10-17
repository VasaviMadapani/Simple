namespace Tourism_Management_System_MVC_Project_.Models
{
    public partial class Bookings
    {
        public int BookingId { get; set; }

        public int? UserId { get; set; }

        public int? TourId { get; set; }

        public DateTime BookingDate { get; set; }

        public string Status { get; set; }

        public virtual TourPackages? Tour { get; set; }

        public virtual Users? User { get; set; }
    }
}
