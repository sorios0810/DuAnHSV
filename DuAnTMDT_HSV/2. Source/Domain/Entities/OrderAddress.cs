namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderAddress")]
    public partial class OrderAddress
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderAddress()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int order_address_id { get; set; }

        [StringLength(10)]
        public string order_address_phonenumber { get; set; }

        [StringLength(20)]
        public string order_address_username { get; set; }

        [StringLength(100)]
        public string order_adress_email { get; set; }

        [StringLength(150)]
        public string order_address_content { get; set; }

        public int times_edit_adress { get; set; }

        [StringLength(50)]
        public string order_adress_province { get; set; }

        [StringLength(50)]
        public string order_adress_district { get; set; }

        [StringLength(50)]
        public string order_adress_wards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
