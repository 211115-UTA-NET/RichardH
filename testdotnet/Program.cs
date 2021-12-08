using System;
using System.IO;

namespace MyNamespace
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Test");

            Person newGuy = new Person("John", "Doe");
            newGuy.Name();

        }
    }

    public class Person
    {
        private string firstName;
        private string lastName;

        public Person(string first, string last)
        {
            firstName = first;
            lastName = last;
        }

        public void Name()
        {
            Console.WriteLine($"Hello, my name is {firstName} {lastName}");
        }

    }


}




