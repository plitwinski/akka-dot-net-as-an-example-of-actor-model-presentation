namespace Domain.Examples.Domains.Warehouse.Commands
{
    public class AddProductToWarehouse
    {
        public AddProductToWarehouse(string id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }

        public string Id { get; }
        public int Quantity { get; }
    }
}
