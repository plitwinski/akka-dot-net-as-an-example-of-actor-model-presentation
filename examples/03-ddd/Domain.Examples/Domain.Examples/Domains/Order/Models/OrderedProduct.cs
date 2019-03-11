namespace Domain.Examples.Domains.Order.Models
{
    public class OrderedProduct
    {
        public OrderedProduct(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
