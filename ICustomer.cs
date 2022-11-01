using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serving
{
    internal interface ICustomer
    {
        string Name { get; set; }
        string Date { get; set; }
        int Table { get; set; }
        string Time { get; set; }
    }
}
