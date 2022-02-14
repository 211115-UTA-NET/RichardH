using System.Text;
using RichardH_P0.BL;
using RichardH_P0.DL;

namespace RichardH_P0.App
{
    internal class IO
    {
        private readonly IRepository _repository;

        public IO(IRepository repository)
        {
            this._repository = repository;
        }

        public string LoginMenu()
        {
            var result = new StringBuilder();

            result.AppendLine("Please make your selection from the options below: ");
            result.AppendLine("---------------------------------------------------------------");
            result.AppendLine("[0] - Exit Application");
            result.AppendLine("[1] - Create New User");
            result.AppendLine("[2] - Login");

            return result.ToString();
        }

        public string CustomerMenu()
        {
            var result = new StringBuilder();

            result.AppendLine("*** Customer Menu *** ");
            result.AppendLine("Please make your option selection below: ");
            result.AppendLine("---------------------------------------------------------------");
            result.AppendLine("[0] - Exit Application");
            result.AppendLine("[1] - Choose Store");
            result.AppendLine("[2] - Review Store Inventory");
            result.AppendLine("[3] - Place New Order");
            result.AppendLine("[4] - Review Past Orders");

            return result.ToString();
        }

        public string ManagerMenu()
        {
            var result = new StringBuilder();

            result.AppendLine("*** Management Menu ***");
            result.AppendLine("Please make your option selection below: ");
            result.AppendLine("---------------------------------------------------------------");
            result.AppendLine("[0] - Exit Application");
            result.AppendLine("[1] - Choose Store");
            result.AppendLine("[2] - Review Store Inventory");
            result.AppendLine("[3] - Review All Orders");
            result.AppendLine("[4] - Review Order by Customer");
            result.AppendLine("[5] - Update Store Inventory");
            result.AppendLine("[6] - Add Item to Invntory");
            result.AppendLine("[7] - Change Location Sale Percentage");

            return result.ToString();

        }

        public User CreateNewUser(bool manager)
        {
            User NewUser = new User();
            NewUser.IsManager = manager;

            Console.Clear();

            while (NewUser.Username == "")
            {
                Console.WriteLine("Please enter a username: ");
                string tmp = Console.ReadLine();
                
                if ((tmp.Length >= 5) && (tmp.Length < 200))
                {
                    NewUser.Username = tmp;
                    Console.Clear();
                    continue;
                }
                Console.WriteLine("Username must be 5 characters or longer");
            }

            while (NewUser.Password == "")
            {
                Console.WriteLine("Please enter a password: ");
                string tmp = Console.ReadLine();

                if ((tmp.Length >= 8) && (tmp.Length < 32))
                {
                    NewUser.Password = tmp;
                    Console.Clear();
                    continue;
                }
                Console.WriteLine("Passwords must be 8 characters or longer");
            }


            while (NewUser.LocationID == -1)
            {
                Console.WriteLine("Please select a default store. You can still order from other locations.");
                Console.WriteLine(this.PrintLocations());
                Console.WriteLine("Enter the ID number of the location you'd like to set: ");
                int tmp = Convert.ToInt32(Console.ReadLine());

                IEnumerable<Location> allLocations = _repository.GetAllLocations();

                if ((tmp > 0) && (tmp <= allLocations.Count()))
                {
                    NewUser.LocationID = tmp;
                    Console.Clear();
                    continue;
                }
                Console.WriteLine("Please select a valid location.");
            }

            NewUser.Id = _repository.CreateUserAndReturnUserID(NewUser);


            return NewUser;
        }

        public User LoginToUser()
        {
            User result = new User();

            Console.Clear();

            Console.WriteLine("Enter your Username: ");
            string Username = Console.ReadLine();
            Console.WriteLine("Enter your password: ");
            string Password = Console.ReadLine();

            if (Username == null || Password == null)
            {
                Console.WriteLine("Please enter valid values.");
            }

            try
            {
                result = _repository.LookupUser(Username, Password);
            }
            catch(Exception e)
            {
                Console.WriteLine("Login failed, press Enter to continue.");
                Console.ReadLine();
            }

            return result;
        }

        public string PrintUsers()
        {
            IEnumerable<User> allUsers = _repository.GetAllUsers();
            var summary = new StringBuilder();
            summary.AppendLine($"UserID\tUserName\tDefault Location\tManager");
            summary.AppendLine("---------------------------------------------------------------");
            foreach (var record in allUsers)
            {
                summary.AppendLine($"{record.Id}\t{record.Username}\t\t{record.LocationID}\t\t{record.IsManager}");
            }
            summary.AppendLine("---------------------------------------------------------------");

            return summary.ToString();
        }

