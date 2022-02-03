using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichardH_P0
{
    internal class Store
    {

        internal List<Location> Locations = new List<Location>();

        public Store() { }
        public Store(List<Location> Locations)
        {
            this.Locations = Locations;
        }

        public void print()
        {
            foreach (var location in Locations)
            {
                Console.WriteLine(location.name);
            }
        }
    }
}
