namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReplyComment")]
    public partial class ReplyComment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReplyComment()
        {
            ReplyCommentLikes = new HashSet<ReplyCommentLike>();
        }

        [Key]
        public int reply_comment_id { get; set; }

        public int comment_id { get; set; }

        public int account_id { get; set; }

        [StringLength(500)]
        public string reply_comment_content { get; set; }

        public DateTime create_at { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        public virtual Account Account { get; set; }

        public virtual NewComment NewComment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReplyCommentLike> ReplyCommentLikes { get; set; }
    }
}
