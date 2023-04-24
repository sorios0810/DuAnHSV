namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        public int id { get; set; }

        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_id { get; set; }

        [Column(Order = 1)]
        public int genre_id { get; set; }

        [Column(Order = 3)]
        public int order_id { get; set; }

        public double price { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [StringLength(20)]
        public string discount_code { get; set; }

        public int quantity { get; set; }

        [Required]
        [StringLength(100)]
        public string create_by { get; set; }

        public DateTime create_at { get; set; }

        [Required]
        [StringLength(100)]
        public string update_by { get; set; }

        public DateTime? update_at { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
