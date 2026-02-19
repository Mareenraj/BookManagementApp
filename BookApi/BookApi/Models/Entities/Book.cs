namespace BookApi.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public DateOnly PublicationDate { get; set; }
    }
}
