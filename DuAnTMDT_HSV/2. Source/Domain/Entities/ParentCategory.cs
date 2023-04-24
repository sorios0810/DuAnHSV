namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParentCategory")]
    public partial class ParentCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ParentCategory()
        {
            ChildCategories = new HashSet<ChildCategory>();
        }

        [Key]
        public int parentcategory_id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(109)]
        public string slug { get; set; }

        [Required]
        [StringLength(500)]
        public string image { get; set; }

        [Required]
        [StringLength(500)]
        public string image2 { get; set; }

        [StringLength(100)]
        public string category_description { get; set; }

        public DateTime create_at { get; set; }

        public DateTime update_at { get; set; }

        [StringLength(100)]
        public string create_by { get; set; }

        [StringLength(100)]
        public string update_by { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChildCategory> ChildCategories { get; set; }
    }
}
