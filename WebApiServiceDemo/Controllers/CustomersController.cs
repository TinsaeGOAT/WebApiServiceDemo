using DataAccessLayer;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiServiceDemo.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly DevelopmentEntities db;

        public CustomersController()
        {
            db = new DevelopmentEntities();
        }


        [HttpGet]
        public HttpResponseMessage GetCustomers()
        {
            var customers = db.customers.Take(10).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, customers);

        }

        [HttpGet]
        public HttpResponseMessage GetCustomerById(int id)
        {
            var customer = db.customers.Find(id);
            if (customer == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, id);

            return Request.CreateResponse(HttpStatusCode.OK, customer);

        }

        [HttpPost]
        public HttpResponseMessage AddCustomerData([FromUri] customer cust)
        {
            if (cust != null)
            {
                db.customers.Add(cust);
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

        }

        [HttpPut]
        public HttpResponseMessage UpdateCustomer([FromUri] customer cust, int id)
        {

            if (cust != null)
            {
                db.Entry(cust).State = EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, id);
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        [HttpDelete]
        public HttpResponseMessage DeleteCustomer(int id)
        {
            try
            {
                var result = db.customers.Remove(db.customers.FirstOrDefault(c => c.customer_id == id));
                db.Entry(result).State = EntityState.Deleted;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (System.Exception)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest); ;
            }




        }
    }
}
