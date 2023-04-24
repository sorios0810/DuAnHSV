namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductImage")]
    public partial class ProductImage
    {
        [Key]
        public int product_image_id { get; set; }

        public int product_id { get; set; }

        public int genre_id { get; set; }

        [StringLength(500)]
        public string image_1 { get; set; }

        [StringLength(500)]
        public string image_2 { get; set; }

        [StringLength(500)]
        public string image_3 { get; set; }

        [StringLength(500)]
        public string image_4 { get; set; }

        [StringLength(500)]
        public string image_5 { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [StringLength(100)]
        public string create_by { get; set; }

        [StringLength(100)]
        public string update_by { get; set; }

        public DateTime create_at { get; set; }

        public DateTime update_at { get; set; }

        public virtual Product Product { get; set; }
    }
}
