namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StickyPost")]
    public partial class StickyPost
    {
        public int id { get; set; }

        public int news_id { get; set; }

        [Required]
        [StringLength(100)]
        public string create_by { get; set; }

        [Required]
        [StringLength(100)]
        public string update_by { get; set; }

        public DateTime sticky_start { get; set; }

        public DateTime sticky_end { get; set; }

        public virtual New New { get; set; }
    }
}
