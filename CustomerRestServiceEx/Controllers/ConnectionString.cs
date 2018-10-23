using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRestServiceEx.Controllers
{
    public class ConnectionString
    {
        //Server=tcp:customerserverex1rest.database.windows.net,1433;Initial Catalog=customerDb;Persist Security Info=False;User ID={your_username};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
        public static readonly string connectionString = 
        "Server=tcp:customerserverex1rest.database.windows.net,1433;Initial Catalog=customerDb;Persist Security Info=False;User ID=issa;Password=0988966947Ab;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
