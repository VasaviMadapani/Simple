namespace Tourism_Management_System_MVC_Project_.Models
{
    public partial class Payments
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Status { get; set; }
        public string? PaymentMethod { get; set; }
        public string? CardNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? Cvv { get; set; }
    }
}