        public string PrintLocations()
        {
            IEnumerable<Location> allLocations = _repository.GetAllLocations();
            var summary = new StringBuilder();
            summary.AppendLine($"ID\tLocation Name\tSale Percentage");
            summary.AppendLine("---------------------------------------------------------------");
            foreach (var record in allLocations)
            {
                summary.AppendLine($"{record.ID}\t{record.LocationName}\t{record.SalePercentage}");
            }
            summary.AppendLine("---------------------------------------------------------------");

            return summary.ToString();
        }

        public string PrintItems()
        {
            IEnumerable<Item> allItems = _repository.GetAllItems();
            var summary = new StringBuilder();
            summary.AppendLine($"ID\tItem Name\tItem Description");
            summary.AppendLine("---------------------------------------------------------------");
            foreach (var record in allItems)
            {
                summary.AppendLine($"{record.ID}\t{record.ItemName}\t{record.ItemDescription}");
            }
            summary.AppendLine("---------------------------------------------------------------");

            return summary.ToString();
        }

        public void AddItem()
        {
            string repeat = "";
            double ItemPrice;
            int ItemID, LocationID, Quantity;

            Console.WriteLine("Enter the name of the item you would like to add.");
            string ItemName = Console.ReadLine();
            Console.WriteLine("Enter a short description of the item.");
            string ItemDescription = Console.ReadLine();
            Console.WriteLine("Enter the base price of the item.");
            var input = Console.ReadLine();

            bool success = double.TryParse( input, out ItemPrice);
            if (!success)
            {
                Console.WriteLine("Invalid number option. Add failed.");
                return;
            }

            try
            {
                ItemID = _repository.CreateItem(ItemName, ItemDescription, ItemPrice);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Item could not be added. Add failed.");
                return;
            }
            do
            {
                Console.WriteLine(this.PrintLocations());
                Console.WriteLine("Enter the LocationID to add item to location.");
                input = Console.ReadLine();

                success = int.TryParse(input, out LocationID);
                if (!success)
                {
                    Console.WriteLine("Invalid number option. Add failed.");
                    return;
                }

                Console.WriteLine("Enter the number of units at this location.");
                input = Console.ReadLine();

                success = int.TryParse(input, out Quantity);
                if (!success)
                {
                    Console.WriteLine("Invalid number option. Add failed.");
                    return;
                }

                try
                {
                    _repository.AddInventory(LocationID, ItemID, Quantity);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Item could not be added to inventory. Add failed.");
                    return;
                }

                Console.WriteLine("Would you like to add this item to another location? [Y/N]");
                repeat = Console.ReadLine();
            }
            while (repeat == "y" || repeat == "Y");

        }

        public string PrintInventories()
        {
            var summary = new StringBuilder();
            IEnumerable<Inventory> allInventories = Enumerable.Empty<Inventory>();

            try
            {
                allInventories = _repository.GetAllInventories();
            }
            catch (Exception ex)
            {
                summary.AppendLine("Invalid entry. Returning to main menu.");
                return summary.ToString();
            }
            
            summary = new StringBuilder();
            summary.AppendLine($"InventoryID\tItemID\tItemPrice\tLocationID\tQuantity");
            summary.AppendLine("---------------------------------------------------------------");
            foreach (var record in allInventories)
            {
                summary.AppendLine($"{record.InventoryID}\t{record.ItemID}\t{record.LocationID}\t{record.Quantity}");
            }
            summary.AppendLine("---------------------------------------------------------------");

            return summary.ToString();
        }
       
        public string PrintLocationInventory(int LocationID)
        {
            var summary = new StringBuilder();
            IEnumerable<InvView> allInventories = Enumerable.Empty<InvView>();
            double SalePercentage;
            try
            {
                SalePercentage = _repository.GetLocationSalePercentage(LocationID) / 100.0;
            }
            catch (Exception ex)
            {
                summary.AppendLine("Invalid entry. Returning to main menu.");
                return summary.ToString();
            }              

            try
            {
                allInventories = _repository.GetLocationInventory(LocationID);
            }
            catch (Exception ex)
            {
                summary.AppendLine("Invalid entry. Returning to main menu.");
                return summary.ToString();
            }

            summary.AppendLine($"ItemId\tItem Name\tUnit Price\tQuantity");
            summary.AppendLine("---------------------------------------------------------------");
            foreach (var record in allInventories)
            {
                double price = Math.Round(((double)record.ItemPrice * SalePercentage), 2);
                summary.AppendLine($"{record.ItemID}\t{record.ItemName}\t{price}\t\t{record.Quantity}");
            }
            summary.AppendLine("---------------------------------------------------------------");

            return summary.ToString();
        }

