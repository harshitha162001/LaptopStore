using System.ComponentModel.DataAnnotations;

namespace activity1.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime BrandAddedDate { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
