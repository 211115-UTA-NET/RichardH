using System.Data.SqlClient;

string? connectionString = File.ReadAllText("c:/Revature/DEMO_RPS_CS.txt"); 
Console.WriteLine("Establishing DB Connection...");
using SqlConnection connection = new(connectionString);
connection.Open();
Console.WriteLine("Connection Established.");

using SqlCommand command = new("SELECT * FROM TEST;", connection);
using SqlDataReader reader = command.ExecuteReader();
while (reader.Read())
{

    string field = reader.GetString(0);

    Console.WriteLine($"\"{field}\"");
}
connection.Close();