        public void UpdateInventory()
        {
            int LocationID, ItemID, Quantity, add;

            Console.WriteLine(this.PrintLocations());
            Console.WriteLine("Select a LocationID from the list above to update the location inventory.");
            var sel = Console.ReadLine();

            bool success = int.TryParse(sel, out LocationID);
            if (!success)
            {
                Console.WriteLine("Invalid entry. Returning to main menu.");
                return;
            }

            try
            {
            Console.WriteLine(this.PrintLocationInventory(LocationID));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Your request could not be processed. Returning to main menu.");
                return;
            }

            Console.WriteLine("Select an ItemID from the list above to update.");
            sel = Console.ReadLine();

            success = int.TryParse(sel, out ItemID);
            if (!success)
            {
                Console.WriteLine("Invalid entry. Returning to main menu.");
                return;
            }

            Quantity = _repository.GetItemQuantity(LocationID, ItemID);
            Console.WriteLine("How many of this item are to be added to this locations inventory?");
            sel = Console.ReadLine();

            success = int.TryParse(sel, out add);
            if (!success)
            {
                Console.WriteLine("Invalid entry. Returning to main menu.");
                return;
            }

            Quantity += add;

            try
            {
                _repository.UpdateInventory(LocationID, ItemID, Quantity);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Your request could not be processed. Returning to main menu.");
                return;
            }

        }

        public string PrintOrders()
        {
            IEnumerable<Order> allOrders = _repository.GetAllOrders();
            var summary = new StringBuilder();
            summary.AppendLine($"OrderID\tOrder Date\t\tUser\t\tLocation\t\tOrder Total");
            summary.AppendLine("---------------------------------------------------------------");
            foreach (var record in allOrders)
            {
                summary.AppendLine($"{record.OrderID}\t{record.OrderDate.ToString("MM/dd/yyyy HH:mm")}\t{_repository.GetUserName(record.UserID)}\t\t{_repository.GetLocationName(record.LocationID)}\t\t${record.OrderTotal}");
            }
            summary.AppendLine("---------------------------------------------------------------");

            return summary.ToString();
        }

