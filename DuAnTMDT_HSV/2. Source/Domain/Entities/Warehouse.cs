namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Warehouse")]
    public partial class Warehouse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Warehouse()
        {
            Inventories = new HashSet<Inventory>();
            Warehouse_Receipt = new HashSet<Warehouse_Receipt>();
            Warehouse_Export = new HashSet<Warehouse_Export>();
            Warehouse_Purchase_Order = new HashSet<Warehouse_Purchase_Order>();
        }

        public int id { get; set; }

        public string stockkeeper { get; set; }

        [StringLength(10)]
        public string phone { get; set; }

        public string address_warehouse { get; set; }

        public int? ward_id { get; set; }

        public int? district_id { get; set; }

        public int? province_id { get; set; }

        public DateTime? modify_at { get; set; }

        public string modify_by { get; set; }

        public virtual District District { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventory> Inventories { get; set; }

        public virtual Province Province { get; set; }

        public virtual Ward Ward { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Warehouse_Receipt> Warehouse_Receipt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Warehouse_Export> Warehouse_Export { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Warehouse_Purchase_Order> Warehouse_Purchase_Order { get; set; }
    }
}
