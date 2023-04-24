namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Warehouse_Receipt
    {
        [Key]
        public int receipt_id { get; set; }

        public int? id_warehouse { get; set; }

        public int? supplier_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? reciept_date { get; set; }

        public string name_deliverer { get; set; }

        public string create_by { get; set; }

        public DateTime? create_at { get; set; }

        public double? debit { get; set; }

        public double? credit { get; set; }

        public DateTime? update_at { get; set; }

        public string update_by { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}
