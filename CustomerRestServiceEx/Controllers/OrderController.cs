using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CustomerRestServiceEx.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRestServiceEx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //SELECT dbo.customer.Id,dbo.customer.FirstName,dbo.customer.LastName,dbo.customer.Year FROM dbo.customer  INNER JOIN dbo.orders ON orders.CustomerId = customer.Id
        //Select * from orders where CustomerId=3

        private string connectionString = ConnectionString.connectionString;

        // GET: api/Order
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            string selectString = "SELECT * FROM orders";

            using (SqlConnection conn=new SqlConnection(connectionString))
            {
                conn.Open();
                using ( SqlCommand command=new SqlCommand(selectString, conn))
                {

                    using ( SqlDataReader reader =command.ExecuteReader())
                    {
                        List<Order> result=new List<Order>();

                        while (reader.Read())
                        {
                            Order order = ReadOrder(reader);

                            result.Add(order);

                            
                        }
                        return result;
                    }
                }
            }
            return null;
        }

        private Order ReadOrder(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            int customerId = reader.GetInt32(1);
            string description = reader.GetString(2);
            decimal price = reader.GetDecimal(3);

            Order order=new Order{Id = id,CustomerId = customerId,Description = description,Price = price};

            return order;

        }

        // GET: api/Order/5
        [Route("{id}")]
        public Order Get(int id)
        {
            string selectString = "Select * from orders where id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return ReadOrder(reader);

                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }

            return null;
        }



    // POST: api/Order
    [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
