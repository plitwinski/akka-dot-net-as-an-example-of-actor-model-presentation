namespace Domain.Examples.Domains.Warehouse
{
    public class WarehouseProduct
    {
        public WarehouseProduct(string id)
        {
            Id = id;
            Quantity = 0;
        }

        public string Id { get; }
        public int Quantity { get; set; }
    }
}
