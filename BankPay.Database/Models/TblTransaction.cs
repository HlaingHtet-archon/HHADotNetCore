using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BankPay.Database.Models;

public partial class TblTransaction
{
    public Guid TransactionNumber { get; set; }

    public string TransactionType { get; set; } = null!;

    public string SenderMobileNumber { get; set; } = null!;

    public string? ReceiverMobileNumber { get; set; }

    public decimal Amount { get; set; }

    public string Pin { get; set; } = null!;

    public DateTime TransactionDate { get; set; }

    public string? Status { get; set; }

    public bool? DeleteFlag { get; set; }

    [JsonIgnore]
    public virtual TblUser? ReceiverMobileNumberNavigation { get; set; }

    [JsonIgnore]
    public virtual TblUser SenderMobileNumberNavigation { get; set; } = null!;
}
