using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BankPay.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblDeposit> TblDeposits { get; set; }

    public virtual DbSet<TblTransaction> TblTransactions { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblWithdraw> TblWithdraws { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Bank;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblDeposit>(entity =>
        {
            entity.HasKey(e => e.DepositId).HasName("PK__Tbl_Depo__AB60DF71709E5D10");

            entity.ToTable("Tbl_Deposit");

            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DeleteFlag).HasDefaultValueSql("((0))");
            entity.Property(e => e.MobileNumber).HasMaxLength(15);

            entity.HasOne(d => d.MobileNumberNavigation).WithMany(p => p.TblDeposits)
                .HasPrincipalKey(p => p.MobileNumber)
                .HasForeignKey(d => d.MobileNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Depos__Mobil__3D5E1FD2");
        });

        modelBuilder.Entity<TblTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionNumber).HasName("PK__Tbl_Tran__E733A2BEFE82E30D");

            entity.ToTable("Tbl_Transaction");

            entity.Property(e => e.TransactionNumber).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DeleteFlag).HasDefaultValueSql("((0))");
            entity.Property(e => e.Pin)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ReceiverMobileNumber).HasMaxLength(15);
            entity.Property(e => e.SenderMobileNumber).HasMaxLength(15);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(dateadd(minute,(390),getdate()))")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ReceiverMobileNumberNavigation).WithMany(p => p.TblTransactionReceiverMobileNumberNavigations)
                .HasPrincipalKey(p => p.MobileNumber)
                .HasForeignKey(d => d.ReceiverMobileNumber)
                .HasConstraintName("FK__Tbl_Trans__Recei__0C85DE4D");

            entity.HasOne(d => d.SenderMobileNumberNavigation).WithMany(p => p.TblTransactionSenderMobileNumberNavigations)
                .HasPrincipalKey(p => p.MobileNumber)
                .HasForeignKey(d => d.SenderMobileNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Trans__Sende__0B91BA14");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Tbl_User__1788CC4C59CD5F5E");

            entity.ToTable("Tbl_User");

            entity.HasIndex(e => e.MobileNumber, "UQ__Tbl_User__250375B1FAD611F3").IsUnique();

            entity.Property(e => e.Balance)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DeleteFlag).HasDefaultValueSql("((0))");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.MobileNumber).HasMaxLength(15);
            entity.Property(e => e.Pin)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<TblWithdraw>(entity =>
        {
            entity.HasKey(e => e.WithdrawId).HasName("PK__Tbl_With__435D94E2C7AA223B");

            entity.ToTable("Tbl_Withdraw");

            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DeleteFlag).HasDefaultValueSql("((0))");
            entity.Property(e => e.MobileNumber).HasMaxLength(15);

            entity.HasOne(d => d.MobileNumberNavigation).WithMany(p => p.TblWithdraws)
                .HasPrincipalKey(p => p.MobileNumber)
                .HasForeignKey(d => d.MobileNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Withd__Mobil__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
