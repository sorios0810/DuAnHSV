namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("New")]
    public partial class New
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public New()
        {
            NewComments = new HashSet<NewComment>();
            StickyPosts = new HashSet<StickyPost>();
            Tags = new HashSet<Tag>();
        }

        [Key]
        public int news_id { get; set; }

        public int account_id { get; set; }

        public int childcategory_id { get; set; }

        [Required]
        [StringLength(500)]
        public string news_title { get; set; }

        [Required]
        [StringLength(150)]
        public string meta_title { get; set; }

        [Required]
        [StringLength(159)]
        public string slug { get; set; }

        [Required]
        public string news_content { get; set; }

        public int ViewCount { get; set; }

        [Required]
        [StringLength(500)]
        public string image { get; set; }

        [Required]
        [StringLength(500)]
        public string image2 { get; set; }

        public DateTime create_at { get; set; }

        public DateTime update_at { get; set; }

        [StringLength(100)]
        public string update_by { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        public virtual Account Account { get; set; }

        public virtual ChildCategory ChildCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewComment> NewComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StickyPost> StickyPosts { get; set; }

        public virtual NewProduct NewProduct { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewTag> NewTags { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<NewProduct> NewProducts { get; set; }
    }
}
