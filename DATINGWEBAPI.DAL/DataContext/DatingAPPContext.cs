using System;
using System.Collections.Generic;
using DATINGWEBAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace DATINGWEBAPI.DAL.DataContext;

public partial class DatingAPPContext : DbContext
{
    public DatingAPPContext(DbContextOptions<DatingAPPContext> options)
        : base(options)
    {
    }

    public virtual DbSet<APPLICATION_LOG> APPLICATION_LOGs { get; set; }

    public virtual DbSet<CONTACT> CONTACTs { get; set; }

    public virtual DbSet<DEVICE> DEVICEs { get; set; }

    public virtual DbSet<INTEREST> INTERESTs { get; set; }

    public virtual DbSet<MATCH> MATCHEs { get; set; }

    public virtual DbSet<NOTIFICATION> NOTIFICATIONs { get; set; }

    public virtual DbSet<NOTIFICATIONSETTING> NOTIFICATIONSETTINGs { get; set; }

    public virtual DbSet<SWIPE> SWIPEs { get; set; }

    public virtual DbSet<USER> USERs { get; set; }

    public virtual DbSet<USERINTEREST> USERINTERESTs { get; set; }

    public virtual DbSet<USER_MEDIum> USER_MEDIAs { get; set; }

    public virtual DbSet<USER_STORy> USER_STORIEs { get; set; }

