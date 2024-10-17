using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Tourism_Management_System_MVC_Project_.Models
{
    public partial class TourPackages
    {
        public int TourId { get; set; }

        public string TourName { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? Category { get; set; }

        public int Duration { get; set; }

        public string? ImageUrl { get; set; }

        public decimal? Rating { get; set; }

        public string? Location { get; set; }

        public virtual ICollection<Bookings> Bookings { get; set; } = new List<Bookings>();

        public virtual ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
    }
}
