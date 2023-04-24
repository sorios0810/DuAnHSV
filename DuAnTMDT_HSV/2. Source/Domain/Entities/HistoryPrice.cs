namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HistoryPrice")]
    public partial class HistoryPrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_id { get; set; }

        public double selling_price { get; set; }

        public double vendor_price { get; set; }

        public DateTime update_at { get; set; }

        public string update_by { get; set; }

        public virtual Product Product { get; set; }
    }
}
