using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using ToDoApp;
using Newtonsoft.Json;

namespace Program
{


    class Program
    {
        public static async Task Main(string[] args)
        {

            
            string uri = "https://localhost:7153/";
            //try
            //{

            //    HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/todos/1");
            //    response.EnsureSuccessStatusCode();
            //    Console.WriteLine(await response.Content.ReadAsStringAsync());

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //using (HttpClient client = new HttpClient()) 
            //using (HttpResponseMessage response = await client.GetAsync(uri)) 
            //using (HttpContent content = response.Content)
            //{
            //    string result = await content.ReadAsStringAsync();
            //    if (result.Length >= 50)
            //    {
            //        result = result.Substring(0, 50);
            //    }
            //    Console.WriteLine(result);
            //}

            CallAPIAsync(uri)
                .Wait();

            GetAll(uri)
                .Wait();

            GetOne(uri, 1)
                .Wait();
        }

        static async Task CallAPIAsync(string uri)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(uri);


            try
            {
                HttpResponseMessage response = await client.GetAsync("api/todoitems");
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }

        static async Task GetAll(string uri)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            try
            {
                HttpResponseMessage response = await client.GetAsync("api/todoitems");
                Console.WriteLine("ID\tName\tComplete");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    List<ToDoItem> content = JsonConvert.DeserializeObject<List<ToDoItem>>(res);
                    if (content == null)
                    {
                        return;
                    }
                    else
                    {
                        foreach (ToDoItem item in content)
                        {
                            Console.WriteLine("{0}\t{1}\t{2}", item.id, item.name, item.isComplete);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }                     
        }

        static async Task GetOne(string uri, int id)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            HttpResponseMessage response = await client.GetAsync($"api/todoitems/{id}");

            if (response.IsSuccessStatusCode)
            {
                ToDoItem item = JsonConvert.DeserializeObject<ToDoItem>(await response.Content.ReadAsStringAsync());

                Console.WriteLine("Id: {0} \t Name: {1} \t Complete: {2}", item.id, item.name, item.isComplete);
            }
            else
            {
                Console.WriteLine("Internal server Error");
            }

        }
    }
}