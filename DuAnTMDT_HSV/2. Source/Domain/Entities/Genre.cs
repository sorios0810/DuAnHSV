namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Genre")]
    public partial class Genre
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Genre()
        {
            BannerDetails = new HashSet<BannerDetail>();
            GenreDetails = new HashSet<GenreDetail>();
            NewProducts = new HashSet<NewProduct>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int genre_id { get; set; }

        public int parent_genre_id { get; set; }

        [Required]
        [StringLength(100)]
        public string genre_name { get; set; }

        [StringLength(109)]
        public string slug { get; set; }

        [StringLength(500)]
        public string genre_image { get; set; }

        [StringLength(200)]
        public string description { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        public DateTime create_at { get; set; }

        [Required]
        [StringLength(100)]
        public string create_by { get; set; }

        [Required]
        [StringLength(100)]
        public string update_by { get; set; }

        public DateTime update_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BannerDetail> BannerDetails { get; set; }

        public virtual ParentGenre ParentGenre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GenreDetail> GenreDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewProduct> NewProducts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
