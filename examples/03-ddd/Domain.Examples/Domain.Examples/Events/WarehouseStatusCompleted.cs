namespace Domain.Examples.Events
{
    public class WarehouseStatusCompleted
    {
        public WarehouseStatusCompleted(string report)
        {
            Report = report;
        }

        public string Report { get; }
    }
}
