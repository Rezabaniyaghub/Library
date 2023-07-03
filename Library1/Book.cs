namespace Library1
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Auther { get; set; }
        public string? Address { get; set; }
        public int TotalNumber { get; set; }
        public DateTime PublishDate { get; set; }

        public virtual ICollection<Barrow>? Barrows { get; set; }
    }
}
