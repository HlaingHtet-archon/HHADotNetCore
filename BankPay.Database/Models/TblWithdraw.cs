using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BankPay.Database.Models;

public partial class TblWithdraw
{
    public int WithdrawId { get; set; }

    public string MobileNumber { get; set; } = null!;

    public decimal Balance { get; set; }

    public bool? DeleteFlag { get; set; }

    [JsonIgnore]
    public virtual TblUser MobileNumberNavigation { get; set; } = null!;
}
