using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichardH_P0
{
    internal class Location
    {
        private int id;
        internal string? name;
        internal List<Item> inventory;

        public Location() { }
        public Location(string name, List<Item> inventory)
        {
            this.name = name;
            this.inventory = inventory;
        }

        public void print()
        {
            foreach (Item item in inventory)
            {
                Console.WriteLine($"{item.id} \t {item.name} \t {item.quantity}");
            }
        }
    }
}