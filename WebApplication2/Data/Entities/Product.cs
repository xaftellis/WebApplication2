namespace WebApplication2.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }             // Unique identifier for the item
        public string UserId { get; set; }       // Who added the item
        public string Name { get; set; }         // Item name/title
        public string Description { get; set; }  // Item description
        public byte[] Photo { get; set; }        // The image
        public DateTime DateCreated { get; set; } = DateTime.Now; // When it was added
    }
}
