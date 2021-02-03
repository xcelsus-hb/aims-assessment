using System;


namespace DeliVeggie.Domain.DTO
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public double Price { get; set; }
    }
}
