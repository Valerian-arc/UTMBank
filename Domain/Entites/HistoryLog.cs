namespace Domain.Entites
{
    public class HistoryLog : BaseEntity
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }
        public int SourceUser { get; set; }
        public int DestinationUser { get; set; }
    }
}