    public virtual DbSet<VERIFICATIONCODE> VERIFICATIONCODEs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<APPLICATION_LOG>(entity =>
        {
            entity.ToTable("APPLICATION_LOGS");

            entity.Property(e => e.IPADDRESS).HasMaxLength(50);
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.USEREMAIL).HasMaxLength(255);
        });

        modelBuilder.Entity<CONTACT>(entity =>
        {
            entity.HasKey(e => e.CONTACTID).HasName("PK__CONTACTS__799118684D8763C6");

            entity.ToTable("CONTACTS");

            entity.Property(e => e.CONTACTNAME)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.CONTACTPHONE)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.USER).WithMany(p => p.CONTACTs)
                .HasForeignKey(d => d.USERID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONTACTS_USERS");
        });

        modelBuilder.Entity<DEVICE>(entity =>
        {
            entity.HasKey(e => e.DEVICEID).HasName("PK__DEVICES__9EE3C9A48322DB4C");

            entity.ToTable("DEVICES");

            entity.Property(e => e.CREATEDAT)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DEVICETOKEN)
                .IsRequired()
                .HasMaxLength(500);
            entity.Property(e => e.PLATFORM)
                .IsRequired()
                .HasMaxLength(10);

            entity.HasOne(d => d.USER).WithMany(p => p.DEVICEs)
                .HasForeignKey(d => d.USERID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEVICES_USERS");
        });

        modelBuilder.Entity<INTEREST>(entity =>
        {
            entity.HasKey(e => e.INTERESTID).HasName("PK__INTEREST__2E93781D8EB88BB2");

            entity.ToTable("INTERESTS");

            entity.Property(e => e.ICONURL).HasMaxLength(500);
            entity.Property(e => e.NAME)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<MATCH>(entity =>
        {
            entity.HasKey(e => new { e.USER1ID, e.USER2ID });

            entity.ToTable("MATCHES");

            entity.Property(e => e.MATCHEDAT).HasColumnType("datetime");

            entity.HasOne(d => d.USER1).WithMany(p => p.MATCHUSER1s)
                .HasForeignKey(d => d.USER1ID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MATCHES_USER1");

            entity.HasOne(d => d.USER2).WithMany(p => p.MATCHUSER2s)
                .HasForeignKey(d => d.USER2ID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MATCHES_USER2");
        });

        modelBuilder.Entity<NOTIFICATION>(entity =>
        {
            entity.HasKey(e => e.NOTIFICATIONID).HasName("PK__NOTIFICA__EAF93BF46FFB8192");

            entity.ToTable("NOTIFICATIONS");

            entity.Property(e => e.CREATEDAT)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ISREAD).HasDefaultValue(false);
            entity.Property(e => e.TITLE).HasMaxLength(200);
            entity.Property(e => e.TYPE)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.USER).WithMany(p => p.NOTIFICATIONs)
                .HasForeignKey(d => d.USERID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTIFICATIONS_USERS");
        });

        modelBuilder.Entity<NOTIFICATIONSETTING>(entity =>
        {
            entity.HasKey(e => e.NOTIFICATIONSETTINGID).HasName("PK__NOTIFICA__C831D3C5C7299F14");

            entity.ToTable("NOTIFICATIONSETTINGS");

            entity.Property(e => e.CREATEDAT)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.USER).WithMany(p => p.NOTIFICATIONSETTINGs)
                .HasForeignKey(d => d.USERID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTIFICATIONSETTINGS_USERS");
        });

        modelBuilder.Entity<SWIPE>(entity =>
        {
            entity.HasKey(e => new { e.SWIPERID, e.SWIPEDID });

            entity.ToTable("SWIPES");

            entity.Property(e => e.TIMESTAMP).HasColumnType("datetime");

            entity.HasOne(d => d.SWIPED).WithMany(p => p.SWIPESWIPEDs)
                .HasForeignKey(d => d.SWIPEDID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SWIPES_SWIPED");

            entity.HasOne(d => d.SWIPER).WithMany(p => p.SWIPESWIPERs)
                .HasForeignKey(d => d.SWIPERID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SWIPES_SWIPER");
        });

        modelBuilder.Entity<USER>(entity =>
        {
            entity.HasKey(e => e.USERID).HasName("PK__USERS__7B9E7F3579584A1A");

            entity.ToTable("USERS");

            entity.Property(e => e.BIO).HasMaxLength(1000);
            entity.Property(e => e.BIRTHDAY).HasColumnType("datetime");
            entity.Property(e => e.CREATEDAT)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DELETEDAT).HasColumnType("datetime");
            entity.Property(e => e.FIRSTNAME).HasMaxLength(100);
            entity.Property(e => e.GENDER).HasMaxLength(10);
            entity.Property(e => e.LASTLOGIN)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LASTNAME).HasMaxLength(100);
            entity.Property(e => e.LOCATION).HasMaxLength(255);
            entity.Property(e => e.PHONENUMBER).HasMaxLength(50);
            entity.Property(e => e.PROFILEIMAGEURL).HasMaxLength(500);
            entity.Property(e => e.ROLE)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("User");
            entity.Property(e => e.UPDATEDAT)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<USERINTEREST>(entity =>
        {
            entity.HasKey(e => e.USERINTERESTID).HasName("PK__USERINTE__F48CD6800197BAEE");

            entity.ToTable("USERINTERESTS");

            entity.HasOne(d => d.INTEREST).WithMany(p => p.USERINTERESTs)
                .HasForeignKey(d => d.INTERESTID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USERINTERESTS_INTERESTS");

            entity.HasOne(d => d.USER).WithMany(p => p.USERINTERESTs)
                .HasForeignKey(d => d.USERID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USERINTERESTS_USERS");
        });

        modelBuilder.Entity<USER_MEDIum>(entity =>
        {
            entity.HasKey(e => e.MEDIAID).HasName("PK__USER_MED__66244A1AE35D9E86");

            entity.ToTable("USER_MEDIA");

            entity.Property(e => e.CREATED_AT)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MEDIA_TYPE)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Image");
            entity.Property(e => e.MEDIA_URL)
                .IsRequired()
                .HasMaxLength(1000);
            entity.Property(e => e.STORAGE_ID).HasMaxLength(255);

            entity.HasOne(d => d.USER).WithMany(p => p.USER_MEDIa)
                .HasForeignKey(d => d.USERID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USER_MEDI__USERI__6C190EBB");
        });

        modelBuilder.Entity<USER_STORy>(entity =>
        {
            entity.HasKey(e => e.STORYID).HasName("PK__USER_STO__5F09C60FAF03F487");

            entity.ToTable("USER_STORIES");

            entity.Property(e => e.CAPTION).HasMaxLength(500);
            entity.Property(e => e.CREATED_AT)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EXPIRES_AT).HasColumnType("datetime");
            entity.Property(e => e.IS_ACTIVE).HasDefaultValue(true);
            entity.Property(e => e.MEDIA_TYPE)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Image");
            entity.Property(e => e.MEDIA_URL)
                .IsRequired()
                .HasMaxLength(1000);
            entity.Property(e => e.STORAGE_ID).HasMaxLength(255);
            entity.Property(e => e.UPDATED_AT)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.USER).WithMany(p => p.USER_STORies)
                .HasForeignKey(d => d.USERID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_STORIES_USERS");
        });

        modelBuilder.Entity<VERIFICATIONCODE>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__VERIFICA__3214EC271120D578");

            entity.ToTable("VERIFICATIONCODES");

            entity.Property(e => e.CODE)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CREATEDAT)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EXPIRESAT).HasColumnType("datetime");
            entity.Property(e => e.PHONENUMBER)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.USER).WithMany(p => p.VERIFICATIONCODEs)
                .HasForeignKey(d => d.USERID)
                .HasConstraintName("FK_VERIFICATIONCODES_USERS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

