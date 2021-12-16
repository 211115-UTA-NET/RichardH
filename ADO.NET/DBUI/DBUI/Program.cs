using System.Data.SqlClient;

string? connectionString = File.ReadAllText("../../../../CS.txt");

ListBooks();

Console.WriteLine("Enter a book title: ");
string? title = Console.ReadLine();
Console.WriteLine("Enter the number of pages: ");
int pages = int.Parse(Console.ReadLine().ToString());
AddNewBook(title, pages);

void ListBooks()
{
    Console.WriteLine("Establishing DB Connection...");
    using SqlConnection connection = new(connectionString);
    connection.Open();
    Console.WriteLine("Connection Established.");

    using SqlCommand command = new("SELECT * FROM Books;", connection);
    using SqlDataReader reader = command.ExecuteReader();

    while (reader.Read())
    {

        string title = reader.GetString(0);
        int pages = reader.GetInt32(2);

        Console.WriteLine($"\"{title}\" with {pages} pages");
    }
    connection.Close();
}

void AddNewBook(string title, int pages)
{
    Console.WriteLine("Establishing DB Connection...");
    using SqlConnection connection = new(connectionString);
    connection.Open();
    Console.WriteLine("Connection Established.");
    using SqlCommand command = new($"INSERT INTO Books (Title, Author, Pages, GenreID, PublisherID) VALUES ('{title}', 'E.F.Codd', {pages}, 1, 2);", connection);
    command.ExecuteNonQuery();
    connection.Close();
   }





