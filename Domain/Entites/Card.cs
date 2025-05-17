namespace Domain.Entites
{
    public class Card : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
