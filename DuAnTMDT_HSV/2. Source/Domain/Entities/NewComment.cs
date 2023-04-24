namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NewComment")]
    public partial class NewComment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NewComment()
        {
            CommentLikes = new HashSet<CommentLike>();
            ReplyComments = new HashSet<ReplyComment>();
        }

        [Key]
        public int comment_id { get; set; }

        public int account_id { get; set; }

        public int news_id { get; set; }

        [Required]
        [StringLength(500)]
        public string comment_content { get; set; }

        public DateTime create_at { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommentLike> CommentLikes { get; set; }

        public virtual New New { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReplyComment> ReplyComments { get; set; }
    }
}
