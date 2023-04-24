namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Warehouse_Export
    {
        [Key]
        public int export_id { get; set; }

        public int? id_warehouse { get; set; }

        [Column(TypeName = "date")]
        public DateTime? export_date { get; set; }

        public string reason_issue { get; set; }

        public string create_by { get; set; }

        public DateTime? create_at { get; set; }

        public DateTime? update_at { get; set; }

        [StringLength(1)]
        public string update_by { get; set; }

        public virtual Order Order { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}
