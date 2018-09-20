namespace WebApplication1.Models
{
    public class OrderLine
    {
        public int OrderId { get; set; }
        public int LineNumber { get; set; }
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
        public decimal LineTotal { get; set; }
    }
}