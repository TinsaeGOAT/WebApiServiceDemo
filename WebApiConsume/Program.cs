
using Newtonsoft.Json;
using WebApiServiceDemo.Models;

namespace WebApiConsume
{
    internal class Program
    {
        //Intializers
        private readonly Uri baseAddress = new Uri("https://localhost:44301/api");
        private readonly HttpClient client;
        private static List<CustomerViewModel> _modelViews = new List<CustomerViewModel>();
        private readonly HttpResponseMessage responseMessage;

        public Program()
        {
            client = new HttpClient
            {
                BaseAddress = baseAddress
            };
            //responseMessage = client.GetAsync(baseAddress + "/customers").Result;
        }
        static void Main(string[] args)
        {
            Uri baseAddress = new("https://localhost:44301/api");
            HttpClient client = new()
            {
                BaseAddress = baseAddress
            };
            HttpResponseMessage responseMessage = new();

            responseMessage = client.GetAsync(baseAddress + "/customers").Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                var data = responseMessage.Content.ReadAsStringAsync().Result;
                _modelViews = JsonConvert.DeserializeObject<List<CustomerViewModel>>(data);
            }

            Console.WriteLine($"---------------------------------------------------------------------------------------------------");
            Console.WriteLine("\t \t \t Customers Info Details");
            foreach (var modelView in _modelViews) 
            {
                Console.WriteLine($"---------------------------------------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine(modelView.Customer_id + " "
                                + modelView.First_name  + " "
                                + modelView.Last_name   + " "
                                + modelView.Phone       + " "
                                + modelView.Email       + " "
                                + modelView.Street      + " "
                                + modelView.City        + " "
                                + modelView.State       + " "
                                +modelView.Zip_code);
            }
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}