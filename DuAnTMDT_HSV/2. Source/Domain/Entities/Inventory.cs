namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inventory")]
    public partial class Inventory
    {
        public int id { get; set; }

        public int product_id { get; set; }

        private int _quantity;
        [Range(1, 10000, ErrorMessage = "S? l??ng ph?i n?m trong kho?ng t? 1 ??n 10000")]
        [Required(ErrorMessage = "Vui lòng nh?p s? l??ng")]
        public int quantity_on_hand
        {
            get { return this._quantity; }
            set { this._quantity = value; }
        }

        public DateTime modify_at { get; set; }

        [Required]
        public string modify_by { get; set; }

        public int? warehouse_id { get; set; }

        public int? opening_stock { get; set; }

        public int? closing_stock { get; set; }

        public virtual Product Product { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}
