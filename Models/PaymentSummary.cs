namespace UniVerServer.Models
{
    public class PaymentSummary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public decimal AmountToBePaid { get; set; }
    }
}
