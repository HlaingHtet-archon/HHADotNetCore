using HHADotNetCore.MiniKpayDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHADotNetCore.MiniKpayDomain.model
{
    public class TransferResponseModel
    {
        public BaseResponseModel responseModel { get; set; }

        public TblTransaction Transaction { get; set; }
    }

    public class ResultTransferResponseModel
    {
        public TblTransaction Transaction { get; set; }
    }
}
