using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichardH_P0
{
    internal class User
    {
        private int userID { get; set; }
        private string userName { get; set; }
        private string password { get; set; }
        private string firstName { get; set; } 
        private string lastName { get; set; }
        private int location { get; set; }



        public User() { }

        private User(string userName, string password, string firstName, string lastName, int store = 0)
        {
            this.userName = userName;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.location = store;
        }



        public static User CreateNewUser()
        {


            string UserName = "";
            string FirstName = "";
            string LastName = "";
            string Password = "";

            //Accept Username 
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.Write("Please enter a Username: ");
                string? input = Console.ReadLine();


                if (8 > input.Length || input.Length > 255)
                {
                    Console.WriteLine("Username must be between 8 and 255 characters.");
                }
                else if (ExitInDb(input))
                {
                    Console.WriteLine("Username already in use.");
                }
                else
                {
                    UserName = input;
                    Console.WriteLine($"Welcome {input}");
                    loop = false;
                }

                Console.WriteLine("Press Enter to continue.");
                Console.ReadKey();
            }

            //Accept Password
            loop = true;
            while (loop)
            {
                Console.Clear();
                Console.WriteLine("Please enter a password. \n" +
                    "It should be between 8 and 255 characters, \n" +
                    "and contain at least one number and one special character.");

                Console.Write("Please enter a password: ");
                string? input = Console.ReadLine();

                bool missingNumber = !input.Any(char.IsDigit);
                bool missingSpec = !input.Any(char.IsPunctuation);

                if (8 > input.Length || input.Length > 255)
                {
                    Console.WriteLine("Password must be between 8 and 255 characters.");
                }
                else if (missingNumber)
                {
                    Console.WriteLine("Password must contain at least one number.");

                }
                else if (missingSpec)
                {
                    Console.WriteLine("Password must contain at least one special character.");

                }
                else
                {
                    Console.Write("Please reenter your password: ");
                    string? input2 = Console.ReadLine();

                    if (input2 == input)
                    {
                        Password = input;
                        Console.WriteLine("Password set");
                        loop = false;
                    }
                    else
                    {
                        Console.WriteLine("Passwords must match.");
                    }
                }
                Console.WriteLine("Press Enter to continue.");
                Console.ReadKey();
            }

            //Accept FirstName
            loop = true;
            while (loop)
            {
                Console.Clear();
                Console.Write("Please enter your First Name: ");
                string? input = Console.ReadLine();


                if (2 > input.Length || input.Length > 255)
                {
                    Console.WriteLine("First name must be between 2 and 255 characters.");
                }
                else
                {
                    FirstName = input;
                    loop = false;
                }

                Console.WriteLine("Press Enter to continue.");
                Console.ReadKey();
            }

            //Accept LastName
            loop = true;
            while (loop)
            {
                Console.Clear();
                Console.Write("Please enter your last Name: ");
                string? input = Console.ReadLine();


                if (2 > input.Length || input.Length > 255)
                {
                    Console.WriteLine("Last name must be between 2 and 255 characters.");
                }
                else
                {
                    LastName = input;
                    loop = false;
                }

                Console.WriteLine("Press Enter to continue.");
                Console.ReadKey();
            }

            User tmp = new User(UserName, Password, FirstName, LastName);
            return tmp;
        }

        private static bool ExitInDb(string userName)
        {
            return false;
        }

        public void Print()
        {
            Console.WriteLine($"User ID: {this.userID}");
            Console.WriteLine($"Username: {this.userName}");
            Console.WriteLine($"Password: {this.password}");
            Console.WriteLine($"User Name: {this.firstName} {this.lastName}");
            Console.WriteLine($"Default location: {this.location}");
        }

    }

}
