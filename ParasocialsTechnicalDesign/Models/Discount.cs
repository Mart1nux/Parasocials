﻿namespace ParasocialsPOSAPI.Models
{
    public class Discount
    {
        public Group Group { get; set; }
        public DiscountType Type { get; set; }
        public decimal Ammount { get; set; }
        public DateTime DiscountStart { get; set; }
        public DateTime DiscountEnd { get; set;}
    }
}
