namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tax")]
    public partial class Tax
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tax()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int taxes_id { get; set; }

        //VAT Name
        [Required(ErrorMessage = "Nh?p tên lo?i VAT")]
        [StringLength(50, ErrorMessage = "Tên VAT t?i ?a 50", MinimumLength = 1)]
        public string taxes_name { get; set; }

        //ph?n tr?m VAT
        [Required(ErrorMessage = "Nh?p tên lo?i VAT")]
        public int taxes_value { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime create_at { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime update_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
