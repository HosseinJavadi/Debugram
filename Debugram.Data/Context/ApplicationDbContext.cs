using Debugram.Common.Utilities;
using Debugram.Entities.ModelDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugram.Data.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<Connection> Connections { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Still> Stills { get; set; } = null!;
        public virtual DbSet<StillType> StillTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserStill> UserStills { get; set; } = null!;


        public override int SaveChanges()
        {
            _cleanString();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(cancellationToken);
        }
        void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(n => n.State == EntityState.Added || n.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;
                var properties = item.Entity.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                    .Where(n => n.CanWrite && n.CanRead && n.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var propVal = (string)property.GetValue(item.Entity, null);
                    if (propVal.HasValue())
                    {
                        var newVal = propVal.Fa2En().FixPersianChars();
                        if (newVal == propVal)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article", "Article");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Article)
                    .HasForeignKey<Article>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Article_Menu");
            });

            modelBuilder.Entity<Connection>(entity =>
            {
                entity.ToTable("Connection", "Profile");

                entity.Property(e => e.IsFllow)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu", "Article");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Url).HasMaxLength(50);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Menu_Menu");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_Post");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post", "Article");

                entity.HasOne(d => d.StillType)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.StillTypeId)
                    .HasConstraintName("FK_Post_StillType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_User");
            });

            modelBuilder.Entity<Still>(entity =>
            {
                entity.ToTable("Still", "Profile");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(30);
            });

            modelBuilder.Entity<StillType>(entity =>
            {
                entity.ToTable("StillType", "Article");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "Profile");

                entity.Property(e => e.AvatarAddress)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Biography).HasMaxLength(300);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Password)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserStill>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.StillId })
                    .HasName("PK_UserStill_1")
                    .IsClustered(false);

                entity.ToTable("UserStill", "Profile");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StillAmount).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Still)
                    .WithMany(p => p.UserStills)
                    .HasForeignKey(d => d.StillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserStill_Still");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserStills)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserStill_User");
            });


        }
    }
}
