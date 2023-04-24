namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tag")]
    public partial class Tag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tag()
        {
            News = new HashSet<New>();
            NewTags = new HashSet<NewTag>();
        }

        [Key]
        public int tag_id { get; set; }

        [Required(ErrorMessage = "Nh?p tên tag")]
        [StringLength(100, ErrorMessage = "Tên tag không ???c quá 100 ký t?", MinimumLength = 1)]
        public string tag_name { get; set; }

        [Required]
        [StringLength(100)]
        public string slug { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<New> News { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewTag> NewTags { get; set; }
    }
}
