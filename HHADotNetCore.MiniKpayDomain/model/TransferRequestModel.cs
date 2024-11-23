using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHADotNetCore.MiniKpayDomain.model
{
    public class TransferRequestModel
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public decimal Amount { get; set; }
        public string PinCode { get; set; }
    }
}
