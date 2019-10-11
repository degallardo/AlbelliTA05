using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlbelliTA05.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
        public int MaxStack { get; set; }
    }
}