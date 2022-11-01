using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serving
{
    internal class Customer: ICustomer
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public int Table { get; set; }
        public string Time { get; set; }

        public Customer(string name, string date, int table, string time)
        {
            Date = date;
            Name = name;
            Table = table;
            Time = time;
        }
    }
}
