namespace Domain.Examples.Domains.Order.Commands
{
    public class AddProductToOrder
    {
        public AddProductToOrder(string productId)
        {
            ProductId = productId;
        }

        public string ProductId { get; }
    }
}
