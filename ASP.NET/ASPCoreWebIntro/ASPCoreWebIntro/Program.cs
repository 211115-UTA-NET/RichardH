using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Text;
using System.Xml.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
WebApplication app = builder.Build();

//app.MapGet("/", () => "Hello World!");



// after you have a WebApplication, you need to construct its
// request-processing pipeline using components called "middleware".

//This call adds a middleware to the pipeline, each middleware runs in sequence.

//app.Use((context, next) =>
//{
//    if (context.Request.Query["authenticated"] == "true")
//    {
//        // This middleware is done, let hte next one in the pipeline take over"
//        next(context);

//    }
//    else
//    {
//        // if we don't invoke "next", this middleware is "short-circuiting" the pipeline - 
//        // it should finish setting up the response.

//        context.Response.StatusCode = 401;
//        context.Response.ContentType = "text/plain";
//        context.Response.Body.Write(Encoding.UTF8.GetBytes("Error, not authenticated"));

//    }
//    return Task.CompletedTask;
//});


//// this call runs the app.
//app.Run();

//app.Map("/map1", context =>
//{
//    context.Response.Body.Write(Encoding.UTF8.GetBytes("hello from map 1"));
//    return Task.CompletedTask;
//});


//app.Map("/map2", context =>
//{
//    context.Response.Body.Write(Encoding.UTF8.GetBytes("hello from map 2"));
//    return Task.CompletedTask;
//});



//app.Run(context =>
//{
//    // the HttpContext parameter (context) gives access to all the request data
//    // and lets you modify all the response data.

//    string path = context.Request.Path;
//    string dataValue = context.Request.Query["data"];




//    context.Response.StatusCode = 200;

//    // serializing the string as bytes using UTF8 encoding, and writing it to the 
//    // HTTP response directly
//    context.Response.Body.Write(Encoding.UTF8.GetBytes($"Path was: {path} Data was: {dataValue}"));


//    // need this line so it compiles
//    // (really this delegate should be async, but we haven't done this yet.
//    return Task.CompletedTask;
//});










app.Map("/Exercise1", context =>
{
    context.Response.Body.Write(Encoding.UTF8.GetBytes("Displaying XML serialized Records\n"));

    string filePath = "C:/Revature/RichardH/RockPaperScissorsApp.App/RockPaperScissorsApp.App/history.xml";

    XmlSerializer serializer = new(typeof(List<Records>));

    using (StreamReader? reader = new(filePath))
    {
        try
        {
            var record = (List<Records>?)serializer.Deserialize(reader);

            if (record is null) throw new InvalidDataException();

            foreach (Records entry in record)
            {
                context.Response.Body.Write(Encoding.UTF8.GetBytes($"Player: {entry.PlayerName}\n"));
                context.Response.Body.Write(Encoding.UTF8.GetBytes($"Date: {entry.Time}\n"));
                context.Response.Body.Write(Encoding.UTF8.GetBytes($"Result: {entry.Res}\n"));
                context.Response.Body.Write(Encoding.UTF8.GetBytes($"Player Throw: {entry.PlayerThrow}\n"));
                context.Response.Body.Write(Encoding.UTF8.GetBytes($"Computer Throw: {entry.ComputerThrow}\n"));
            }
        }
        catch (IOException)
        {
            context.Response.Body.Write(Encoding.UTF8.GetBytes("File Not Found"));
        }
    }

    return Task.CompletedTask;
});


app.Map("/Exercise2", context =>
{
    string filePath = "C:/TestFolder/" + context.Request.Query["path"];
    context.Response.Body.Write(Encoding.UTF8.GetBytes($"Display file at {filePath}\n"));

    if (File.Exists(filePath))
    {
        context.Response.Body.Write(Encoding.UTF8.GetBytes(File.ReadAllText(filePath)));
    }
    else
    {
        context.Response.Body.Write(Encoding.UTF8.GetBytes("File not found."));

    }

    return Task.CompletedTask;
});


// this call runs the app.
app.Run();




public class Records
{
    [XmlAttribute]
    public string? Res { get; set; }
    public string? PlayerName { get; set; }
    public string? PlayerThrow { get; set; }
    public string? ComputerThrow { get; set; }
    public DateTime Time { get; set; }
}



