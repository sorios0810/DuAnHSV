namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Brand")]
    public partial class Brand
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int brand_id { get; set; }

        [Required]
        [StringLength(100)]
        public string brand_name { get; set; }

        [StringLength(500, ErrorMessage = "?nh th??ng hi?u không ???c quá 500 ký t?", MinimumLength = 1)]
        public string brand_image { get; set; }

        [StringLength(109)]
        public string slug { get; set; }

        [StringLength(200)]
        public string description { get; set; }

        [StringLength(200)]
        public string Web_directory { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [Required]
        [StringLength(100)]
        public string create_by { get; set; }

        public DateTime create_at { get; set; }

        [Required]
        [StringLength(100)]
        public string update_by { get; set; }

        public DateTime update_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
