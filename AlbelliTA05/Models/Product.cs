namespace AlbelliTA05.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Width { get; set; }
        public int MaxStack { get; set; }
    }
}
