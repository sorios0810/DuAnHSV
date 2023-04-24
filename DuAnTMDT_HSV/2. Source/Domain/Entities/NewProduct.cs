namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NewProduct")]
    public partial class NewProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int news_id { get; set; }

        public int product_id { get; set; }

        public int genre_id { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual New New { get; set; }

        public virtual Product Product { get; set; }
    }
}
