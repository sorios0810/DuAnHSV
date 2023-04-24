namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public partial class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int cart_id { get; set; }

        public int product_id { get; set; }

        public DateTime create_at { get; set; }

        public int? quantity { get; set; }

        public double? total { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual Product Product { get; set; }

    }
}
