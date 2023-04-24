namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Warehouse_Purchase_Order
    {
        [Key]
        public int purchase_order_id { get; set; }

        public int? id_warehouse { get; set; }

        public int? product_id { get; set; }

        public int? supplier_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? order_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? delivery_date { get; set; }

        public string approved_by { get; set; }

        public int? quantity { get; set; }

        public double? rate { get; set; }

        public double? price { get; set; }

        public double? shipping { get; set; }

        public double? tax { get; set; }

        public double? discount { get; set; }

        public double? total { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        public string create_by { get; set; }

        public DateTime? create_at { get; set; }

        public virtual Product Product { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}
