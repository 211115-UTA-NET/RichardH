using System;

namespace RichardH_P0
{
    public class Program
    {
        public static void Main()
        {
            //Create User()
            //loginf


            //change location
            //view inventory
            //add to cart
            //view cart
            //place order

            //review orders

            //logout



            //login as Manager
            //    view all orders
            //    view all orders by user

            //    select location
            //        view orders by location
            //        view orders by user
            //        edit inventory for location





            User currentUser = new User();
            Location currentLocation = null;


            // Create Temp locations and inventories

            List<Item> items = new List<Item>();
            items.Add(new Item("Front Tire", 74.99, 5, "Great grip to hold your conering line."));
            items.Add(new Item("Rear Tire", 99.99, 5, "Keep the power to the ground wiht new rubber."));
            items.Add(new Item("Half-Helmet", 49.50, 3, "Half a helmet to keep you half as safe."));
            items.Add(new Item("Full-Helmet", 125, 4, "Nobdy like to eat bugs!"));
            items.Add(new Item("Riding Jacket", 150, 3, "No such thing as a 'little scrape', ssave your skin!"));

            Location NewYork = new Location("New York", items);
            Location Atlanta = new Location("Atlanta", items);

            List<Location> Locations = new List<Location>();
            Locations.Add(NewYork);
            Locations.Add(Atlanta);

            Store store = new Store(Locations);



            //Main Menu
            bool main = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to the MotoParts Inc.");
                Console.WriteLine("Please select from the menu options below.");
                Console.WriteLine("[1] - Create a new user.");
                Console.WriteLine("[2] - Login to an exiting user.");
                Console.WriteLine("[3] - Select a store location.");
                if (currentLocation != null) { Console.WriteLine($"[4] - Shop {currentLocation.name} location"); }
                Console.WriteLine("[0] - Exit MotoParts Inc.");

                
                switch (Console.ReadLine().ToString())
                {
                    case "0":
                        {
                            Console.WriteLine("Thank you for shopping with MotoParts Inc.");
                            main = false;
                            break;
                        }
                    case "1":
                        {
                            currentUser = User.CreateNewUser();
                            currentUser.Print();
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("Login functionality coming soon");
                            break;
                        }
                    case "3":
                        {
                            store.print();
                            Console.WriteLine("Please select your choice of store locations.");


                            try
                            {
                                currentLocation = store.Locations[(Int32.Parse(Console.ReadLine())-1)];
                                    }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Not a valid location selection.");
                            }
                            break;
                        }
                    case "4":
                        {
                            currentLocation.print();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Not a valid selection.");
                            break;
                        }
                }
                Console.ReadKey();
            }
            while (main);
        }

    }
}
