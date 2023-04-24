namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Feedback()
        {
            FeedbackImages = new HashSet<FeedbackImage>();
        }

        [Key]
        [Column(Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int feedback_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int account_id { get; set; }

        public int product_id { get; set; }

        public int genre_id { get; set; }

        public int parent_feedback_id { get; set; }

        [StringLength(200, ErrorMessage = "N?i dung ?ánh giá không ???c quá 200 ký t?", MinimumLength = 1)]
        public string description { get; set; }

        public int rate_star { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime create_at { get; set; }

        [Required]
        [StringLength(100)]
        public string create_by { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [Required]
        [StringLength(100)]
        public string update_by { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime update_at { get; set; }

        public virtual Account Account { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeedbackImage> FeedbackImages { get; set; }
    }
}
