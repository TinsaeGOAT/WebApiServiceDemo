using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using WebApiServiceDemo.Models;
using Newtonsoft;
using Newtonsoft.Json;

namespace WebAppMVC.Controllers
{
    public class CustomerController : Controller
    {
        //Intializers
        private readonly Uri baseAddress = new Uri("https://localhost:44301/api");
        private readonly HttpClient client;
        private List<CustomerViewModel> _modelViews = new List<CustomerViewModel>();
        private readonly HttpResponseMessage responseMessage; 
        public CustomerController()
        {
            client = new HttpClient
            {
                BaseAddress = baseAddress
            };
            responseMessage = client.GetAsync(baseAddress + "/customers").Result;
        }
        // GET: Customer
        public ActionResult Index()
        {
            
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = responseMessage.Content.ReadAsStringAsync().Result;
                _modelViews = JsonConvert.DeserializeObject<List<CustomerViewModel>>(data);
            }
            return View(_modelViews);
        }
    }
}