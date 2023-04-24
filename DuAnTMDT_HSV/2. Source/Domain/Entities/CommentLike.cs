namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommentLike")]
    public partial class CommentLike
    {
        [Key]
        public int comment_like_id { get; set; }

        public int comment_id { get; set; }

        public int account_id { get; set; }

        [StringLength(1)]
        public string comment_like { get; set; }

        public DateTime create_at { get; set; }

        public virtual Account Account { get; set; }

        public virtual NewComment NewComment { get; set; }
    }
}
