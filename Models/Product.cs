using System.ComponentModel.DataAnnotations;

namespace Product_WebAPI_App.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }


    }
}
