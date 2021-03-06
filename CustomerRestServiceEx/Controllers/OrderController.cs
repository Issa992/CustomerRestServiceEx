﻿using System;
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

        private string connectionString = ConnectionString.connectionString;

        // GET: api/Order
        [HttpGet]
        //return book instead string
        public IEnumerable<Order> Get()
        {
            string selectString = "SELECT * FROM orders";

            //using handle the connection and it close it automatically 
            using (SqlConnection conn=new SqlConnection(connectionString))
            {
                conn.Open();

                //the command is select string 
                using ( SqlCommand command=new SqlCommand(selectString, conn))
                {

                    using ( SqlDataReader reader =command.ExecuteReader())
                    {
                        //to retrive the book into a list
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
        //to read 1 row book
        private Order ReadOrder(SqlDataReader reader)
        {
            //reader is for reading the data from DB as same type
            int id = reader.GetInt32(0);
            int customerId = reader.GetInt32(1);
            string description = reader.GetString(2);
            decimal price = reader.GetDecimal(3);

            Order order=new Order{Id = id,CustomerId = customerId,Description = description,Price = price};

            return order;

        }

        // GET: api/Order/5
        //root to spicify the method how to address this function in the rest way from browser or java or type 
        [Route("orderBy/{Customerid}")]
        public IEnumerable<Order>  Get(int Customerid)

        {
            const string selectString = "Select * from orders where CustomerId=@CustomerId";
            List<Order> result=new List<Order>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(selectString, conn))
                {
                    //assign what ever id
                    //@id the name of the place holder and more secure because sql injection
                    command.Parameters.AddWithValue("@CustomerId", Customerid);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Order order = ReadOrder(reader);
                                result.Add(order);
                            }
                        
                        }
                    }
                }
                
            }

            return result;
        }

        //Get by CustomerId
        [Route("{id}")]
        public Order GetByCustomerId(int id)
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
        public int Post([FromBody] Order value)
        {
            const string insertString = "INSERT INTO dbo.ordersCustomerId,Desciption,price)VALUES( @customerid, @description, @price )";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using ( SqlCommand command=new SqlCommand(insertString,conn))
                {
                    command.Parameters.AddWithValue("@customerid", value.CustomerId);
                    command.Parameters.AddWithValue("@description", value.Description);
                    command.Parameters.AddWithValue("@price", value.Price);
                    int rawsAffected = command.ExecuteNonQuery();

                    return rawsAffected;
                }
            }
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody] Order value)
        {
            //UPDATE dbo.orders SET CustomerId=2,Desciption='NewDescripton',price=10101 WHERE Id=1;
            const string updateOrder =
                "UPDATE orders SET CustomerId= @customerid, Desciption= @desciption, price= @price WHERE Id=@id"; //
            using ( SqlConnection conn=new SqlConnection(connectionString))
            {
                conn.Open();
                using ( SqlCommand command=new SqlCommand(updateOrder,conn))
                {
                    command.Parameters.AddWithValue("@customerid", value.CustomerId);
                    command.Parameters.AddWithValue("@desciption", value.Description);
                    command.Parameters.AddWithValue("@price", value.Price);
                    command.Parameters.AddWithValue("@id", value.Id);

                    int rawsAffected = command.ExecuteNonQuery();

                    return rawsAffected;

                }

            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            const string deleteStatement = "DELETE FROM dbo.orders WHERE Id=@id";
            using (SqlConnection conn=new SqlConnection(connectionString))
            {
                conn.Open();
                using ( SqlCommand command=new SqlCommand(deleteStatement,conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rawsAffected = command.ExecuteNonQuery();

                    return rawsAffected;
                }
            }

        }
    }
}
