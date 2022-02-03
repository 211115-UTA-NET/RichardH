using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichardH_P0
{
    internal class Item
    {
        internal int id { get; set; }
        internal string? name { get; set; }
        internal string? description { get; set; }
        internal double price { get; set; }
        internal int quantity { get; set; }


        public Item() { }
        public Item(string name, double price, int quantity, string descritpion = "")
        {
            this.name = name;
            this.description = descritpion; 
            this.price = price;
            this.quantity = quantity;
        }




    }
}
