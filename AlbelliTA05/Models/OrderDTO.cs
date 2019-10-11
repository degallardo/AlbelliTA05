namespace AlbelliTA05.Models
{
    using System.Collections.Generic;

    public class OrderDTO
    {
        public class Detail
        {
            public int ProductID { get; set; }
            public string Product { get; set; }
            public double Width { get; set; }
            public double MaxStack { get; set; }
            public int Quantity { get; set; }
        }
        public IEnumerable<Detail> Details { get; set; }
        public double RequiredBinWidth { get; set; }
    }
}