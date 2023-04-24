namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Banner")]
    public partial class Banner
    {
        [Key]
        public int banner_id { get; set; }

        [Required]
        [StringLength(200)]
        public string banner_name { get; set; }

        [StringLength(209)]
        public string slug { get; set; }

        public DateTime banner_start { get; set; }

        public DateTime banner_end { get; set; }

        [StringLength(100)]
        public string description { get; set; }

        [Required]
        [StringLength(500)]
        public string image_thumbnail { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [Required]
        [StringLength(100)]
        public string create_by { get; set; }

        public DateTime create_at { get; set; }

        public int banner_type { get; set; }

        [Required]
        [StringLength(100)]
        public string update_by { get; set; }

        public DateTime update_at { get; set; }

        public virtual BannerDetail BannerDetail { get; set; }
    }
}
