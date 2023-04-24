using Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Domain
{
    public partial class CmsDbContext : DbContext
    {
        public CmsDbContext()
            : base("name=CmsDbContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountAddress> AccountAddresses { get; set; }
        public virtual DbSet<API_Key> API_Key { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<BannerDetail> BannerDetails { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<ChildCategory> ChildCategories { get; set; }
        public virtual DbSet<CommentLike> CommentLikes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<FeedbackImage> FeedbackImages { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<GenreDetail> GenreDetails { get; set; }
        public virtual DbSet<HistoryPrice> HistoryPrices { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<New> News { get; set; }
        public virtual DbSet<NewComment> NewComments { get; set; }
        public virtual DbSet<NewProduct> NewProducts { get; set; }
        public virtual DbSet<NewTag> NewTags { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderAddress> OrderAddresses { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<ParentCategory> ParentCategories { get; set; }
        public virtual DbSet<ParentGenre> ParentGenres { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<ReplyComment> ReplyComments { get; set; }
        public virtual DbSet<ReplyCommentLike> ReplyCommentLikes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<StickyPost> StickyPosts { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<Warehouse_Export> Warehouse_Export { get; set; }
        public virtual DbSet<Warehouse_Purchase_Order> Warehouse_Purchase_Order { get; set; }
        public virtual DbSet<Warehouse_Receipt> Warehouse_Receipt { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.CommentLikes)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Feedbacks)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.ReplyComments)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.ReplyCommentLikes)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Banner>()
                .HasOptional(e => e.BannerDetail)
                .WithRequired(e => e.Banner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BannerDetail>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BannerDetail>()
                .Property(e => e.create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .Property(e => e.create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Brand)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cart>()
                .HasMany(e => e.CartItems)
                .WithRequired(e => e.Cart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChildCategory>()
                .HasMany(e => e.News)
                .WithRequired(e => e.ChildCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Delivery>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Delivery>()
                .Property(e => e.create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Delivery>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Delivery>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Delivery)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Discount>()
                .Property(e => e.create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Discount>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Discount>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Discount)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<District>()
                .HasMany(e => e.Wards)
                .WithRequired(e => e.District)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Feedback>()
               .Property(e => e.rate_star);

            modelBuilder.Entity<Feedback>()
                .Property(e => e.create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Feedback>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Feedback>()
                .HasMany(e => e.FeedbackImages)
                .WithRequired(e => e.Feedback)
                .HasForeignKey(e => new { e.feedback_id, e.account_id });

            modelBuilder.Entity<FeedbackImage>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<FeedbackImage>()
                .Property(e => e.create_by)
                .IsUnicode(false);

            modelBuilder.Entity<FeedbackImage>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Genre>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Genre>()
                .Property(e => e.create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.BannerDetails)
                .WithRequired(e => e.Genre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.NewProducts)
                .WithRequired(e => e.Genre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GenreDetail>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<New>()
                .HasMany(e => e.NewComments)
                .WithRequired(e => e.New)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<New>()
                .HasMany(e => e.StickyPosts)
                .WithRequired(e => e.New)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<New>()
                .HasOptional(e => e.NewProduct)
                .WithRequired(e => e.New);

            modelBuilder.Entity<New>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.News)
                .Map(m => m.ToTable("NewTag").MapLeftKey("news_id").MapRightKey("tag_id"));

            modelBuilder.Entity<Order>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderAddress>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.OrderAddress)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.create_by)
                .IsUnicode(false);

            modelBuilder.Entity<ParentCategory>()
                .HasMany(e => e.ChildCategories)
                .WithRequired(e => e.ParentCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ParentGenre>()
                .HasMany(e => e.Genres)
                .WithRequired(e => e.ParentGenre)
                .HasForeignKey(e => e.parent_genre_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Payment>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Payment)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Permission>()
            //    .HasMany(e => e.Roles)
            //    .WithMany(e => e.Permissions)
            //    .Map(m => m.ToTable("RolePermission").MapLeftKey("permission_id").MapRightKey("role_id"));

            modelBuilder.Entity<Product>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.create_by)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.BannerDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Inventories)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.NewProducts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.HistoryPrices)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductImages)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Province>()
                .HasMany(e => e.Districts)
                .WithRequired(e => e.Province)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tax>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Tax)
                .HasForeignKey(e => e.tax_id);

            modelBuilder.Entity<Warehouse>()
                .Property(e => e.phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.Inventories)
                .WithOptional(e => e.Warehouse)
                .HasForeignKey(e => e.warehouse_id);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.Warehouse_Receipt)
                .WithOptional(e => e.Warehouse)
                .HasForeignKey(e => e.id_warehouse);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.Warehouse_Export)
                .WithOptional(e => e.Warehouse)
                .HasForeignKey(e => e.id_warehouse);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.Warehouse_Purchase_Order)
                .WithOptional(e => e.Warehouse)
                .HasForeignKey(e => e.id_warehouse);

            modelBuilder.Entity<Warehouse_Purchase_Order>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
