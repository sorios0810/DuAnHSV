using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("NewTags")]
    public class NewTag
    {
        [Key]
        [Column(Order = 1)]
        public int news_id { get; set; }

        [Key]
        [Column(Order = 2)]
        public int tag_id { get; set; }

        public virtual New News { get; set; }

        public virtual Tag Tags { get; set; }
    }
}