        public string PrintUserOrders(int UserID)
        {
            var summary = new StringBuilder();
            try
            {
                IEnumerable<Order> allOrders = _repository.GetUserOrders(UserID);
                summary.AppendLine($"Displaying order history for {_repository.GetUserName(UserID)}:");
                summary.AppendLine($"OrderID\tOrder Date\t\tOrder Total");
                summary.AppendLine("---------------------------------------------------------------");
                foreach (var record in allOrders)
                {
                    summary.AppendLine($"{record.OrderID}\t{record.OrderDate.ToString("MM/dd/yyyy HH:mm")}\t${record.OrderTotal}");
                }
                summary.AppendLine("---------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                summary.AppendLine("Your request could not be processed, please enter a valid UserID.");
            }
            return summary.ToString();
        }

        public string PrintOrderDetail(int OrderID)
        {
            var summary = new StringBuilder();
            try
            {
                IEnumerable<OrderItem> OrderDetail = _repository.GetOrderDetail(OrderID);
                summary.AppendLine($"Displaying order history for Order: {OrderID}");
                summary.AppendLine($"Item\tQuantity\tPrice");
                summary.AppendLine("---------------------------------------------------------------");
                foreach (var Item in OrderDetail)
                {
                    summary.AppendLine($"{Item.ItemName}\t{Item.Quantity}\t${Item.Price}");
                }
                summary.AppendLine("---------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                summary.AppendLine("Your request could not be processed, please enter a valid OrderID.");
            }
            return summary.ToString();
        }

        public void PlaceOrder(User CurrentUser)
        {
            int ItemID, quantity;
            double price, SalePercentage;
            double OrderTotal = 0;
            bool repeat = true;
            Order NewOrder = new Order();
            List<OrderItem> OrderItems = new();

            try
            {
                SalePercentage = _repository.GetLocationSalePercentage(CurrentUser.LocationID) / 100.0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid entry. Returning to main menu.");
                return;
            }

            do
            {
                Console.Clear();
                Console.WriteLine($"Placing order  for {_repository.GetLocationName(CurrentUser.LocationID)}:");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine(this.PrintLocationInventory(CurrentUser.LocationID));
                Console.WriteLine("To add an item to your order, enter the ItemID from the list above.\n If you do not wish to add any additional items, enter [0].");

                var raw = Console.ReadLine();
                bool success = int.TryParse(raw, out ItemID);
                if (!success)
                {
                    Console.WriteLine("Invald selection. Returning to main menu.");
                    Console.ReadLine();
                    return;
                }
                else if (ItemID == 0)
                {
                    if (!OrderItems.Any())
                    {
                        Console.WriteLine("Exiting order.");
                        Console.ReadLine();
                        return;
                    }
                    else
                    {
                        foreach (OrderItem item in OrderItems)
                        {
                            OrderTotal += item.Price;
                        }

                        int OrderID = _repository.CreateOrderAndGetOrderID(CurrentUser.LocationID, CurrentUser.Id, OrderTotal);

                        foreach (OrderItem item in OrderItems)
                        {
                            
                            item.OrderID = OrderID;
                            _repository.CreateOrderItem(item);
                        }
                    }
                    repeat = false;
                }
                else
                {
                    Item SelectedItem = _repository.GetItem(ItemID);
                    Console.WriteLine("How many of this item would you like to add to your order?");

                    raw = Console.ReadLine();
                    success = int.TryParse(raw, out quantity);
                    if (!success)
                    {
                        Console.WriteLine("Invald selection. Returning to main menu.");
                        Console.ReadLine();
                        return;
                    }
                    else if (_repository.GetItemQuantityFromLocation(CurrentUser.LocationID, SelectedItem.ID) < quantity)
                    {
                        Console.WriteLine("Not enough stock to complete your order. Removing item from order.\nPress enter to continue.");
                        Console.ReadLine();
                        continue;
                    }

                    price = quantity * SalePercentage * SelectedItem.ItemPrice;

                    OrderItems.Add(new(ItemID, quantity, price));                    
                }               
            }
            while (repeat);
            
            foreach (OrderItem item in OrderItems)
                {
                    _repository.UpdateInventory(CurrentUser.LocationID, item.ItemID, (_repository.GetItemQuantityFromLocation(CurrentUser.LocationID, item.ItemID) - item.Quantity));
                }
            return;
        }

        public int SelectLocation(User CurrentUser)
        {
            int sel = -1;
            while(true)
            {
                IEnumerable<Location> allLocations = _repository.GetAllLocations();

                Console.WriteLine("Please select the location you would like to shop from the list below.");
                Console.WriteLine(this.PrintLocations());

                var input = Console.ReadLine();

                bool success = int.TryParse(input, out sel);
                if (!success)
                {
                    Console.WriteLine("Please enter a valid number option.");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
                else if (sel <= (allLocations.Count()))
                {
                    CurrentUser.LocationID = sel;
                    _repository.SelectLocation(CurrentUser);
                    return sel;
                }
                else
                {
                    Console.WriteLine("Please make a valid selection from the list of locations.");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
            }
            return sel;
        }

        public Location GetLocation(User CurrentUser)
        {
            return _repository.GetLocation(CurrentUser);
        }

        public void SetLocationSalePercentage(Location CurrentLocation)
        {
            Console.WriteLine($"Displaying order history for Order: {CurrentLocation.LocationName}");
            Console.WriteLine($"Location Name\tSale Percentage");
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine($"{CurrentLocation.LocationName}\t{CurrentLocation.SalePercentage}%");
            Console.WriteLine("---------------------------------------------------------------");
        
            int SalePercentage;
            Console.WriteLine("Please enter the sale percentage as an integer that you would like to set for the current location.");
            var input = Console.ReadLine();

            bool success = int.TryParse(input, out SalePercentage);
            if (!success)
            {
                Console.WriteLine("Please enter a valid number option.");
            }
            else if(SalePercentage == 0)
            {
                Console.WriteLine("Sale Percentage unchanged, returning to main menu.");
                return;
            }

            try
            {
                _repository.SetLocationSalePercentage(CurrentLocation, SalePercentage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sale percentage update failed.");
                return;
            }

            Console.WriteLine("Sale percentage updated.");
            return;
        }
    }
}
