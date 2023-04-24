namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Diagnostics.CodeAnalysis;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BannerDetails = new HashSet<BannerDetail>();
            CartItems = new HashSet<CartItem>();
            Inventories = new HashSet<Inventory>();
            NewProducts = new HashSet<NewProduct>();
            HistoryPrices = new HashSet<HistoryPrice>();
            Warehouse_Purchase_Order = new HashSet<Warehouse_Purchase_Order>();
            ProductImages = new HashSet<ProductImage>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int product_id { get; set; }

        public int brand_id { get; set; }

        public int discount_id { get; set; }

        public int? supplier_id { get; set; }

        public int? genre_detail_id { get; set; }

        [Required]
        [StringLength(200)]
        public string product_name { get; set; }

        [Required]
        [StringLength(150)]
        public string title_seo { get; set; }

        [StringLength(159)]
        public string slug { get; set; }

        public long view { get; set; }

        public long buyturn { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        public DateTime create_at { get; set; }

        [Required]
        [StringLength(100)]
        public string create_by { get; set; }

        [StringLength(100)]
        public string update_by { get; set; }

        public DateTime update_at { get; set; }

        [StringLength(500)]
        public string image { get; set; }

        public string description { get; set; }

        public string specification { get; set; }

        public int? tax_id { get; set; }

        [StringLength(10)]
        public string color { get; set; }

        [StringLength(100)]
        public string model { get; set; }

        public double selling_price { get; set; }

        public double vendor_price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BannerDetail> BannerDetails { get; set; }

        public virtual Brand Brand { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartItem> CartItems { get; set; }

        public virtual Discount Discount { get; set; }

        public virtual GenreDetail GenreDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventory> Inventories { get; set; }

        //public virtual Inventory Inventories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewProduct> NewProducts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoryPrice> HistoryPrices { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Warehouse_Purchase_Order> Warehouse_Purchase_Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImage> ProductImages { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual Tax Tax { get; set; }
    }
}
