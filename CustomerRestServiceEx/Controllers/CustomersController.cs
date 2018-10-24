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
    public class CustomersController : ControllerBase
    {
        private string connectionString = ConnectionString.connectionString;
        // GET: api/Customers
        [HttpGet]
        public IEnumerable<Customer> Get()
        {

            string selectString = "SELECT *FROM Customer";
            using (SqlConnection conn=new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command=new SqlCommand(selectString,conn))
                {


                    using (SqlDataReader reader= command.ExecuteReader())
                    {
                        List<Customer> result=new List<Customer>();
                        while (reader.Read())
                        {
                            Customer customer = ReadCustomer(reader);
                            result.Add(customer);
                        }

                        return result;
                    }
                }
            }


            return null;

        }

        private Customer ReadCustomer(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            string firstname = reader.GetString(1);
            string lastname = reader.GetString(2);
            int year = reader.GetInt32(3);
            Customer customer=new Customer{Id = id,FirstName = firstname,LastName = lastname,Year = year};
            return customer;

        }

        // GET: api/Customers/5
        [Route("{id}")]
        public Customer Get(int id)
        {
            string selectString = "Select * from customer where id = @id";
            using (SqlConnection conn=new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command=new SqlCommand(selectString,conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using ( SqlDataReader reader=command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            return ReadCustomer(reader);

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

        // POST: api/Customers
        [HttpPost]
        public int AddCustomer([FromBody] Customer value)
        {
            const string insertString= "INSERT INTO customer (FirstName,LastName,Year) VALUES ( @firstname, @lastname , @year)";
            using (SqlConnection conn=new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command=new SqlCommand(insertString,conn))
                {
                    command.Parameters.AddWithValue("@firstname", value.FirstName);
                    command.Parameters.AddWithValue("@lastname", value.LastName);
                    command.Parameters.AddWithValue("@year", value.Year);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }

            }

        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public int UpdateCustomer(int id, [FromBody] Customer value)
        {
            const string updateCustomer =
                "UPDATE customer SET FirstName='@firstname',LastName='@lastname',Year='@year' WHERE Id=@id";
            using (SqlConnection conn=new SqlConnection(connectionString))
            {
                conn.Open();
                using ( SqlCommand command=new SqlCommand(updateCustomer,conn))
                {
                    command.Parameters.AddWithValue("@firstname", value.FirstName);
                    command.Parameters.AddWithValue("@lastname", value.LastName);
                    command.Parameters.AddWithValue("@year", value.Year);
                    command.Parameters.AddWithValue("@id", value.Id);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public int DeleteCustomer(int id)
        {
            const string deleteStatement = "delete from customer where id=@id";
            using (SqlConnection conn=new SqlConnection(connectionString))
            {
                conn.Open();

                using ( SqlCommand command=new SqlCommand(deleteStatement,conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
    }
}
