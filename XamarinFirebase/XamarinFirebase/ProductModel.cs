using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFirebase
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string NameOfMarket { get; set; }
        public string Price { get; set; }
        public DateTime PriceDate { get; set; }
        public string Image { get; set; }
        public string Barcode { get; set; }
        public string Favorite { get; set; }

    }
}