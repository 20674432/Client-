namespace ClientPortal.ViewModels
{
    public class OrderDto
    {
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int? StatusId { get; set; }
    }
}
