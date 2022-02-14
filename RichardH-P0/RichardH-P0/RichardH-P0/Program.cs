﻿using RichardH_P0.DL;
using RichardH_P0.BL;

namespace RichardH_P0.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = File.ReadAllText("C:/Revature/Connection Strings/P0-DB.txt");
            IRepository repository = new SqlRepository(connectionString);
            IO io = new IO(repository);
            int opt = -1;
            User CurrentUser = new User();
            Location CurrentLocation;

            Console.WriteLine("Welcome to Richs' Store App");

            do
            {
                // Login or Create user
                if (CurrentUser.Id == -1)
                { 

                    Console.WriteLine(io.LoginMenu());
                    var sel = Console.ReadLine();

                    bool success = int.TryParse(sel, out opt);
                    if (!success)
                    {
                        opt = -1;
                        Console.WriteLine("Please enter a valid number option.");
                    }

                    switch (opt)
                    {
                        case 0:
                            opt = -1;
                            break;

                        case 1:
                            CurrentUser = io.CreateNewUser(false);
                            break;

                        case 2:
                            CurrentUser = io.LoginToUser();
                            break;

                        default:
                            opt = 9999999;
                            Console.WriteLine("Please make a valid selection.");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                    }
                }

                // Customer Menu
                else if ((CurrentUser.Id != -1) && (!CurrentUser.IsManager))
                {
                    CurrentLocation = io.GetLocation(CurrentUser);
                    Console.Clear();
                    Console.WriteLine($"You are currently shopping: { CurrentLocation.LocationName}");
                    Console.WriteLine(io.CustomerMenu());

                    var sel = Console.ReadLine();

                    bool success = int.TryParse(sel, out opt);
                    if (!success)
                    {
                        Console.WriteLine("Invalid option.");
                        opt = -1;
                    }

                    switch (opt)
                    {
                        case 0:
                            opt = 0;
                            break;

                        case 1:
                            Console.Clear();
                            CurrentUser.LocationID = io.SelectLocation(CurrentUser);
                            Console.WriteLine("Location Updated!");
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine(io.PrintLocationInventory(CurrentUser.LocationID));
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            break;

                        case 3:
                            //Buil Place Order
                            Console.Clear();
                            io.PlaceOrder(CurrentUser);
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            break;

                        case 4:
                            Console.Clear();
                            Console.WriteLine(io.PrintUserOrders(CurrentUser.Id));
                            Console.WriteLine("Please enter a OrderID number from the table above to view details for that order, or enter '0' to exit.");
                            int input;
                            var raw = Console.ReadLine();
                            success = int.TryParse(raw, out input);
                            if (!success)
                            {
                                Console.WriteLine("Invald selection. Returning to main menu.");
                                Console.ReadLine();
                                Console.ReadLine();
                                break;
                            }
                                                        
                            if (input == 0)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine(io.PrintOrderDetail(input));
                                Console.WriteLine("Press Enter to continue.");
                                Console.ReadLine();
                                break;
                            }

                        default:
                            opt = 99999;
                            Console.WriteLine("Please make a valid selection.");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                    }
                    
                }
                // Manager Menu
                else if ((CurrentUser.Id != -1) && (CurrentUser.IsManager))
                {
                    CurrentLocation = io.GetLocation(CurrentUser);
                    Console.Clear();

                    Console.WriteLine($"You are currently managing: { CurrentLocation.LocationName}");
                    Console.WriteLine(io.ManagerMenu());

                    var sel = Console.ReadLine();

                    bool success = int.TryParse(sel, out opt);
                    if (!success)
                    {
                        Console.WriteLine("Please enter a valid number option.");
                    }

                    switch (opt)
                    {
                        case 0:
                            opt = -1;
                            Console.Clear();
                            break;

                        case 1:
                            Console.Clear();
                            CurrentUser.LocationID = io.SelectLocation(CurrentUser);
                            Console.WriteLine("Location Updated!");
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine(io.PrintLocationInventory(CurrentLocation.ID));
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            break;

                        case 3:
                            Console.Clear();
                            Console.WriteLine(io.PrintOrders());
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            break;

                        case 4:
                            Console.Clear();
                            Console.WriteLine(io.PrintUsers());
                            Console.WriteLine("Please enter a UserID number from the table above to search all orders by that user.");

                            int input;
                            var raw = Console.ReadLine();
                            success = int.TryParse(raw, out input);
                            if (!success)
                            {
                                Console.WriteLine("Invald selection. Returning to main menu.");
                                Console.ReadLine();
                                break;
                            }

                            Console.Clear();
                            Console.WriteLine(io.PrintUserOrders(input));
                            Console.WriteLine("Please enter a OrderID number from the table above to view details for that order, or enter '0' to exit.");
                            raw = Console.ReadLine();
                            success = int.TryParse(raw, out input);
                            if (!success)
                            {
                                Console.WriteLine("Invald selection. Returning to main menu.");
                                Console.ReadLine();
                                break;
                            }

                            Console.Clear();
                            if (input == 0)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine(io.PrintOrderDetail(input));
                                Console.WriteLine("Press Enter to continue.");
                                Console.ReadLine();
                                break;
                            }

                        case 5:
                            Console.Clear();
                            io.UpdateInventory();
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            break;

                        case 6:
                            Console.Clear();
                            io.AddItem();
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            break;

                        case 7:
                            Console.Clear();
                            io.SetLocationSalePercentage(CurrentLocation);
                            Console.WriteLine("Press Enter to continue.");
                            Console.ReadLine();
                            break;

                        default:
                            opt = 99999;
                            Console.Clear();
                            Console.WriteLine("Please make a valid selection, or enter [0] to exit.");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                    }
                }
            }
            while (opt > 0);

        }
    }
}