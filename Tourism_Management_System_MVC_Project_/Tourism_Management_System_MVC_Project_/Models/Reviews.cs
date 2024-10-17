namespace Tourism_Management_System_MVC_Project_.Models
{
    public partial class Reviews
    {
        public int ReviewId { get; set; }

        public int? UserId { get; set; }

        public int? TourId { get; set; }

        public decimal? Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime? ReviewDate { get; set; }

        public virtual TourPackages? Tour { get; set; }

        public virtual Users? User { get; set; }
    }
}
