namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GenreDetail")]
    public partial class GenreDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GenreDetail()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int genre_detail_id { get; set; }

        public int? genre_id { get; set; }

        [Required]
        [StringLength(100)]
        public string genre_detail_name { get; set; }

        public string slug { get; set; }

        [StringLength(500)]
        public string genre_image { get; set; }

        public string description { get; set; }

        public DateTime create_at { get; set; }

        [StringLength(100)]
        public string create_by { get; set; }

        public DateTime update_at { get; set; }

        [StringLength(100)]
        public string update_by { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        public virtual Genre Genre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
