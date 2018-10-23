using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRestServiceEx.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return Id + " " + FirstName + " " + LastName + " " + Year + " ";
        }
    }
}
