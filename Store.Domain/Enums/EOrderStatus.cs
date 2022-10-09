using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Enums
{
    public enum EOrderStatus: int
    {
        WaitingPayment = 1,
        WaitingDelivery = 2,
        Canceled = 3,
    }
}
