using System.ComponentModel.DataAnnotations;

namespace activity1.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime CategoryAddedDate { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
