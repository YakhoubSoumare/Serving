using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serving
{
    internal class Reservation
    {
        List<ICustomer> customers;
        public Reservation()
        {
            customers = new List<ICustomer>();
        }

        public void Reserve(Customer customer)
        {

            customers.Add(customer);
        }

        public List<ICustomer> ViewReservations()
        {
            return customers;
        }

        public void CancelReservation(ICustomer customer)
        {
            if (customers.Contains(customer))
            {
                customers.Remove(customer);
            }
        }
    }
}
