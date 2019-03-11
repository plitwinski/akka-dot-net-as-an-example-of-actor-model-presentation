using System.Collections.Generic;

namespace Domain.Examples.Domains.Warehouse
{
    public class Warehouse
    {
        public Warehouse(string name)
        {
            Name = name;
            Products = new List<WarehouseProduct>();
        }

        public IList<WarehouseProduct> Products { get; set; }
        public string Name { get; }
    }
}
