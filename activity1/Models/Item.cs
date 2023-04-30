namespace activity1.Models
{
    public class Item
    {
        public Product Product { get; set; }
        public Brand Brand { get; set; }

        public int Quantity { get; set; }
        public Category Category { get; set; }
    }
}
