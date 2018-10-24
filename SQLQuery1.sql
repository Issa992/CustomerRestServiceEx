--SELECT dbo.customer.Id,dbo.customer.FirstName,dbo.customer.LastName,dbo.customer.Year FROM dbo.customer INNER JOIN dbo.orders ON orders.CustomerId = customer.Id;
--select * from dbo.customer where dbo.orders.CustomerId=dbo.customer.Id ORDER BY FirstName;

Select * from orders where CustomerId=3