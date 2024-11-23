using HHADotNetCore.MiniKpayDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHADotNetCore.MiniKpayDomain.model
{
    public class TransactionResponseModel
    {
        public class ResultTransactionResponseModel
        {
            public TblTransaction Transaction { get; set; }
        }
    }
}
