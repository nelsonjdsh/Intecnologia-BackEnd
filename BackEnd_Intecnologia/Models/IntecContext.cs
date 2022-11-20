using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BackEnd_Intecnologia.Models
{
    public partial class IntecContext : DbContext
    {
        public IntecContext()
        {
        }

        public IntecContext(DbContextOptions<IntecContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agendum> Agenda { get; set; } = null!;
        public virtual DbSet<Tblactivity> Tblactivities { get; set; } = null!;
        public virtual DbSet<Tblmessage> Tblmessages { get; set; } = null!;
        public virtual DbSet<Tblrole> Tblroles { get; set; } = null!;
        public virtual DbSet<Tblstand> Tblstands { get; set; } = null!;
        public virtual DbSet<TblstandType> TblstandTypes { get; set; } = null!;
        public virtual DbSet<Tbluser> Tblusers { get; set; } = null!;
        public virtual DbSet<TbluserActivity> TbluserActivities { get; set; } = null!;
        public virtual DbSet<TbluserStand> TbluserStands { get; set; } = null!;
        public virtual DbSet<TbluserType> TbluserTypes { get; set; } = null!;
        public virtual DbSet<Vwactivity> Vwactivities { get; set; } = null!;
        public virtual DbSet<Vwmessage> Vwmessages { get; set; } = null!;
        public virtual DbSet<Vwstand> Vwstands { get; set; } = null!;
        public virtual DbSet<Vwuser> Vwusers { get; set; } = null!;
        public virtual DbSet<VwuserActivity> VwuserActivities { get; set; } = null!;
        public virtual DbSet<VwuserStand> VwuserStands { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:BACKConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tblactivity>(entity =>
            {
                entity.HasKey(e => e.PkidActivity)
                    .HasName("PK__Activity__A117F6C409328BA2");

                entity.Property(e => e.CreationDateActivity).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Tblmessage>(entity =>
            {
                entity.HasKey(e => e.PkidMessage)
                    .HasName("PK__Message__A33138E6D58B9F02");

                entity.Property(e => e.CreationDateMessage).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdRecieverNavigation)
                    .WithMany(p => p.TblmessageIdRecieverNavigations)
                    .HasForeignKey(d => d.IdReciever)
                    .HasConstraintName("FK_Message.Id_Reciever");

                entity.HasOne(d => d.IdSenderNavigation)
                    .WithMany(p => p.TblmessageIdSenderNavigations)
                    .HasForeignKey(d => d.IdSender)
                    .HasConstraintName("FK_Message.Id_Sender");
            });

            modelBuilder.Entity<Tblrole>(entity =>
            {
                entity.HasKey(e => e.PkidRole)
                    .HasName("PK__Role__34ADFA60606934B9");

                entity.Property(e => e.CreationDateRole).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Tblstand>(entity =>
            {
                entity.HasKey(e => e.PkidStand)
                    .HasName("PK__Stand__3FD9CE34C3DCD6E0");

                entity.Property(e => e.CreationDateStand).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdStandTypeNavigation)
                    .WithMany(p => p.Tblstands)
                    .HasForeignKey(d => d.IdStandType)
                    .HasConstraintName("FK_Stand.Id_StandType");
            });

            modelBuilder.Entity<TblstandType>(entity =>
            {
                entity.HasKey(e => e.PkidStandType)
                    .HasName("PK__StandTyp__927BED91914B0838");

                entity.Property(e => e.CreationDateStandType).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Tbluser>(entity =>
            {
                entity.HasKey(e => e.PkidUser)
                    .HasName("PK__User__D03DEDCB509B23A5");

                entity.Property(e => e.CreationDateUser).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Tblusers)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User.Id_Role");

                entity.HasOne(d => d.IduserTypeNavigation)
                    .WithMany(p => p.Tblusers)
                    .HasForeignKey(d => d.IduserType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User.ID_UserType");
            });

            modelBuilder.Entity<TbluserActivity>(entity =>
            {
                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TbluserStand>(entity =>
            {
                entity.HasKey(e => new { e.FkidUser, e.FkidStand })
                    .HasName("PK__User_Sta__83C07128AB526080");

                entity.Property(e => e.CreationDateUserStand).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FkidStandNavigation)
                    .WithMany(p => p.TbluserStands)
                    .HasForeignKey(d => d.FkidStand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Stand.Id_Stand");

                entity.HasOne(d => d.FkidUserNavigation)
                    .WithMany(p => p.TbluserStands)
                    .HasForeignKey(d => d.FkidUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Stand.Id_User");
            });

            modelBuilder.Entity<TbluserType>(entity =>
            {
                entity.HasKey(e => e.PkidUserType)
                    .HasName("PK__UserType__DC992C7E143BBBC2");

                entity.Property(e => e.CreationDateUserType).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Vwactivity>(entity =>
            {
                entity.ToView("VWActivity");
            });

            modelBuilder.Entity<Vwmessage>(entity =>
            {
                entity.ToView("VWMessage");

                entity.Property(e => e.PkidMessage).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Vwstand>(entity =>
            {
                entity.ToView("VWStand");
            });

            modelBuilder.Entity<Vwuser>(entity =>
            {
                entity.ToView("VWUser");

                entity.Property(e => e.IdUser).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwuserActivity>(entity =>
            {
                entity.ToView("VWUserActivity");

                entity.Property(e => e.PkidActivity).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwuserStand>(entity =>
            {
                entity.ToView("VWUserStand");

                entity.Property(e => e.PkidStand).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
