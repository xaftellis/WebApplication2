using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Product
    {
        public Guid Id { get; set; }             // Unique identifier for the item

        public string UserId { get; set; }       // Who added the item

        public byte[] Photo { get; set; }        // The image

        public string Name { get; set; }         // Item name/title

        public string Description { get; set; }  // Item description

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now; // When it was added

        [NotMapped]
        public IFormFile FormImage { get; set; }
    }
}
