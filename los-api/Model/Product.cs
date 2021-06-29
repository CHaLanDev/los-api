using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace los_api
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public double Price { get; set; }
        public List<Stock> Stocks { get; set; }
    }
}
