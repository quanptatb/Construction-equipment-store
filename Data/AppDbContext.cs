using Microsoft.EntityFrameworkCore;
using VietMachWeb.Models.Entities;

namespace VietMachWeb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //  Audit
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<AuditAction> AuditActions { get; set; }
        public DbSet<AuditEntityType> AuditEntityTypes { get; set; }

        //  User - Role
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        //  CMS
        public DbSet<Page> Pages { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactStatus> ContactStatuses { get; set; }

        //  Media 
        public DbSet<Media> Media { get; set; }

        //  Category
        public DbSet<Category> Categories { get; set; }

        //  Post
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostStatus> PostStatuses { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<PostTagMap> PostTagMaps { get; set; }

        //  Product
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductDocument> ProductDocuments { get; set; }
        public DbSet<ProductSpec> ProductSpecs { get; set; }
        public DbSet<SpecKey> SpecKeys { get; set; }
        public DbSet<FileType> FileTypes { get; set; }

        //  Setting
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder e)
        {
            base.OnModelCreating(e);

            // ======================= ROLE =======================
            e.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(x => x.NormalizedName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(x => x.NormalizedName)
                    .IsUnique();

                // Seed
                entity.HasData(
                    new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                    new Role { Id = 2, Name = "Manager", NormalizedName = "MANAGER" },
                    new Role { Id = 3, Name = "Staff", NormalizedName = "STAFF" },
                    new Role { Id = 4, Name = "Customer", NormalizedName = "CUSTOMER" },
                    new Role { Id = 5, Name = "Guest", NormalizedName = "GUEST" }
                );
            });

            // ======================= USER =======================

            e.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasIndex(x => x.Email)
                    .IsUnique();

                entity.Property(x => x.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // ================= RELATION =================
                entity.HasMany(x => x.AuditLogs)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(x => x.UserRoles)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ================= USER ROLE =================
            e.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles");

                // Composite key
                entity.HasKey(x => new { x.UserId, x.RoleId });

                // ================= RELATION =================
                entity.HasOne(x => x.User)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Role)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ================= PAGE =================
            e.Entity<Page>(entity =>
            {
                entity.ToTable("Pages");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.Slug)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasIndex(x => x.Slug)
                    .IsUnique();

                entity.Property(x => x.Content)
                    .HasColumnType("nvarchar(max)");

                entity.Property(x => x.MetaTitle)
                    .HasMaxLength(200);

                entity.Property(x => x.MetaDescription)
                    .HasMaxLength(500);

                entity.Property(x => x.IsActive)
                    .HasDefaultValue(true);

                entity.Property(x => x.UpdatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");
            });

            // ================= BANNER =================
            e.Entity<Banner>(entity =>
            {
                entity.ToTable("Banners");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title)
                    .HasMaxLength(200);

                entity.Property(x => x.Subtitle)
                    .HasMaxLength(300);

                entity.Property(x => x.LinkUrl)
                    .HasMaxLength(500);

                entity.Property(x => x.ButtonText)
                    .HasMaxLength(100);

                entity.Property(x => x.SortOrder)
                    .HasDefaultValue(0);

                entity.Property(x => x.IsActive)
                    .HasDefaultValue(true);

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // RELATION
                entity.HasOne(x => x.Media)
                    .WithMany()
                    .HasForeignKey(x => x.MediaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => x.SortOrder);
            });

            // ================= PARTNER =================
            e.Entity<Partner>(entity =>
            {
                entity.ToTable("Partners");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.WebsiteUrl)
                    .HasMaxLength(500);

                entity.Property(x => x.SortOrder)
                    .HasDefaultValue(0);

                entity.Property(x => x.IsActive)
                    .HasDefaultValue(true);

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // RELATION
                entity.HasOne(x => x.Media)
                    .WithMany()
                    .HasForeignKey(x => x.MediaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => x.SortOrder);
            });

            // ================= CONTACT =================
            e.Entity<Contact>(entity =>
            {
                entity.ToTable("Contacts");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(x => x.Phone)
                    .HasMaxLength(20);

                entity.Property(x => x.Subject)
                    .HasMaxLength(200);

                entity.Property(x => x.Message)
                    .HasColumnType("nvarchar(max)");

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(x => x.ReplyContent)
                    .HasColumnType("nvarchar(max)");

                // RELATION
                entity.HasOne(x => x.Status)
                    .WithMany(x => x.Contacts)
                    .HasForeignKey(x => x.StatusId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Replier)
                    .WithMany()
                    .HasForeignKey(x => x.RepliedBy)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => x.StatusId);
                entity.HasIndex(x => x.CreatedAt);

            });

            // ================= CONTACT STATUS =================
            e.Entity<ContactStatus>(entity =>
            {
                entity.ToTable("ContactStatuses");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasData(
                    new ContactStatus { Id = 1, Name = "Pending" },
                    new ContactStatus { Id = 2, Name = "Resolved" },
                    new ContactStatus { Id = 3, Name = "Rejected" },
                    new ContactStatus { Id = 4, Name = "InProgress" },
                    new ContactStatus { Id = 5, Name = "Closed" }
                );
            });

            // ================= MEDIA =================
            e.Entity<Media>(entity =>
            {
                entity.ToTable("Media");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.FileName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(x => x.Url)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(x => x.Size)
                    .IsRequired();

                entity.Property(x => x.MimeType)
                    .HasMaxLength(100);

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // ================= RELATION =================

                // User upload
                entity.HasOne(x => x.Uploader)
                    .WithMany()
                    .HasForeignKey(x => x.UploadedBy)
                    .OnDelete(DeleteBehavior.Restrict);

                // Banner
                entity.HasMany(x => x.Banners)
                    .WithOne(x => x.Media)
                    .HasForeignKey(x => x.MediaId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Partner
                entity.HasMany(x => x.Partners)
                    .WithOne(x => x.Media)
                    .HasForeignKey(x => x.MediaId)
                    .OnDelete(DeleteBehavior.Restrict);

                // ================= INDEX =================

                entity.HasIndex(x => x.UploadedBy);
                entity.HasIndex(x => x.CreatedAt);

            });

            // ================= CATEGORY =================
            e.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(x => x.Slug)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasIndex(x => x.Slug)
                    .IsUnique();

                entity.Property(x => x.SortOrder)
                    .HasDefaultValue(0);

                entity.Property(x => x.IsActive)
                    .HasDefaultValue(true);

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // ================= SELF RELATION =================
                entity.HasOne(x => x.Parent)
                    .WithMany(x => x.Children)
                    .HasForeignKey(x => x.ParentId)
                    .OnDelete(DeleteBehavior.Restrict); // ❗ cực quan trọng (tránh xóa dây chuyền)

                // ================= INDEX =================
                entity.HasIndex(x => x.ParentId);
                entity.HasIndex(x => x.SortOrder);

            });

            // ================= POST STATUS =================
            e.Entity<PostStatus>(entity =>
            {
                entity.ToTable("PostStatuses");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(x => x.Name)
                    .IsUnique();

                // Seed
                entity.HasData(
                    new PostStatus { Id = 1, Name = "Draft" },
                    new PostStatus { Id = 2, Name = "Published" },
                    new PostStatus { Id = 3, Name = "Archived" },
                    new PostStatus { Id = 4, Name = "PendingReview" },
                    new PostStatus { Id = 5, Name = "Rejected" }
                );
            });

            // ================= POST =================
            e.Entity<Post>(entity =>
            {
                entity.ToTable("Posts");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(x => x.Slug)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasIndex(x => x.Slug)
                    .IsUnique();

                entity.Property(x => x.Content)
                    .HasColumnType("nvarchar(max)");

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(x => x.UpdatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // ================= RELATION =================

                entity.HasOne(x => x.Thumbnail)
                    .WithMany(x => x.Posts)
                    .HasForeignKey(x => x.ThumbnailId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Author)
                    .WithMany()
                    .HasForeignKey(x => x.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Status)
                    .WithMany(x => x.Posts)
                    .HasForeignKey(x => x.StatusId)
                    .OnDelete(DeleteBehavior.Restrict);

                // ================= INDEX =================
                entity.HasIndex(x => x.StatusId);
                entity.HasIndex(x => x.AuthorId);
                entity.HasIndex(x => x.PublishedAt);

            });

            // ================= POST TAG =================
            e.Entity<PostTag>(entity =>
            {
                entity.ToTable("PostTags");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.Slug)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(x => x.Slug)
                    .IsUnique();
            });

            // ================= POST TAG MAP =================
            e.Entity<PostTagMap>(entity =>
            {
                entity.ToTable("PostTagMaps");

                entity.HasKey(x => new { x.PostId, x.TagId });

                entity.HasOne(x => x.Post)
                    .WithMany(x => x.PostTags)
                    .HasForeignKey(x => x.PostId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Tag)
                    .WithMany(x => x.PostTags)
                    .HasForeignKey(x => x.TagId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            // ================= PRODUCT STATUS =================
            e.Entity<ProductStatus>(entity =>
            {
                entity.ToTable("ProductStatuses");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(x => x.NormalizedName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(x => x.Description)
                    .HasMaxLength(255);

                entity.HasIndex(x => x.NormalizedName)
                    .IsUnique();
            });

            // ================= PRODUCT =================
            e.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.Slug)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasIndex(x => x.Slug)
                    .IsUnique();

                entity.Property(x => x.Description)
                    .HasColumnType("nvarchar(max)");

                entity.Property(x => x.Model)
                    .HasMaxLength(100);

                entity.Property(x => x.IsFeatured)
                    .HasDefaultValue(false);

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(x => x.UpdatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // ================= RELATION =================

                entity.HasOne(x => x.Category)
                    .WithMany()
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Status)
                    .WithMany(x => x.Products)
                    .HasForeignKey(x => x.StatusId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(x => x.Images)
                    .WithOne(x => x.Product)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(x => x.Documents)
                    .WithOne(x => x.Product)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(x => x.Specs)
                    .WithOne(x => x.Product)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                // ================= INDEX =================
                entity.HasIndex(x => x.CategoryId);
                entity.HasIndex(x => x.StatusId);
                entity.HasIndex(x => x.IsFeatured);
            });

            // ================= FILE TYPE =================
            e.Entity<FileType>(entity =>
            {
                entity.ToTable("FileTypes");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasData(
                    new FileType { Id = 1, Name = "PDF" },
                    new FileType { Id = 2, Name = "DOCX" },
                    new FileType { Id = 3, Name = "XLSX" },
                    new FileType { Id = 4, Name = "IMAGE" },
                    new FileType { Id = 5, Name = "ZIP" }
                );
            });

            // ================= PRODUCT IMAGE =================
            e.Entity<ProductImage>(entity =>
            {
                entity.ToTable("ProductImages");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.AltText)
                    .HasMaxLength(255);

                entity.Property(x => x.SortOrder)
                    .HasDefaultValue(0);

                entity.Property(x => x.IsPrimary)
                    .HasDefaultValue(false);

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(x => x.Product)
                    .WithMany(x => x.Images)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Media)
                    .WithMany(x => x.ProductImages)
                    .HasForeignKey(x => x.MediaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => x.ProductId);
            });

            // ================= PRODUCT DOCUMENT =================
            e.Entity<ProductDocument>(entity =>
            {
                entity.ToTable("ProductDocuments");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title)
                    .HasMaxLength(200);

                entity.Property(x => x.SortOrder)
                    .HasDefaultValue(0);

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(x => x.Product)
                    .WithMany(x => x.Documents)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Media)
                    .WithMany(x => x.ProductDocuments)
                    .HasForeignKey(x => x.MediaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.FileType)
                    .WithMany(x => x.Documents)
                    .HasForeignKey(x => x.FileTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ================= SPEC KEY =================
            e.Entity<SpecKey>(entity =>
            {
                entity.ToTable("SpecKeys");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

            });

            // ================= PRODUCT SPEC =================
            e.Entity<ProductSpec>(entity =>
            {
                entity.ToTable("ProductSpecs");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.SpecValue)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(x => x.Product)
                    .WithMany(x => x.Specs)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.SpecKey)
                    .WithMany(x => x.ProductSpecs)
                    .HasForeignKey(x => x.SpecKeyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => x.ProductId);
            });

            // ======================= AUDIT ACTION =======================
            e.Entity<AuditAction>(entity =>
            {
                entity.ToTable("AuditActions");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(x => x.NormalizedName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(x => x.NormalizedName)
                    .IsUnique();

                // Seed
                entity.HasData(
                    new AuditAction { Id = 1, Name = "Create", NormalizedName = "CREATE" },
                    new AuditAction { Id = 2, Name = "Update", NormalizedName = "UPDATE" },
                    new AuditAction { Id = 3, Name = "Delete", NormalizedName = "DELETE" },
                    new AuditAction { Id = 4, Name = "Restore", NormalizedName = "RESTORE" },
                    new AuditAction { Id = 5, Name = "Login", NormalizedName = "LOGIN" }
                );
            });

            // ======================= AUDIT ENTITY TYPE =======================
            e.Entity<AuditEntityType>(entity =>
            {
                entity.ToTable("AuditEntityTypes");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.NormalizedName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(x => x.NormalizedName)
                    .IsUnique();

                // Seed
                entity.HasData(
                    new AuditEntityType { Id = 1, Name = "User", NormalizedName = "USER" },
                    new AuditEntityType { Id = 2, Name = "Product", NormalizedName = "PRODUCT" },
                    new AuditEntityType { Id = 3, Name = "Order", NormalizedName = "ORDER" },
                    new AuditEntityType { Id = 4, Name = "Category", NormalizedName = "CATEGORY" },
                    new AuditEntityType { Id = 5, Name = "Voucher", NormalizedName = "VOUCHER" }
                );
            });

            // ======================= AUDIT LOG =======================
            e.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLogs");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.TableName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.OldValues)
                    .HasColumnType("nvarchar(max)");

                entity.Property(x => x.NewValues)
                    .HasColumnType("nvarchar(max)");

                entity.Property(x => x.IpAddress)
                    .HasMaxLength(50);

                entity.Property(x => x.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                // ================= RELATION =================
                entity.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Action)
                    .WithMany()
                    .HasForeignKey(x => x.ActionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.EntityType)
                    .WithMany()
                    .HasForeignKey(x => x.EntityTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                // ================= INDEX =================
                entity.HasIndex(x => x.UserId);
                entity.HasIndex(x => x.ActionId);
                entity.HasIndex(x => x.EntityTypeId);
                entity.HasIndex(x => x.CreatedAt);

            });


            // ================= SETTING =================
            e.Entity<Setting>(entity =>
            {
                entity.ToTable("Settings");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Key)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(x => x.Value)
                    .HasColumnType("nvarchar(max)");

                entity.Property(x => x.Group)
                    .HasMaxLength(100);

                entity.Property(x => x.Description)
                    .HasMaxLength(500);

                // Unique key (quan trọng)
                entity.HasIndex(x => x.Key)
                    .IsUnique();

                entity.HasIndex(x => x.Group);

            });
        }
    }
}