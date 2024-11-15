using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BankPay.Database.Models;

public partial class TblUser
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public decimal? Balance { get; set; }

    public string Pin { get; set; } = null!;

    public bool? DeleteFlag { get; set; }

    [JsonIgnore]
    public virtual ICollection<TblDeposit> TblDeposits { get; set; } = new List<TblDeposit>();

    [JsonIgnore]
    public virtual ICollection<TblTransaction> TblTransactionReceiverMobileNumberNavigations { get; set; } = new List<TblTransaction>();

    [JsonIgnore]
    public virtual ICollection<TblTransaction> TblTransactionSenderMobileNumberNavigations { get; set; } = new List<TblTransaction>();

    [JsonIgnore]
    public virtual ICollection<TblWithdraw> TblWithdraws { get; set; } = new List<TblWithdraw>();
}
