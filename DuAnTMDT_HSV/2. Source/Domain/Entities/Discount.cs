namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.UI.WebControls.WebParts;

    [Table("Discount")]
    public partial class Discount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Discount()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int discount_id { get; set; }

        [Required]
        [StringLength(200)]
        public string discount_name { get; set; }

        public DateTime discount_start { get; set; }

        public DateTime discount_end { get; set; }

        public double discount_price { get; set; }

        [StringLength(20)]
        public string discount_code { get; set; }

        public double discount_max { get; set; }

        public int discount_type { get; set; }

        public DateTime create_at { get; set; }

        [Required]
        [StringLength(100)]
        public string create_by { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [StringLength(1)]
        public string discount_global { get; set; }

        [StringLength(10)]
        public string quantity { get; set; }

        [Required]
        [StringLength(100)]
        public string update_by { get; set; }

        public DateTime update_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
