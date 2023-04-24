namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Warehouse_Export = new HashSet<Warehouse_Export>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int order_id { get; set; }

        public int order_address_id { get; set; }

        public int payment_id { get; set; }

        public int delivery_id { get; set; }

        public DateTime oder_date { get; set; }

        public int account_id { get; set; }

        [StringLength(1)]
        public string payment_transaction { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [StringLength(200)]
        public string order_note { get; set; }

        public DateTime create_at { get; set; }

        public double total { get; set; }

        [Required]
        [StringLength(100)]
        public string create_by { get; set; }

        [Required]
        [StringLength(100)]
        public string update_by { get; set; }

        public DateTime? update_at { get; set; }

        public virtual Account Account { get; set; }

        public virtual Delivery Delivery { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Warehouse_Export> Warehouse_Export { get; set; }

        public virtual OrderAddress OrderAddress { get; set; }

        public virtual Payment Payment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
