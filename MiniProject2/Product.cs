using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject2
{
    internal class Product
    {

        internal String Category { get; set; }
        internal String ProductName { get; set; }
        internal decimal Price { get; set; }

        public Product() { }

        public Product(String category, String productName, decimal price)
        {
            Category = category;
            ProductName = productName;
            Price = price;
        }

        public String toString()
        {
            return this.Category.PadLeft(20) + this.ProductName.PadLeft(20) + this.Price.ToString().PadLeft(20);
        }

    }
}
