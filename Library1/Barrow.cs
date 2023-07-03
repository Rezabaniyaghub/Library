namespace Library1
{
    public class Barrow
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public bool? WasDelivered { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }

        public virtual Member? Member { get; set; }
        public virtual Book? Book { get; set; }
    }
}
