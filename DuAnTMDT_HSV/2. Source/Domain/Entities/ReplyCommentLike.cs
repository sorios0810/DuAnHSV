namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReplyCommentLike")]
    public partial class ReplyCommentLike
    {
        [Key]
        public int reply_like_id { get; set; }

        public int reply_comment_id { get; set; }

        public int account_id { get; set; }

        [StringLength(1)]
        public string reply_like { get; set; }

        public DateTime create_at { get; set; }

        public virtual Account Account { get; set; }

        public virtual ReplyComment ReplyComment { get; set; }
    }
}
