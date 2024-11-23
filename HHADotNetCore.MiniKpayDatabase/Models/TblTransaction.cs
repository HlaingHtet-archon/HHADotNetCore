using System;
using System.Collections.Generic;

namespace HHADotNetCore.MiniKpayDatabase.Models;

public partial class TblTransaction
{
    public int TransactionId { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }
}
